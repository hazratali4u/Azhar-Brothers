using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_RptOrderBookerWiseSale : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadDistributor();
        LoadOrderBooker();
        LoadPrincipal();
        SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
        txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpDistributor, dt, 0, 2, true);
    }
    protected void LoadPrincipal()
    {
        try
        {
            drpPrincipal.Items.Add(new ListItem("All", "0"));
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            //this.drpPrincipal.Items.Add(new ListItem("All", "0"));
            clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, "Company_Id", "Company_Name");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    private void LoadOrderBooker()
    {
        if (DrpDistributor.Items.Count > 0 )
        {
            DrpOrderBooker.Items.Add(new ListItem("All", "0"));
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(Constants.SALES_FORCE_ORDERBOOKER, int.Parse(DrpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue);
            clsWebFormUtil.FillDropDownList(DrpOrderBooker, m_dt, 0, 3, false);
        }
        else
        {
            DrpOrderBooker.Items.Clear();
        }
    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        //string PrincipalIDs = null;
        //string PrincipalNames = "";
        //foreach (ListItem li in cblPrincipal.Items)
        //{
        //    if (li.Selected)
        //    {
        //        PrincipalIDs += li.Value + ",";
        //        PrincipalNames += li.Text + ", ";
        //    }
        //}
        //PrincipalIDs = PrincipalIDs.Substring(0, PrincipalIDs.Length - 1);
        //PrincipalNames = PrincipalNames.Substring(0, PrincipalNames.Length - 1);
        DocumentPrintController mController = new DocumentPrintController();
        RptOrderBookerWiseSaleController RptInventoryCtl = new RptOrderBookerWiseSaleController();
        SAMSBusinessLayer.Reports.CrpOrderBookerWiseSaleReport CrpReport = new SAMSBusinessLayer.Reports.CrpOrderBookerWiseSaleReport();
        DataTable dt = mController.SelectReportTitle(int.Parse(DrpDistributor.SelectedValue.ToString()));
        DataSet ds = RptInventoryCtl.SelectOrderBookerWiseSale(int.Parse(DrpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(DrpOrderBooker.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text));
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();


        CrpReport.SetParameterValue("LOCATION_NAME", DrpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("PRINCIPAL_NAME", drpPrincipal.SelectedItem.Text);
        CrpReport.SetParameterValue("DATEFROM", txtStartDate.Text);
        CrpReport.SetParameterValue("DATETO", txtEndDate.Text);
        //CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
       
    }
}