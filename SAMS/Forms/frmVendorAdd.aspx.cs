using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.IO;

/// <summary>
/// From to Add, Edit, Delete Geo Heirarical Data
/// </summary>
public partial class Forms_frmVendorAdd : System.Web.UI.Page
{    
    readonly VenderEntryController VendorCtl = new VenderEntryController();
    readonly SKUPriceDetailController PController = new SKUPriceDetailController();

    readonly AccountHeadController mah_Controller = new AccountHeadController();
    /// <summary>
    /// Page_Load Function Populates All Combos and Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadDistributor();
            LoadOpeningInformation();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtOpeningDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }



    #region Opening Balance
    private void LoadPrincipal()
    {
        try
        {
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1, true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void LoadDistributor()
    {

        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);


    }
    private void LoadOpeningInformation()
    {
        DataTable dtOpening = VendorCtl.GetVendorOpening(Constants.LongNullValue, Convert.ToInt64(DrpPrincipal.SelectedValue), Constants.IntNullValue);
        LoadDistributor();
        if (dtOpening.Rows.Count > 0)
        {
            Session.Add("OpeningID", dtOpening.Rows[0]["VENDOR_OPENING_ID"]);
            if (dtOpening.Rows[0]["OPENING_DATE"].ToString() != "")
            {
                txtOpeningDate.Text = Convert.ToDateTime(dtOpening.Rows[0]["OPENING_DATE"]).ToString("dd-MMM-yyyy");
            }
            txtOpeningBalance.Text = dtOpening.Rows[0]["BALANCE"].ToString();
            txtOpeningBalanceRemarks.Text = dtOpening.Rows[0]["REMARKS"].ToString();
            rblOpening.SelectedValue = dtOpening.Rows[0]["TYPE_ID"].ToString();
            
            drpDistributor.SelectedValue = dtOpening.Rows[0]["DISTRIBUTOR_ID"].ToString();
            //drpDistributor.Enabled = false;

            btnSaveOpeningBalance.Text = "Update";
        }
        else
        {
            btnSaveOpeningBalance.Text = "Save";
            drpDistributor.Enabled = true;
            txtOpeningBalance.Text = "";
            txtOpeningBalanceRemarks.Text = "";
        }
    }

    protected void btnOpeningBalance_Click(object sender, EventArgs e)
    {
        DataControl dc = new DataControl();
        LedgerController ledgerCtl = new LedgerController();
        DateTime dtOpening = Constants.DateNullValue;
        if (txtOpeningDate.Text.Length > 0)
        {
            dtOpening = Convert.ToDateTime(txtOpeningDate.Text);
        }
        if (btnSaveOpeningBalance.Text == "Save")
        {
            if (VendorCtl.InsertVendorOpening(Convert.ToInt64(DrpPrincipal.SelectedValue), Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(rblOpening.SelectedValue), dtOpening, Convert.ToDecimal(dc.chkNull_0(txtOpeningBalance.Text)), txtOpeningBalanceRemarks.Text))
            {
                ledgerCtl.InsertVendorOpening(Convert.ToInt32(drpDistributor.SelectedValue), txtOpeningBalanceRemarks.Text, int.Parse(rblOpening.SelectedValue), Convert.ToDateTime(txtOpeningDate.Text), int.Parse(DrpPrincipal.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), Convert.ToDecimal(dc.chkNull_0(txtOpeningBalance.Text)),
                       0, 0, false, 0, "", int.Parse(this.Session["UserId"].ToString()), int.Parse(DrpPrincipal.SelectedValue));

                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Opening Added Successfully.');", true);
                LoadOpeningInformation();
            }
        }
        else
        {
             CustomerDataController cdc = new CustomerDataController();
             DataTable dt = cdc.SelectOpeningCreditVendor(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt64(DrpPrincipal.SelectedValue), Constants.DateNullValue, 1,Constants.IntNullValue);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    ledgerCtl.DeleteOpeningCreditVendor(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpPrincipal.SelectedValue), 25, Constants.DateNullValue, Convert.ToInt64(DrpPrincipal.SelectedValue), Convert.ToInt64(dt.Rows[0]["SALE_INVOICE_MASTER_ID"]), Convert.ToInt32(this.Session["UserId"]), 1);
                }
            }
            if (VendorCtl.UpdateVendorOpening(Convert.ToInt64(Session["OpeningID"]), Convert.ToInt64(DrpPrincipal.SelectedValue), Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(rblOpening.SelectedValue), dtOpening, Convert.ToDecimal(dc.chkNull_0(txtOpeningBalance.Text)), txtOpeningBalanceRemarks.Text))
            {

                ledgerCtl.InsertVendorOpening(Convert.ToInt32(drpDistributor.SelectedValue), txtOpeningBalanceRemarks.Text, int.Parse(rblOpening.SelectedValue), Convert.ToDateTime(txtOpeningDate.Text), int.Parse(DrpPrincipal.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), Convert.ToDecimal(dc.chkNull_0(txtOpeningBalance.Text)),
                        0, 0, false, 0, "", int.Parse(this.Session["UserId"].ToString()), int.Parse(DrpPrincipal.SelectedValue));

                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Opening Updated Successfully.');", true);
                LoadOpeningInformation();
            }
        }
    }

    #endregion

    protected void btnCancelOpeningBalance_Click(object sender, EventArgs e)
    {

    }

    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOpeningInformation();
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOpeningInformation();
    }
}