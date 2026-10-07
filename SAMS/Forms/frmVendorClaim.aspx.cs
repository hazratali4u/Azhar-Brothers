using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmVendorClaim : System.Web.UI.Page
{
    DataTable ClaimedSKU;
    private static int RowId;
    private static int URowId;
    private static string VoucherNo;
    LedgerController LController = new LedgerController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadClaimType();
            LoadAccountHead();

            CreatTableValue();

            LoadVendor();
            LoadGrid();

            btnAddNew.Attributes.Add("onclick", "return ValidateValueForm();");
            ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
            lblRowId.Text = "-1";
        }
    }

    private void LoadDistributor()
    {
        var dController = new DistributorController();
        var dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }

    private void LoadClaimType()
    {
        RbdClaimType.Items.Add(new ListItem("Credit Claim", Constants.CreditClaim.ToString()));
        RbdClaimType.Items.Add(new ListItem("Debit Claim", Constants.DebitClaim.ToString()));
        RbdClaimType.SelectedIndex = 0;
    }

    private void LoadAccountHead()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 4, true);

    }

    private void CreatTableValue()
    {
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        Session.Add("dtVoucher", dtVoucher);
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();

    }

    private void LoadVendor()
    {
        if (drpDistributor.Items.Count > 0)
        {
            var PController = new SKUPriceDetailController();
            DataTable dtVendor = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));


            clsWebFormUtil.FillDropDownList(DrpVendor, dtVendor, 0, 1, true);

            //if (dtVendor.Rows.Count > 0)
            //{
            //    DataRow[] foundRows = dtVendor.Select("Company_Id  = '" + DrpVendor.SelectedValue + "'");
            //    if (foundRows.Length > 0)
            //    {
            //        hfVendorType.Value = Convert.ToString(foundRows[0]["vendorType"]);
            //    }
            //}

            Session.Add("dtVendor", dtVendor);
        }
        else
        {
            DrpVendor.Items.Clear();
        }
    }

    private void LoadGrid()
    {
        if (drpDistributor.Items.Count > 0)
        {
            var lController = new LedgerController();
            var dt = lController.SelectClaimDetailVendor(int.Parse(drpDistributor.SelectedValue), int.Parse(RbdClaimType.SelectedValue),
                DateTime.Parse(Session["CurrentWorkDate"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()),1);
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
    }


    private void ClearGrd_Order()
    {
        txtRemarks.Text = "";
        txtAmount.Text = "";
        btnAddNew.Text = "Save";

    }

    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LedgerController LController = new LedgerController();
        DataControl dc = new DataControl();

        //uPDATE  VENDOR lEDGER,gl MASTER DETAIL

        LController.UpdateVendorLedger(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[3].Text)),
        long.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[6].Text)), Convert.ToString(GrdOrder.Rows[e.RowIndex].Cells[9].Text), decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[7].Text)), 2);

        ClearGrd_Order();
        LoadGrid();

    }
    private void EnableDisable(bool flag)
    {
        if (flag == true)
        {
            drpDistributor.Enabled = false;
            
            DrpVendor.Enabled = false;
        }
        else
        {
            drpDistributor.Enabled = true;
            DrpVendor.Enabled = true;
        }
    }
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        URowId = Convert.ToInt32(GrdOrder.Rows[e.NewEditIndex].Cells[0].Text);
        
        LoadVendor();
        DrpVendor.SelectedValue = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[2].Text);
        DrpAccountHead.SelectedValue = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[1].Text);
        txtAmount.Text = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[7].Text);
        txtRemarks.Text = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[8].Text.Replace("&nbsp;", ""));

        VoucherNo =GrdOrder.Rows[e.NewEditIndex].Cells[9].Text;
        EnableDisable(true);

        btnAddNew.Text = "Update";
    }

    private void InsertGL()
    {
        SAMSCommon.Classes.Configuration.GetAccountHead();

        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));

        if (RbdClaimType.SelectedValue == Constants.CreditClaim.ToString())
        {
            DataRow dr = dtVoucher.NewRow();
            dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountHead.SelectedValue);
            dr["REMARKS"] = DrpAccountHead.SelectedItem.Text + " Claim to " + DrpVendor.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(txtAmount.Text);
            dr["CREDIT"] = 0;
            dr["Principal_Id"] = 0;
            dtVoucher.Rows.Add(dr);


            DataRow dr1 = dtVoucher.NewRow();
          
            dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
            dr1["ACCOUNT_HEAD_ID"] = Configuration.LocalPartsVendor2W;
            //if (hfVendorType.Value == "False")
            //{
            //    dr1["ACCOUNT_HEAD_ID"] = Configuration.LocalPartsVendor2W;
            //}
            //else if (hfVendorType.Value == "True")
            //{
            //    dr1["ACCOUNT_HEAD_ID"] = Configuration.ImportedPartsVendor2W;
            //}

            dr1["REMARKS"] = DrpAccountHead.SelectedItem.Text + " Claim to " + DrpVendor.SelectedItem.Text;

            dr1["DEBIT"] = 0;
            dr1["CREDIT"] = decimal.Parse(txtAmount.Text); 
            dr1["Principal_Id"] = 0;
            dtVoucher.Rows.Add(dr1);

        }
        else
        {
            ///Credit Side Entry
            DataRow dr1 = dtVoucher.NewRow();
            dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
            dr1["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountHead.SelectedValue);
            dr1["DEBIT"] = 0;
            dr1["CREDIT"] = decimal.Parse(txtAmount.Text);
            dr1["Principal_Id"] = 0;
            dr1["REMARKS"] = DrpAccountHead.SelectedItem.Text + " Claim to " + DrpVendor.SelectedItem.Text;
            dtVoucher.Rows.Add(dr1);

            DataRow dr = dtVoucher.NewRow();

            dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
            dr1["ACCOUNT_HEAD_ID"] = Configuration.LocalPartsVendor2W;
            //if (hfVendorType.Value == "False")
            //{
            //    dr["ACCOUNT_HEAD_ID"] = Configuration.LocalPartsVendor2W;
            //}
            //else if (hfVendorType.Value == "True")
            //{
            //    dr["ACCOUNT_HEAD_ID"] = Configuration.ImportedPartsVendor2W;
            //}
            dr["DEBIT"] = decimal.Parse(txtAmount.Text);
            dr["CREDIT"] = 0; 
            dr["Principal_Id"] = 0;
            dr["REMARKS"] = DrpAccountHead.SelectedItem.Text + " Claim to " + DrpVendor.SelectedItem.Text;
            dtVoucher.Rows.Add(dr);

        }

        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));

        LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), int.Parse(Session["VoucherNo"].ToString()), MaxDocumentId, Constants.Journal_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]), int.Parse(RbdClaimType.SelectedValue), Session["VoucherNo"].ToString(), "Defualt " + DrpAccountHead.SelectedItem.Text + " Voucher", Convert.ToDateTime(Session["CurrentWorkDate"]), null, dtVoucher, Convert.ToInt32(Session["UserID"]), "-1", Constants.DateNullValue,true);
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnAddNew.Text == "Save")
            {
                string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()), 1);
                Session.Add("VoucherNo", MaxDocumentId);

                if (RbdClaimType.SelectedValue == Constants.CreditClaim.ToString())
                {

                    LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(MaxDocumentId), int.Parse(DrpAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtAmount.Text),
                                   DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()), Constants.IntNullValue,
                                   null, int.Parse(Session["UserId"].ToString()), -1, "0", Constants.CreditClaim, null, null, Constants.DateNullValue);

                    InsertGL();
                }
                else
                {


                    LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(MaxDocumentId), int.Parse(DrpAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), 0,
                                   DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()),  Constants.IntNullValue,
                                   null, int.Parse(Session["UserId"].ToString()), -1, "0", Constants.DebitClaim, null, null, Constants.DateNullValue);
                    InsertGL();
                }
            }
            else
            {
                DataControl dc = new DataControl();


                //uPDATE  VENDOR lEDGER,gl MASTER DETAIL
                LController.UpdateVendorLedgerClaim(int.Parse(drpDistributor.SelectedValue),URowId, VoucherNo, long.Parse(DrpAccountHead.SelectedValue), decimal.Parse(dc.chkNull_0(txtAmount.Text)), txtRemarks.Text, int.Parse(RbdClaimType.SelectedValue));

              

                EnableDisable(false);

            }


            ClearGrd_Order();
            LoadGrid();
        }
        catch (Exception ex)
        {
 
        }
    }

    #region Index/Change

    protected void DrpVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();

        if (Session["dtVendor"] != null)
        {
            DataTable dtVendor = (DataTable)Session["dtVendor"];

            //    DataRow[] foundRows = dtVendor.Select("VENDOR_ID  = '" + DrpVendor.SelectedValue + "'");
            //    if (foundRows.Length > 0)
            //    {
            //      //  hfVendorType.Value = Convert.ToString(foundRows[0]["vendorType"]);
            //   // hfVendorType.Value = "false";
            //}
        }
    }

    protected void RbdClaimType_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        LoadGrid();

    }
   
    #endregion


}