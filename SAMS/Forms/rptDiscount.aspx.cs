using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;

public partial class Forms_rptDiscount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            LoadCategory();
            LoadSKUDetail();
            LoadRoute();
            LoadCustomer();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
       // drpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpPrincipal, m_dt, 0, 1, false);

    }
    private void LoadCategory()
    {
        cblCategory.Items.Clear();
        
        SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        DataTable dtCategory = mHer_Controller.SelectSKUCategory(Convert.ToInt32(drpPrincipal.SelectedValue), Constants.SKUCategory);

        clsWebFormUtil.FillListBox(cblCategory, dtCategory, 0, 3);
     
        foreach (ListItem li in cblCategory.Items)
        {
            li.Selected = true;
        }
    }
    private void LoadRoute()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DrpRoute.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6);
        }
        else
        {
            DrpRoute.Items.Clear();
        }
    }
    private void LoadCustomer()
    {
        DrpCustomer.Items.Clear();
        DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Convert.ToInt32(DrpRoute.SelectedValue));
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 3);
        }
    }
    private void LoadSKUDetail()
    {
       string CategoryIDs = null;
      //  ddlSKU.Items.Clear();
        
        foreach (ListItem li in cblCategory.Items)
        {
            if (li.Selected)
            {
                CategoryIDs += li.Value + ",";
            }
        }
        SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        DataTable dt = mHer_Controller.SelectSkuHierarchy(2, CategoryIDs);
    //    ddlSKU.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
         //clsWebFormUtil.FillDropDownList(ddlSKU, dt, 0, 1);

         clsWebFormUtil.FillListBox(cblSKU, dt, 0, 1, true);

         foreach (ListItem li in cblSKU.Items)
         {
             li.Selected = true;
         }
    }
    
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRoute();
        LoadCustomer();
    }
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCategory();
        LoadSKUDetail();
    }
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }
    protected void cbAllCategory_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in cblCategory.Items)
        {
            li.Selected = cbAllCategory.Checked;
        }
        cblCategory_SelectedIndexChanged(null, null);
    }
    protected void cblCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSKUDetail();
    }
    protected void cbAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in cblSKU.Items)
        {
            li.Selected = cbAll.Checked;
        }
    }


    private void ShowReport(int p_ReportType)
    {
        string sbSKUIDs = null;

        foreach (ListItem li in cblSKU.Items)
        {
            if (li.Selected)
            {
                sbSKUIDs += li.Value + ",";
            }
        }
        RptCustomerController RptCustCtl = new RptCustomerController();
        DocumentPrintController DPrint = new DocumentPrintController();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DataControl dc = new DataControl();
            ds = RptCustCtl.GetCustomerWiseDiscountFreeSKU(Convert.ToInt32(drpDistributor.SelectedValue), Constants.IntNullValue, Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(DrpRoute.SelectedValue),
                sbSKUIDs.ToString(), Convert.ToInt32(Session["UserID"]), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

            ReportDocument CrpReport = new ReportDocument();
            CrpReport = new CrpCustomerWiseDiscountFreeSKU();

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("Principal", "");
            CrpReport.SetParameterValue("SKU", "");
            CrpReport.SetParameterValue("Area", DrpRoute.SelectedItem.Text);
            CrpReport.SetParameterValue("Customer", DrpCustomer.SelectedItem.Text);
            CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
            CrpReport.SetParameterValue("ToDate", txtEndDate.Text);

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", p_ReportType);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

   
}