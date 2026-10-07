using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_RptPrincipalDisbursmentReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadLocation();
        //LoadDistributor();
        LoadPrincipal();
        FormPanel.Visible = true;
        SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
        txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
    }
    protected void LoadLocation()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            this.DrpDistributor.DataSource = dt;
            this.DrpDistributor.DataTextField = "DISTRIBUTOR_NAME";
            this.DrpDistributor.DataValueField = "DISTRIBUTOR_ID";
            this.DrpDistributor.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    private void LoadDistributor()
    {
        //DrpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        //DistributorController DController = new DistributorController();
        //DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        //clsWebFormUtil.FillDropDownList(this.DrpDistributor, dt, 0, 2, true);
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

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        DateTime FROMDATE = Convert.ToDateTime(txtStartDate.Text);
        DateTime TODATE = Convert.ToDateTime(txtEndDate.Text);
        SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();

        SAMSBusinessLayer.Reports.CrpPrincipalDisbursmentReport CrpReport = new SAMSBusinessLayer.Reports.CrpPrincipalDisbursmentReport();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpDistributor.SelectedValue.ToString()));
        ds = RptSaleCtl.PrincpalDisbursement(int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(DrpDistributor.SelectedValue.ToString()), DateTime.Parse(FROMDATE.ToShortDateString()), DateTime.Parse(TODATE.ToShortDateString()));

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("PRINCIPAL_ID", this.drpPrincipal.SelectedItem.Text.ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_ID", this.DrpDistributor.SelectedItem.Text.ToString());
        CrpReport.SetParameterValue("FROMDATE", Convert.ToDateTime(txtStartDate.Text).ToShortDateString());
        CrpReport.SetParameterValue("TODATE", Convert.ToDateTime(txtEndDate.Text).ToShortDateString());
        CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());

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
        //ReportPanel.Visible = true;
        //FormPanel.Visible = false;
        //ReportDocument RptDoc = new ReportDocument();
        //ConnectionInfo crConnectionInfo = new ConnectionInfo();
        //SqlConnection con = new SqlConnection(Configuration.ConnectionString);
        //Tables CrTables = default(Tables);
        //System.Web.UI.WebControls.Table CrTable = default(System.Web.UI.WebControls.Table);
        //TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        //RptDoc.Load(Server.MapPath(Request.ApplicationPath + "/Rpt/CrpPrincipalDisbursmentReport.rpt"));
        //crConnectionInfo.ServerName = "FS-DEV-01";
        //crConnectionInfo.DatabaseName = "sams-azhar";
        //crConnectionInfo.UserID = "sa";
        //crConnectionInfo.Password = "fast1234";
        //CrTables = RptDoc.Database.Tables;
        //crtableLogoninfo = CrTables[0].LogOnInfo;
        //crtableLogoninfo.ConnectionInfo = crConnectionInfo;
        //CrTables[0].ApplyLogOnInfo(crtableLogoninfo);
        //RptDoc.SetDatabaseLogon(crConnectionInfo.UserID, crConnectionInfo.Password, crConnectionInfo.ServerName, crConnectionInfo.ServerName);
        //RptDoc.SetParameterValue("PRINCIPAL_ID", drpPrincipal.SelectedValue);
        //RptDoc.SetParameterValue("DISTRIBUTOR_ID", DrpDistributor.SelectedValue);
        //RptDoc.SetParameterValue("FROMDATE", Convert.ToDateTime(txtStartDate.Text).ToShortDateString());
        //RptDoc.SetParameterValue("TODATE", Convert.ToDateTime(txtEndDate.Text).ToShortDateString());
        //this.CrystalReportViewer1.ReportSource = RptDoc;
        //CrystalReportViewer1.Zoom(100);
        DateTime FROMDATE = Convert.ToDateTime(txtStartDate.Text);
        DateTime TODATE = Convert.ToDateTime(txtEndDate.Text);
        SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();

        SAMSBusinessLayer.Reports.CrpPrincipalDisbursmentReport CrpReport = new SAMSBusinessLayer.Reports.CrpPrincipalDisbursmentReport();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpDistributor.SelectedValue.ToString()));
         ds = RptSaleCtl.PrincpalDisbursement(int.Parse(drpPrincipal.SelectedValue.ToString()), int.Parse(DrpDistributor.SelectedValue.ToString()), DateTime.Parse(FROMDATE.ToShortDateString()), DateTime.Parse(TODATE.ToShortDateString()));

        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("PRINCIPAL_ID", this.drpPrincipal.SelectedItem.Text.ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_ID", this.DrpDistributor.SelectedItem.Text.ToString());
        CrpReport.SetParameterValue("FROMDATE", Convert.ToDateTime(txtStartDate.Text).ToShortDateString());
        CrpReport.SetParameterValue("TODATE", Convert.ToDateTime(txtEndDate.Text).ToShortDateString());
        CrpReport.SetParameterValue("COMPANY_NAME", dt.Rows[0]["COMPANY_NAME"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 1);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}