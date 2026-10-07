using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmChequeEntryVendor : System.Web.UI.Page
{
    readonly DataControl dc = new DataControl();
    readonly LedgerController LController = new LedgerController();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DrpStatus.Items.Add(new ListItem("Cheque Issue" , "527"));
            DrpStatus.Items.Add(new ListItem("Cheque Clear" , "529"));
            DrpStatus.Items.Add(new ListItem("Cheque Bounce", "530"));
            DrpStatus.Items.Add(new ListItem("Cheque Cancel", "560"));
            LoadAccountHead();
            LoadDistributor();
            LoadAccountDetail();
            LoadData();
            SelectCreditInvoice();
            LoadReceviedCheque();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    
    private void toggleControls(string pAccountType)
    {
        if (pAccountType == "21")
        {
            lblChequeNo.Visible = false;
            txtChequeNo.Visible = false;
            lblChequeDate.Visible = false;
            txtStartDate.Visible = false;
            ibtnStartDate.Visible = false;
            DrpStatus.Visible = false;
            lblStatus.Visible = false;
            GrdCO.Visible = true;
            GrdCheque.Visible = false;
            lblSlipno.Visible = false;
            txtSlipno.Visible = false;

        }
        else if (pAccountType == "33")
        {
            lblChequeNo.Visible = false;
            txtChequeNo.Visible = false;
            lblChequeDate.Visible = true;
            txtStartDate.Visible = true;
            ibtnStartDate.Visible = true;
            DrpStatus.Visible = false;
            lblStatus.Visible = false;
            lblChequeDate.Text = "Transfer Date";
            GrdCO.Visible = true;
            GrdCheque.Visible = false;
            lblSlipno.Visible = true;
            txtSlipno.Visible = true;
            
        }
        else
        {

            lblChequeNo.Visible = true;
            txtChequeNo.Visible = true;
            lblChequeDate.Visible = true;
            txtStartDate.Visible = true;
            ibtnStartDate.Visible = true;
            DrpStatus.Visible = true;
            lblStatus.Visible = true;
            lblChequeDate.Text = "Cheque Date";
            GrdCO.Visible = false;
            GrdCheque.Visible = true;
            lblSlipno.Visible = false;
            txtSlipno.Visible = false;

        }
    }
  
    #region Load

    private void LoadPaymentRecieved()
    {
        ChequeEntryController CController = new ChequeEntryController();
        LedgerController LController = new LedgerController();
        if (drpDistributor.Items.Count > 0)
        {
            DataSet dsReceived = LController.SelectBankCashTransction(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, 21,
                DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            DataTable dtRealized = dsReceived.Tables[2];
            if (dtRealized.Rows.Count > 0)
            {
                lblAmount.Text = string.Format("{0:0,0.00}", Convert.ToDecimal(dtRealized.Rows[0][0].ToString()));
            }
        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }

    private void LoadData()
    {
        hfVendorType.Value = "";
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        SKUPriceDetailController PController = new SKUPriceDetailController();
        //if (DrpAccountType.SelectedIndex != 0)
        //{
             DataTable dtVendor = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
             clsWebFormUtil.FillDropDownList(DrpVendor, dtVendor, 0, 1, true);
        //}
        //else
        //{
        //    if (drpDistributor.Items.Count > 0)
        //    {
        //        LedgerController LedgerCtl = new LedgerController();
        //        DataTable dtCredit = LedgerCtl.SelectCreditPendingInvoice2(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        //        clsWebFormUtil.FillDropDownList(DrpVendor, dtCredit, 0, 1, true);

        //    }
        //}
    }

    private void LoadReceviedCheque()
    {
        ChequeEntryController CController = new ChequeEntryController();
        GrdCheque.DataSource =null;
        GrdCheque.DataBind();

        GrdCO.DataSource = null;
        GrdCO.DataBind();

        decimal chqAmount = 0;
        if (DrpAccountType.SelectedValue == "21" || DrpAccountType.SelectedValue == "33")
        {
            if (drpDistributor.Items.Count > 0)
            {
                DataTable dt = LController.VendorBankCashTransction(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(DrpAccountType.SelectedValue),
                           DateTime.Parse(Session["CurrentWorkDate"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                Session.Add("dt", dt);
                GrdCO.DataSource = dt;
                GrdCO.DataBind();
                if (dt != null)
                {
                    foreach (DataRow gvr in dt.Rows)
                    {
                        chqAmount += Convert.ToDecimal(gvr["CHEQUE_AMOUNT"]);
                    }
                    lblTotalAmount.Text = string.Format("{0:0.00}", chqAmount);
                }
            }
        }
        else
        {
            if (DrpStatus.SelectedValue.ToString() != Constants.Cheque_Clear.ToString())
            {
                DataTable dt = null;
                if (DrpVendor.Items.Count > 0)
                {
                    dt = CController.SelectVendorChequeEntry(int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpVendor.SelectedValue), DrpAccountType.SelectedIndex);
                    Session.Add("dt", dt);                  
                    GrdCheque.DataSource = dt;
                    GrdCheque.DataBind();                   
                }
                if (dt != null)
                {
                    foreach (DataRow gvr in dt.Rows)
                    {
                        chqAmount += Convert.ToDecimal(gvr["CHEQUE_AMOUNT"]);
                    }
                }
                    lblTotalAmount.Text = string.Format("{0:0.00}", chqAmount);
                    foreach (GridViewRow gvr in GrdCredit.Rows)
                    {
                        CheckBox row = gvr.FindControl("ChbIsAssigned") as CheckBox;
                        if (row.Enabled)
                        {
                            break;                            
                        }
                        else
                        {
                            row.Enabled = true;
                        }
                    }
            }
            else
            {
                ///Load Cash Realization Detail
                if (Convert.ToInt32(DrpStatus.SelectedValue) < 530)
                {
                    DataSet ds = CController.SelectVendorChequeEntry(int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), DrpAccountType.SelectedIndex);
                    if (ds != null)
                    {
                        DataTable dt2 = ds.Tables[1];
                        if (dt2.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt2.Rows)
                            {
                                chqAmount += Convert.ToDecimal(dr[2].ToString());
                            }
                            lblTotalAmount.Text = string.Format("{0:0.00}", chqAmount);
                        }
                    }
                    else
                    {
                        lblTotalAmount.Text = "0";
                    }
                }
                ////////////////////////////////////////////////
                DataTable dt = CController.SelectVendorChequeEntry(int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, DrpAccountType.SelectedIndex);
                Session.Add("dt", dt);
                GrdCheque.DataSource = dt;
                GrdCheque.DataBind();
                foreach (GridViewRow gvr in GrdCredit.Rows)
                {
                    CheckBox row = gvr.FindControl("ChbIsAssigned") as CheckBox;
                    row.Checked = false;
                    row.Enabled = false;
                    
                }                                
            }
        }
    }

    private void checkDuplication()
    {
        DataTable dt = (DataTable)Session["dt"];
        if (dt != null)
        {
            DataRow[] foundRows = dt.Select("VENDOR_ID = '" + DrpVendor.SelectedValue + "' and CHEQUE_NO='" + txtChequeNo.Text + "'");

            if (foundRows.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Cheque No Already exist against this vendor!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Data Not found !');", true);

        }
    }

    private void SelectCreditInvoice()
    {
        LedgerController CDC = new LedgerController();
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        if (DrpVendor.Items.Count > 0 && !cbAdvance.Checked)
        {
            DataTable dtCredit = CDC.SelectCreditPendingInvoice2(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpVendor.SelectedValue.ToString()), 0);
            GrdCredit.DataSource = dtCredit;
            GrdCredit.DataBind();
        }
    }

    private void LoadAccountHead()
    {
        SAMSCommon.Classes.Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();

        if (DrpAccountType.SelectedValue == "21")
        {
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.CashDefaultType));
            clsWebFormUtil.FillDropDownList(DrpBankAccount, dt, 0, 4, true);
        }
        else
        {
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommon.Classes.Configuration.BankDefaultType));
            clsWebFormUtil.FillDropDownList(DrpBankAccount, dt, 0, 4, true);
        }
    }

    private void LoadAccountDetail()
    {
        AccountHeadController mAccountController = new AccountHeadController();

        DataTable dtHead = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
            clsWebFormUtil.FillDropDownList(this.ddlAccountHead, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_NAME", true);
        
    }

    #endregion

    private void ChequeRealization(string PayeeName)
    {
        DateTime ChequeDate=Constants.DateNullValue;
        try
        {
            if (DrpAccountType.SelectedValue == "18" || DrpAccountType.SelectedValue == "33")
            {
                if (txtStartDate.Text.Length == 10)
                {
                    ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text));

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Correct Cheque Date Pattern is DD/MM/YYYY.');", true);
            return;
        }
        string slipNo = "";
        LedgerController LController = new LedgerController();
        string MaxDocumentId = "";
        int VoucherType = Constants.Expanse_Voucher;
      
        decimal OfferAmount = decimal.Parse(txtAmount.Text);
        decimal realizeAmount = 0;
       
        string remarks="";

        
        if (DrpAccountType.SelectedValue == "18")//Cheque
        {
            MaxDocumentId = LController.SelectLedgerMaxDocumentId(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()), 1);
            if (cbAdvance.Checked)
            {
                remarks = "Advance Chq# " + txtChequeNo.Text + ", " + DrpBankAccount.SelectedItem.Text + ", " + txtRemarks.Text;
            }
            else
            {
                remarks = "Chq# " + txtChequeNo.Text + ", " + DrpBankAccount.SelectedItem.Text + ", " + txtRemarks.Text;
            }
        }
        else if (DrpAccountType.SelectedValue == "33")//Online
        {
            MaxDocumentId = LController.SelectLedgerMaxDocumentId(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()), 1);
            slipNo = txtSlipno.Text;
            if (cbAdvance.Checked)
            {
                remarks = "Cash Advance Online Transfer " + DrpAccountType.SelectedItem.Text + ", " + txtRemarks.Text;
            }
            else
            {
                remarks = "Online Transfer " + DrpAccountType.SelectedItem.Text + ", " + txtRemarks.Text;
            }
        }
        else if (DrpAccountType.SelectedValue == "21")//Cash
        {

            VoucherType = Constants.Cash_Voucher;
            MaxDocumentId = LController.SelectLedgerMaxDocumentId(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()), 1);
            if (cbAdvance.Checked)
            {
                remarks = "Cash Advance " + txtRemarks.Text;
            }
            else
            {
                remarks = "Cash " + txtRemarks.Text;
            }
        }
            Session.Add("VoucherNo", MaxDocumentId);

            foreach (GridViewRow dr in GrdCredit.Rows)
            {
                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                if (chRelized.Checked == true)
                {

                    string manualNo = dr.Cells[5].Text;

                    if (Convert.ToString(dr.Cells[5].Text) == "opng")
                    {
                        manualNo = "opng";
                    }
                    if (decimal.Parse(dr.Cells[4].Text) >= OfferAmount)
                    {
                        realizeAmount += OfferAmount;

                        LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), int.Parse(Configuration.PayableAccount), int.Parse(drpDistributor.SelectedValue.ToString()), 0, OfferAmount,
                        DateTime.Parse(Session["CurrentWorkDate"].ToString()), remarks, DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()), Constants.Document_PrincipalInvoice,
                        txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]), manualNo,int.Parse(DrpAccountType.SelectedValue), slipNo, PayeeName, ChequeDate);

                        LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount, 0,
                         DateTime.Parse(Session["CurrentWorkDate"].ToString()), remarks, DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()),  Constants.Document_PrincipalInvoice,
                         txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]), manualNo, int.Parse(DrpAccountType.SelectedValue), slipNo, PayeeName, ChequeDate);


                        OfferAmount = decimal.Parse(dr.Cells[4].Text) - OfferAmount;
                        LController.UpdatePurchaseMaster(Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]),int.Parse(drpDistributor.SelectedValue), OfferAmount);
                        break;
                    }
                    else if (decimal.Parse(dr.Cells[4].Text) <= OfferAmount)
                    {
                        realizeAmount += decimal.Parse(dr.Cells[4].Text);
                        LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), int.Parse(Configuration.PayableAccount), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(dr.Cells[4].Text),
                        DateTime.Parse(Session["CurrentWorkDate"].ToString()), remarks, DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()), Constants.Document_PrincipalInvoice,
                         txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]), manualNo, int.Parse(DrpAccountType.SelectedValue), slipNo, "Group Payment", ChequeDate);

                        LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(dr.Cells[4].Text), 0,
                        DateTime.Parse(Session["CurrentWorkDate"].ToString()), remarks, DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()), Constants.Document_PrincipalInvoice,
                        txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]), manualNo, int.Parse(DrpAccountType.SelectedValue), slipNo, "Group Payment", ChequeDate);

                        OfferAmount = OfferAmount - decimal.Parse(dr.Cells[4].Text);
                        LController.UpdatePurchaseMaster(long.Parse(dr.Cells[1].Text), int.Parse(drpDistributor.SelectedValue), 0);
                    }
                }
            }

        //Discount Entry
            decimal discount = 0;

            if (Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)) > 0)
            {
                discount = Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text));

                if (ChkIsDiscount.Checked)
                {
                    discount = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)) * (discount / 100);
                }

                LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), int.Parse(Configuration.InvoiceDiscountVendors), int.Parse(drpDistributor.SelectedValue.ToString()), 0, discount,
                          DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Discount on Payment", DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()),  Constants.Document_PrincipalInvoice,
                          txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), 0, "0", int.Parse(DrpAccountType.SelectedValue), "Discount", PayeeName, ChequeDate);

                }

            //Tax Entry
            decimal tax = 0;

            if (Convert.ToDecimal(dc.chkNull_0(txtTax.Text)) > 0)
            {
                tax = (Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)) - discount) * (Convert.ToDecimal(txtTax.Text) / 100);

                LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), int.Parse(ddlAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()), 0, tax,
                          DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Tax on Payment (" + ddlAccountHead.SelectedItem.Text + ")", DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()),  Constants.Document_PrincipalInvoice,
                          txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), 0, "0", int.Parse(DrpAccountType.SelectedValue), "Tax", PayeeName, ChequeDate);
   
            }

            //Advance Entry
            if (realizeAmount < decimal.Parse(txtAmount.Text))
            {

                LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), int.Parse(Configuration.PayableAccount), int.Parse(drpDistributor.SelectedValue.ToString()), 0, OfferAmount,
                                      DateTime.Parse(Session["CurrentWorkDate"].ToString()), remarks + " (Advance)", DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()),  Constants.Document_PrincipalInvoice,
                                      txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), 0, "0", int.Parse(DrpAccountType.SelectedValue), slipNo, "Group Payment", ChequeDate);

                LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount, 0,
                                      DateTime.Parse(Session["CurrentWorkDate"].ToString()), remarks + " (Advance)", DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()),  Constants.Document_PrincipalInvoice,
                                      txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), 0, "0", int.Parse(DrpAccountType.SelectedValue), slipNo, "Group Payment", ChequeDate);

            }
    }

    protected void GrdCheque_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ChequeEntryController CController = new ChequeEntryController();
        CController.DeleteChequeEntry(long.Parse(GrdCheque.Rows[e.RowIndex].Cells[0].Text), 1);
        LoadReceviedCheque();
    }

    protected void GrdCheque_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ChequeEntryController Ccontroller = new ChequeEntryController();
            HFChqueProcessId.Value = GrdCheque.Rows[e.NewEditIndex].Cells[0].Text;
           
            //LoadData();

            DrpVendor.SelectedValue = GrdCheque.Rows[e.NewEditIndex].Cells[1].Text;
            txtChequeNo.Text = GrdCheque.Rows[e.NewEditIndex].Cells[3].Text;
            txtStartDate.Text = GrdCheque.Rows[e.NewEditIndex].Cells[4].Text;
            txtReceivedDate.Text = GrdCheque.Rows[e.NewEditIndex].Cells[5].Text;
            txtAmount.Text = GrdCheque.Rows[e.NewEditIndex].Cells[6].Text;
            txtRemarks.Text = GrdCheque.Rows[e.NewEditIndex].Cells[7].Text.Replace("&nbsp;","");
            
            DrpBankAccount.SelectedValue = GrdCheque.Rows[e.NewEditIndex].Cells[9].Text;
            
            ChkIsDiscount.Checked =Convert.ToBoolean(GrdCheque.Rows[e.NewEditIndex].Cells[11].Text);

            txtDiscount.Text = GrdCheque.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;","");

            txtTax.Text = GrdCheque.Rows[e.NewEditIndex].Cells[12].Text.Replace("&nbsp;", ""); ;
           // ddlAccountHead.SelectedValue = GrdCheque.Rows[e.NewEditIndex].Cells[13].Text;

            btnSave.Text = "Update";

            SelectCreditInvoice();

            if (DrpAccountType.SelectedValue == "18")
            {
                DataTable dt = Ccontroller.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 0);

                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    foreach (DataRow dbr in dt.Rows)
                    {

                        if (Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]) == Convert.ToInt64(dbr["SALE_INVOICE_ID"]))
                        {
                            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                            chRelized.Checked = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Invoice not found for selected cheque');", true);
        }
    }

    #region Sel/Index Change
    
    protected void DrpVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectCreditInvoice();
        LoadReceviedCheque();
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectCreditInvoice();
        LoadReceviedCheque();
    }
    
    protected void DrpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            LoadReceviedCheque();
        }
    }
    protected void DrpAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountHead();
        toggleControls(DrpAccountType.SelectedValue);
        SelectCreditInvoice();
        LoadReceviedCheque();
    }
  

    #endregion

    #region Click

    private bool InvalidDate()
    {
        if (DrpAccountType.SelectedValue == "18")
        {
            if (DrpStatus.SelectedValue == "527")
            {
                if (Convert.ToDateTime(ConvertDate.British_To_American(txtStartDate.Text)) < Convert.ToDateTime(Session["CurrentWorkDate"]))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invalid Cheque Date');", true);
                 
                    return true;// for time being , remove this check
                }
            }
            
        }
        return true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (InvalidDate())
            {
                ChequeEntryController CController = new ChequeEntryController();
                DateTime ChequeDate;
                try
                {
                    if (txtStartDate.Text.Length == 10)
                    {
                        ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text));
                    }
                    else
                    {
                        ChequeDate = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Correct Cheque Date Pattern is DD/MM/YYYY.');", true);
                    return;
                }
                int InvoiceCount = Constants.IntNullValue;
                string PayeeName = "";
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                    if (chRelized.Checked == true)
                    {
                        InvoiceCount++;
                        break;
                    }
                }
                if (InvoiceCount == Constants.IntNullValue && !cbAdvance.Checked) 
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Must Select Invoice');", true);
                    return;
                }
                if (btnSave.Text == "Save")
                {
                    int Count = 0;
                    foreach (GridViewRow dr in GrdCredit.Rows)
                    {
                        CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                        if (chRelized.Checked == true)
                        {
                            Count++;
                            if (Count > 1)
                            {
                                PayeeName = "Group Payment"; //in case multiple invoices selection
                                break;
                            }
                        }
                    }

                    if (DrpAccountType.SelectedIndex != 0)//on Cash Realization check Invoice Selection
                    {
                        if (InvoiceCount == Constants.IntNullValue && DrpAccountType.SelectedIndex != 0)
                        {
                            CashAdvance();
                            InsertGL2();
                            Session.Remove("VoucherNo");
                        }
                        else if (InvoiceCount != Constants.IntNullValue && DrpAccountType.SelectedIndex != 0)
                        {
                            ChequeRealization(PayeeName);// used as All cash, online, cheque
                            InsertGL2();
                            Session.Remove("VoucherNo");
                            SelectCreditInvoice();
                        }
                    }
                    else if (DrpAccountType.SelectedIndex == 0)//on Cash Realization check Invoice Selection
                    {
                        checkDuplication();
                        if (DrpStatus.SelectedIndex == 0)
                        {
                            short chkDiscount = 0;
                            if (ChkIsDiscount.Checked)
                            {
                                chkDiscount = 1;
                            }
                            
                            HFChqueProcessId.Value = CController.InsertChequeEntry(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpVendor.SelectedValue.ToString()), 0, txtChequeNo.Text, txtBankName.Text, ChequeDate,
                                DateTime.Parse(Session["CurrentWorkDate"].ToString()), Constants.DateNullValue, Constants.DateNullValue, Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)), int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Now, DrpAccountType.SelectedIndex, "", txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()), 1
                                , Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), chkDiscount, Convert.ToDecimal(dc.chkNull_0(txtTax.Text)), Convert.ToInt32(ddlAccountHead.SelectedValue));


                            foreach (GridViewRow dr in GrdCredit.Rows)
                            {
                                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                                if (chRelized.Checked == true)
                                {
                                    CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]));
                                }
                            }
                            PrintChequeVoucher(HFChqueProcessId.Value);
                        }
                    }

                    if (DrpAccountType.SelectedValue == Constants.ChequePayment.ToString() && int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Clear && cbAdvance.Checked)
                    {
                        short chkDiscount = 0;
                        if (ChkIsDiscount.Checked)
                        {
                            chkDiscount = 1;
                        }
                        HFChqueProcessId.Value = CController.InsertChequeEntry(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpVendor.SelectedValue.ToString()), 0, txtChequeNo.Text, txtBankName.Text, ChequeDate,
                                DateTime.Parse(Session["CurrentWorkDate"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)), int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Now, DrpAccountType.SelectedIndex, "", txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()), 1
                                , Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), chkDiscount, Convert.ToDecimal(dc.chkNull_0(txtTax.Text)), Convert.ToInt32(ddlAccountHead.SelectedValue));
                        ChequeRealization(PayeeName);
                        InsertGL();
                        Session.Remove("VoucherNo");                        
                    }
                }
                else
                {
                    if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Pending && txtReceivedDate.Text == DateTime.Parse(Session["CurrentWorkDate"].ToString()).ToString("dd/MM/yyyy"))
                    {
                        short chkDiscount = 0;
                        if (ChkIsDiscount.Checked)
                        {
                            chkDiscount = 1;
                        }

                        CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpVendor.SelectedValue.ToString()), 0, txtChequeNo.Text, txtBankName.Text, ChequeDate, DateTime.Parse(Session["CurrentWorkDate"].ToString()), Constants.DateNullValue, Constants.DateNullValue,
                           Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)), int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, "", DrpAccountType.SelectedIndex, txtRemarks.Text, int.Parse(DrpBankAccount.SelectedValue.ToString())
                           , Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), chkDiscount, Convert.ToDecimal(dc.chkNull_0(txtTax.Text)), Convert.ToInt32(ddlAccountHead.SelectedValue));

                        CController.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 1);

                        foreach (GridViewRow dr in GrdCredit.Rows)
                        {
                            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                            if (chRelized.Checked == true)
                            {
                                CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]));
                            }
                        }
                    }                   
                    else if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Bons || int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Cancel)
                    {
                        CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, null, null, Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue, DateTime.Parse(Session["CurrentWorkDate"].ToString()),
                                                         Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, "", DrpAccountType.SelectedIndex, txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString())
                                                         , Constants.DecimalNullValue, Constants.ShortNullValue, Constants.DecimalNullValue, Constants.IntNullValue);

                    }

                    else if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Clear)
                    {

                        CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, null, null, Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue, DateTime.Parse(Session["CurrentWorkDate"].ToString()),
                                                         Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, "", DrpAccountType.SelectedIndex, txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString())
                                                         , Constants.DecimalNullValue, Constants.ShortNullValue, Constants.DecimalNullValue, Constants.IntNullValue);

                        CController.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 1);

                        foreach (GridViewRow dr in GrdCredit.Rows)
                        {
                            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                            if (chRelized.Checked == true)
                            {
                                CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["PURCHASE_MASTER_ID"]));
                            }
                        }
                        ChequeRealization(PayeeName);
                        InsertGL();
                        Session.Remove("VoucherNo");
                        //LoadData();
                        SelectCreditInvoice();
                    }
                }

                ClearAll();
                LoadReceviedCheque();
                LoadPaymentRecieved();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dt"];
        if (DrpAccountType.SelectedValue == "18")
        {
            switch (ddSearchType.SelectedIndex)
            {
                case 1:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 2:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 3:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 4:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    dt.DefaultView.RowFilter = "CHEQUE_NO" + " like '%" + "" + "%'";
                    break;
            }
            GrdCheque.DataSource = dt.DefaultView;
            GrdCheque.DataBind();
        }
        else
        {
            switch (ddSearchType.SelectedIndex)
            {
                case 1:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                case 2:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                    break;
                default:
                    dt.DefaultView.RowFilter = "CHEQUE_NO" + " like '%" + "" + "%'";
                    break;
            }
            GrdCO.DataSource = dt.DefaultView;
            GrdCO.DataBind();
        }
    }

    #endregion

    private void CashAdvance()
    {
        string remarks = "Cash " + txtRemarks.Text;
        if(cbAdvance.Checked)
        {
            remarks = "Cash Advance " + txtRemarks.Text;
        }
        DateTime chqDate = Constants.DateNullValue;

        int VoucherType = Constants.CashPayment_Voucher;
        string slipNo = "";
        if (DrpAccountType.SelectedValue == "33")
        {
            chqDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text)); //Convert.ToDateTime(txtStartDate.Text);
            VoucherType = Constants.Expanse_Voucher;
            remarks = "Online Transfer " + DrpBankAccount.SelectedItem.Text  + ", " + txtRemarks.Text;
            slipNo = txtSlipno.Text;
            if (cbAdvance.Checked)
            {
                remarks = "Online Transfer Cash Advance " + DrpBankAccount.SelectedItem.Text + ", " + txtRemarks.Text;
            }
        }
        string MaxDocumentID = LController.SelectLedgerMaxDocumentId(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()), 1);
        Session.Add("VoucherNo", MaxDocumentID);
        decimal discount = 0;
        decimal tax=0;
        if (Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)) > 0)
        {
            discount = Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text));
            if (ChkIsDiscount.Checked)
            {
                discount = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)) * (discount / 100);
            }
            LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentID), int.Parse(Configuration.InvoiceDiscountVendors), int.Parse(drpDistributor.SelectedValue.ToString()), 0, discount,
                      DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Discount on Cash Advance Payment", DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()),  Constants.Document_PrincipalInvoice,
                      txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), 0, "0", int.Parse(DrpAccountType.SelectedValue), "Discount", "", chqDate);
        }
        if (Convert.ToDecimal(dc.chkNull_0(txtTax.Text)) > 0)
        {
             tax = (Convert.ToDecimal(dc.chkNull_0(txtAmount.Text))-discount) * (Convert.ToDecimal(txtTax.Text) / 100);
            LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentID), int.Parse(ddlAccountHead.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()), 0, tax,
                      DateTime.Parse(Session["CurrentWorkDate"].ToString()), "Tax on Cash Advance Payment (" + ddlAccountHead.SelectedItem.Text + ")", DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()),  Constants.Document_PrincipalInvoice,
                      txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), 0, "0", int.Parse(DrpAccountType.SelectedValue), "Tax", "", chqDate);
        }

        LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentID), int.Parse(Configuration.PayableAccount), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(dc.chkNull_0(txtAmount.Text)),
                       DateTime.Parse(Session["CurrentWorkDate"].ToString()), remarks, DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()),  Constants.Document_PrincipalInvoice,
                       txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), 0, "0",int.Parse(DrpAccountType.SelectedValue), slipNo, "", chqDate);

        LController.PostingPrinvipalInvoiceAccount(VoucherType, long.Parse(MaxDocumentID), int.Parse(DrpBankAccount.SelectedValue), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(dc.chkNull_0(txtAmount.Text)), 0,
                       DateTime.Parse(Session["CurrentWorkDate"].ToString()), remarks, DateTime.Now, int.Parse(DrpVendor.SelectedValue.ToString()),  Constants.Document_PrincipalInvoice,
                       txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), 0, "0", int.Parse(DrpAccountType.SelectedValue), slipNo, "", chqDate);

    }

    private void InsertGL()
    {

        Configuration.GetAccountHead();
        DataTable dtVoucher = new DataTable();

        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));

        decimal discount = 0;
        if (Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)) > 0)
        {
            discount = Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text));

            if (ChkIsDiscount.Checked)
            {
                discount = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)) * (discount / 100);
            }
            DataRow drDiscount = dtVoucher.NewRow();
            drDiscount["ACCOUNT_HEAD_ID"] = Convert.ToInt64(Configuration.InvoiceDiscountVendors);
            drDiscount["REMARKS"] = "Discount on Payment";
            drDiscount["DEBIT"] = 0;
            drDiscount["CREDIT"] = discount;
            drDiscount["Principal_Id"] = 0;
            dtVoucher.Rows.Add(drDiscount);
        }

        decimal tax = 0;
        if (Convert.ToDecimal(dc.chkNull_0(txtTax.Text)) > 0)
        {
            tax = (Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)) - discount) * (Convert.ToDecimal(txtTax.Text) / 100);

            DataRow drTax = dtVoucher.NewRow();
            drTax["ACCOUNT_HEAD_ID"] = Convert.ToInt64(ddlAccountHead.SelectedValue);
            drTax["REMARKS"] = "Tax on Payment " + ddlAccountHead.SelectedItem.Text;
            drTax["DEBIT"] = 0;
            drTax["CREDIT"] = tax;
            drTax["Principal_Id"] = 0;
            dtVoucher.Rows.Add(drTax);
        }


        DataRow dr = dtVoucher.NewRow();
        dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpBankAccount.SelectedValue);
        dr["REMARKS"] = DrpBankAccount.SelectedItem.Text + " Paid to " + DrpVendor.SelectedItem.Text;
        dr["DEBIT"] = 0;
        dr["CREDIT"] = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)) - tax - discount;
        dr["Principal_Id"] = 0;
        dtVoucher.Rows.Add(dr);

        //Debit Side Entry

        DataRow dr1 = dtVoucher.NewRow();

        dr1["ACCOUNT_HEAD_ID"] = Configuration.PayableAccount;
        
        dr1["DEBIT"] = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text));
        dr1["CREDIT"] = 0;
        dr1["Principal_Id"] = 0;
        dr1["REMARKS"] = DrpBankAccount.SelectedItem.Text + " Paid to " + DrpVendor.SelectedItem.Text;
        dtVoucher.Rows.Add(dr1);


        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Expanse_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));

        LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), 0, MaxDocumentId, Constants.Expanse_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.Bank_Deposit, Session["VoucherNo"].ToString(), DrpBankAccount.SelectedItem.Text + " Voucher", Constants.DateNullValue, txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), "-1", Constants.DateNullValue, true);


        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Voucher No : " + MaxDocumentId + " has been Saved');", true);

        // PrintVoucher(MaxDocumentId);
    }

    private void InsertGL2()
    {
        DateTime ChequeDate = Constants.DateNullValue;
        try
        {
            if (DrpAccountType.SelectedValue == "18" || DrpAccountType.SelectedValue == "33")
            {
                if (txtStartDate.Text.Length == 10)
                {
                    ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text));

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Correct Cheque Date Pattern is DD/MM/YYYY.');", true);
            return;
        }

        Configuration.GetAccountHead();
        DataTable dtVoucher = new DataTable();

        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));


        decimal discount = 0;

        if (Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)) > 0)
        {
            discount = Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text));

            if (ChkIsDiscount.Checked)
            {
                discount = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)) * (discount / 100);
            }
            DataRow drDiscount = dtVoucher.NewRow();
            drDiscount["ACCOUNT_HEAD_ID"] = Convert.ToInt64(Configuration.InvoiceDiscountVendors);
            drDiscount["REMARKS"] = "Discount on Payment";
            drDiscount["DEBIT"] = 0;
            drDiscount["CREDIT"] = discount;
            drDiscount["Principal_Id"] = 0;
            dtVoucher.Rows.Add(drDiscount);
        }

        decimal tax = 0;

        if (Convert.ToDecimal(dc.chkNull_0(txtTax.Text)) > 0)
        {
            tax = (Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)) - discount) * (Convert.ToDecimal(txtTax.Text) / 100);

            DataRow drTax = dtVoucher.NewRow();
            drTax["ACCOUNT_HEAD_ID"] = Convert.ToInt64(ddlAccountHead.SelectedValue);
            drTax["REMARKS"] = "Tax on Payment " + ddlAccountHead.SelectedItem.Text;
            drTax["DEBIT"] = 0;
            drTax["CREDIT"] = tax;
            drTax["Principal_Id"] = 0;
            dtVoucher.Rows.Add(drTax);
        }


        DataRow dr = dtVoucher.NewRow();
        dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpBankAccount.SelectedValue);
        dr["REMARKS"] = DrpAccountType.SelectedItem.Text + " Paid to " + DrpVendor.SelectedItem.Text;
        dr["DEBIT"] = 0;
        dr["CREDIT"] = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)) - tax - discount;
        dr["Principal_Id"] = 0;
        dtVoucher.Rows.Add(dr);


        //Debit Side Entry


        DataRow dr1 = dtVoucher.NewRow();

        dr1["ACCOUNT_HEAD_ID"] = Configuration.PayableAccount;
        
        dr1["DEBIT"] = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text));
        dr1["CREDIT"] = 0;
        dr1["Principal_Id"] = 0;
        dr1["REMARKS"] = DrpAccountType.SelectedItem.Text + " Paid to " + DrpVendor.SelectedItem.Text;
        dtVoucher.Rows.Add(dr1);


        string MaxDocumentId = "";

        //using chequeDate as Transfer Date
        if (DrpAccountType.SelectedValue == "21")
        {
            MaxDocumentId = LController.SelectMaxVoucherId(Constants.CashPayment_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));

            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), int.Parse(DrpVendor.SelectedValue), MaxDocumentId, Constants.CashPayment_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.Cash_Relization, Session["VoucherNo"].ToString(), DrpAccountType.SelectedItem.Text + " Voucher", Constants.DateNullValue, txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), "-1", Constants.DateNullValue, true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Voucher No : " + MaxDocumentId + " has been Saved');", true);

            PrintVoucher(MaxDocumentId);
        }
        else
        {
            MaxDocumentId = LController.SelectMaxVoucherId(Constants.Expanse_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));

            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), int.Parse(DrpVendor.SelectedValue), MaxDocumentId, Constants.Expanse_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.Bank_Deposit, Session["VoucherNo"].ToString(), DrpAccountType.SelectedItem.Text + " Voucher", ChequeDate, txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), "-1", Constants.DateNullValue, true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Voucher No : " + MaxDocumentId + " has been Saved');", true);

            PrintVoucher(MaxDocumentId);
        }
    }

    private void PrintVoucher(string VoucherNo)
    {
      
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();
        int VoucherType = Constants.IntNullValue;
        
        string VoucherType2 = "";
        string pVoucherType = "";

        if (DrpAccountType.SelectedValue == "18")
        {
            pVoucherType = "Bank Voucher";
            VoucherType = 17; 
            VoucherType2 = "Bank Payment Voucher";
        }
        else if (DrpAccountType.SelectedValue == "21")
        {
            pVoucherType = "Cash Voucher";
            VoucherType = 24; 
            VoucherType2 = "Cash Payment Voucher";
        }
        else
        {
            pVoucherType = "Bank Voucher";
            VoucherType = 17;
            VoucherType2 = "Bank Payment Voucher";
        }

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), VoucherNo, VoucherType,Constants.IntNullValue);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
      //  CrpReport.SetParameterValue("VoucherType", pVoucherType);
      //  CrpReport.SetParameterValue("VoucherSubType", VoucherType2);
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
       
       
    }

    private void PrintChequeVoucher(string ChequeProcessId)
    {
        
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpChequeVoucher CrpReport = new SAMSBusinessLayer.Reports.crpChequeVoucher();
       

        string VoucherType2 = "";
        string pVoucherType = "";

        if (DrpAccountType.SelectedValue == "18")
        {
            pVoucherType = "Bank Voucher";
            VoucherType2 = "Bank Payment Voucher";
        }
       

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), null, Convert.ToInt32(ChequeProcessId), 0);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("VoucherType", pVoucherType);
        CrpReport.SetParameterValue("VoucherSubType", VoucherType2);
        Session.Add("CrpReport", CrpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
        
        
    }
  
    private void ClearAll()
    {
        txtChequeNo.Text = "";
        txtAmount.Text = "";
        txtBankName.Text = "";
        txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        txtReceivedDate.Text = "";
        txtRemarks.Text = "";
        txtTax.Text = "";
        txtDiscount.Text = "";
        txtSlipno.Text = "";

        ChkIsDiscount.Checked = false;

    }

    protected void cbAdvance_CheckedChanged(object sender, EventArgs e)
    {
        if(cbAdvance.Checked)
        {
            DrpStatus.SelectedValue = Constants.Cheque_Clear.ToString();
            DrpStatus.Enabled = false;
            GrdCredit.Enabled = false;
        }
        else
        {
            DrpStatus.SelectedValue = Constants.Cheque_Pending.ToString();
            DrpStatus.Enabled = true;
            GrdCredit.Enabled = true;
        }
        DrpStatus_SelectedIndexChanged(null, null);
        this.SelectCreditInvoice();

        foreach (GridViewRow gvr in GrdCheque.Rows)
        {
            LinkButton btnEdit = gvr.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = gvr.FindControl("btnDelete") as LinkButton;
            if (cbAdvance.Checked)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = false;
            }
        }
    }

    protected void GrdCO_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataControl dc = new DataControl();
            if (DrpAccountType.SelectedValue == "21" || DrpAccountType.SelectedValue == "33")
            {
                int VoucherType = 17;//fOR GL DELETTION
                if (DrpAccountType.SelectedValue == "21")
                {
                    VoucherType = 24;
                }
                long invoiceId = Convert.ToInt64(GrdCO.Rows[e.RowIndex].Cells[2].Text.Replace("&nbsp;", "0"));
                if (LController.DeleteCashBankTransction2(int.Parse(drpDistributor.SelectedValue.ToString())
                    , int.Parse(dc.chkNull_0(GrdCO.Rows[e.RowIndex].Cells[9].Text)), VoucherType,
                 int.Parse(dc.chkNull_0(GrdCO.Rows[e.RowIndex].Cells[10].Text)), invoiceId, Convert.ToDecimal(dc.chkNull_0(GrdCO.Rows[e.RowIndex].Cells[5].Text))))
                {
                    DrpAccountType_SelectedIndexChanged(null, null);
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record remove successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred.')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred.')", true);
        }
    }
    
}