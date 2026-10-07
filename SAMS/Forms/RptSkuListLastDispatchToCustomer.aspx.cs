using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI.WebControls;


public partial class Forms_RptSkuListLastDispatchToCustomer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            LoadLocation();
            LoadArea();
           // LoadMarket();
          //  LoadCustomer();
            LoadPrincipal();
            LoadSKUDetail();

            DateTime dt = (DateTime)Session["CurrentWorkDate"];

            txtStartDate.Text = dt.ToString("dd-MMM-yyyy");
            txtEndDate.Text = dt.ToString("dd-MMM-yyyy");

            txtStartDate.Attributes.Add("readonly", "readonly");

            txtEndDate.Attributes.Add("readonly", "readonly");
        }
    }
    //private void LoadCustomer()
    //{
    //    DrpCustomer.Items.Clear();
    //    DrpCustomer.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
    //    if (drpDistributor.Items.Count > 0 && ddlArea.Items.Count > 0)
    //    {
    //        if (long.Parse(ddlArea.SelectedValue) == Constants.LongNullValue)
    //        {
    //            CustomerDataController mController = new CustomerDataController();
    //            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
    //            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 3);
    //        }else
    //        {
    //            CustomerDataController mController = new CustomerDataController();
    //            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(ddlArea.SelectedValue.ToString()), Constants.IntNullValue);
    //            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 3);
    //        }
    //    }
    //}
    //private void LoadMarket()
    //{
    //    DrpMarket.Items.Clear();
    //    DrpMarket.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
    //    if (drpDistributor.Items.Count > 0  && ddlArea.Items.Count > 0)
    //    {
    //        DistributorRouteController gController = new DistributorRouteController();
    //        DataTable dt = gController.SelectDistributorRoute(Constants.LongNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, long.Parse(ddlArea.SelectedValue.ToString()));
    //        clsWebFormUtil.FillDropDownList(DrpMarket, dt, 0, 8, false);
    //    }
       
    //}
    protected void LoadLocation()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
            drpDistributor.DataSource = dt;
            drpDistributor.DataTextField = "DISTRIBUTOR_NAME";
            drpDistributor.DataValueField = "DISTRIBUTOR_ID";
            drpDistributor.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0)
        {
            ddlArea.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            ddlArea.Items.Add(new ListItem("All", Constants.LongNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlArea, dt, 0, 6);
        }
        else
        {
            ddlArea.Items.Clear();
        }
    }

    protected void LoadPrincipal()
    {
        try
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
           // drpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(drpPrincipal, m_dt, "Company_Id", "Company_Name");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }


    private void LoadSKUDetail()
    {

        DrpSKU.Items.Clear();
        SkuController mContoller = new SkuController();
        DataTable dt = mContoller.SelectSkuInfo(int.Parse(drpPrincipal.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
      //  DrpSKU.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpSKU, dt, 0, 18, false);

    }
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    //protected void btnViewExcel_Click(object sender, EventArgs e)
    //{
    //    ShowReport(1);
    //}

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        LoadArea();
        //LoadMarket();
      //  LoadCustomer();
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        // LoadMarket();
       // LoadCustomer();
    }

    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSKUDetail();
    }

    protected void ShowReport(int type)
    {
        try
        {
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
            RptCustomerController RptCustomerCtl = new RptCustomerController();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

            DateTime parsed_date_fromdate = DateTime.Parse(txtStartDate.Text);
            DateTime parsed_date_todate = DateTime.Parse(txtEndDate.Text);
            string FromDate = parsed_date_fromdate.ToShortDateString();
            string ToDate = parsed_date_todate.ToShortDateString();

            SAMSBusinessLayer.Reports.crpSkuListByLastDispatch CrpReport = new SAMSBusinessLayer.Reports.crpSkuListByLastDispatch();
            DataSet ds = null;
            ds = RptCustomerCtl.GetSKuByLastDispatchDate(DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 
                long.Parse(ddlArea.SelectedValue), Convert.ToInt32(drpDistributor.SelectedValue),
                Convert.ToInt32(drpPrincipal.SelectedValue),Convert.ToInt32(DrpSKU.SelectedValue));
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("Area", ddlArea.SelectedItem.Text);
             CrpReport.SetParameterValue("sku", DrpSKU.SelectedItem.Text);
            CrpReport.SetParameterValue("From_Date", txtStartDate.Text);
            CrpReport.SetParameterValue("To_Date", txtEndDate.Text);
            CrpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);


            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", type);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }



    //protected void DrpMarket_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    LoadCustomer();
    //}
}