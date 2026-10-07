using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Form For General Ledger Report
/// </summary>
public partial class Forms_RptLedgerReport2 : System.Web.UI.Page
{

    static string opType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadDistributor();
            
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

   
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        //DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        //this.drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        //clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);

        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        //DataTable dt2 = dt3.Clone();
        //DataRow[] dr = dt3.Select("SUBZONE_ID = 5");

        //if (dr.Length > 0)
        //{
        //    for (int k = 0; k < dr.Length; k++)
        //    {
        //        dt2.ImportRow(dr[k]);
        //    }
        //}

        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }

    private void LoadOpeningType()
    {
        VenderEntryController VendorCtl = new VenderEntryController();
        DataTable dtOpening = VendorCtl.GetVendorOpening(Constants.LongNullValue, Convert.ToInt64(DrpPrincipal.SelectedValue), Constants.IntNullValue);
        if (dtOpening.Rows.Count > 0)
        {

            if (dtOpening.Rows[0]["TYPE_ID"].ToString() == "0")
            {
                opType = "CR";
            }
            else
            {
                opType = "DR";
            }

        }
    }
    private decimal LoadVendoerOpBalance()
    {
        if (drpDistributor.Items.Count > 0 && DrpPrincipal.Items.Count > 0)
        {
            RptCustomerController mController = new RptCustomerController();
            DataTable dt = mController.GetVendoerOpening(int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()),
                      DateTime.Parse(txtStartDate.Text + " 00:00:00"));

            if (decimal.Parse(dt.Rows[0][0].ToString()) > 0)
            {
                opType = "DR";

            }
            else
            {
                opType = "CR";
            }


            return decimal.Parse(dt.Rows[0][0].ToString());
        }
        return 0;
    }

    private void showReport(int reportType)
    {
        try
        {
            LoadOpeningType();
            DocumentPrintController DPrint = new DocumentPrintController();
            RptCustomerController RptCustCtl = new RptCustomerController();

            DataSet ds = null;

            {
                ds = RptCustCtl.GetVendoerLedger(int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()),
                          DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

                DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

                CrpVendorLedger CrpReport = new CrpVendorLedger();
                ReportDocument subReport = CrpReport.OpenSubreport("SubReport");

                CrpReport.SetDataSource(ds);
                subReport.SetDataSource(ds);

                CrpReport.Refresh();

                CrpReport.SetParameterValue("FromDate", DateTime.Parse(txtStartDate.Text));
                CrpReport.SetParameterValue("To_date", DateTime.Parse(txtEndDate.Text));
                CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
                CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
                CrpReport.SetParameterValue("Op_Balance", LoadVendoerOpBalance());
                CrpReport.SetParameterValue("opType", opType);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

                Session.Add("CrpReport", CrpReport);
                Session.Add("ReportType", reportType);
                const string url = "'Default.aspx'";
                const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        showReport(0);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        showReport(1);
    }

  
}