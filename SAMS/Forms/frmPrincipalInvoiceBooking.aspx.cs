using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmPrincipalInvoiceBooking : System.Web.UI.Page
{
    Principal_Invoice_BookingController PIBController = new Principal_Invoice_BookingController();
    private static int Pri_Inv_Booking_ID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtInvoiceDate.Attributes.Add("readonly", "readonly");
            txtInvoiceDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            this.LoadPrincipal();
            this.LoadLocation();
            this.LoadGrid();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {


            PIBController.InsertPRINCIPAL_INVOICE_BOOKING(
               int.Parse(ddlLocation.SelectedValue.ToString())
               , int.Parse(ddlPrincipal.SelectedValue.ToString())
               , txtInvoiceNumber.Text
               , Convert.ToDateTime(txtInvoiceDate.Text)
               , Convert.ToInt32(txtInvoicAmount.Text)
               , txtRemarks.Text
               , int.Parse(this.Session["UserId"].ToString())
               );

            int Pri_Invoice_ID = PIBController.Pri_Invoice_ID();
            if (Pri_Invoice_ID > 0)
            {
                LedgerController LController = new LedgerController();
                IDbTransaction mTransaction = null;
                IDbConnection mConnection = null;
                try
                {
                    mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                    mConnection.Open();
                    #region Vendor Ledger
                    Configuration.GetAccountHead();
                    string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(ddlLocation.SelectedValue.ToString()), 1, mTransaction, mConnection);
                    LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), int.Parse(ddlLocation.SelectedValue.ToString()), 0, Convert.ToInt32(txtInvoicAmount.Text), Convert.ToDateTime(txtInvoiceDate.Text), "Invoice Default", DateTime.Now, int.Parse(ddlPrincipal.SelectedValue.ToString()), Pri_Invoice_ID, txtInvoiceNumber.Text, Constants.Document_PrincipalInvoice, int.Parse(this.Session["UserId"].ToString()), mTransaction, mConnection, Constants.CreditSale, "Invoice");
                    LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.PayableAccount), int.Parse(ddlLocation.SelectedValue.ToString()), Convert.ToInt32(txtInvoicAmount.Text), 0, Convert.ToDateTime(txtInvoiceDate.Text), "Invoice Default", DateTime.Now, int.Parse(ddlPrincipal.SelectedValue.ToString()), Pri_Invoice_ID, txtInvoiceNumber.Text, Constants.Document_PrincipalInvoice, int.Parse(this.Session["UserId"].ToString()), mTransaction, mConnection, Constants.CreditSale, "Invoice");
                    #endregion
                }
                catch (Exception exp)
                {
                    ExceptionPublisher.PublishException(exp);
                    //return null;
                }
                finally
                {
                    if (mConnection != null && mConnection.State == ConnectionState.Open)
                    {
                        mConnection.Close();
                    }
                }
            }
        }
        else
        {
            PIBController.UpdatePRINCIPAL_INVOICE_BOOKING(
                 int.Parse(ddlLocation.SelectedValue.ToString())
                 , int.Parse(ddlPrincipal.SelectedValue.ToString())
                 , txtInvoiceNumber.Text
                 , Convert.ToDateTime(txtInvoiceDate.Text)
                 , Convert.ToInt32(txtInvoicAmount.Text)
                 , txtRemarks.Text
                 , Pri_Inv_Booking_ID
                 , int.Parse(this.Session["UserId"].ToString())
                 );
            if (Pri_Inv_Booking_ID > 0)
            {
                LedgerController LController = new LedgerController();
                IDbTransaction mTransaction = null;
                IDbConnection mConnection = null;
                try
                {
                    mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                    mConnection.Open();
                    #region Vendor Ledger
                    Configuration.GetAccountHead();
                    string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(ddlLocation.SelectedValue.ToString()), 1, mTransaction, mConnection);
                    LController.UpdatePostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), int.Parse(ddlLocation.SelectedValue.ToString()), 0, Convert.ToInt32(txtInvoicAmount.Text), Convert.ToDateTime(txtInvoiceDate.Text), "Invoice Default", DateTime.Now, int.Parse(ddlPrincipal.SelectedValue.ToString()), Pri_Inv_Booking_ID, txtInvoiceNumber.Text, Constants.Document_PrincipalInvoice, int.Parse(this.Session["UserId"].ToString()), mTransaction, mConnection, Constants.CreditSale, "Invoice");
                    LController.UpdatePostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.PayableAccount), int.Parse(ddlLocation.SelectedValue.ToString()), Convert.ToInt32(txtInvoicAmount.Text), 0, Convert.ToDateTime(txtInvoiceDate.Text), "Invoice Default", DateTime.Now, int.Parse(ddlPrincipal.SelectedValue.ToString()), Pri_Inv_Booking_ID, txtInvoiceNumber.Text, Constants.Document_PrincipalInvoice, int.Parse(this.Session["UserId"].ToString()), mTransaction, mConnection, Constants.CreditSale, "Invoice");
                    #endregion
                }
                catch (Exception exp)
                {
                    ExceptionPublisher.PublishException(exp);
                    //return null;
                }
                finally
                {
                    if (mConnection != null && mConnection.State == ConnectionState.Open)
                    {
                        mConnection.Close();
                    }
                }
            }
        }
        lblErrorMsg.Text = "";
        this.LoadGrid();
        ClearAll();
    }
    private void LoadGrid()
    {
        SqlConnection con = new SqlConnection(Configuration.ConnectionString);
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.Connection = con;
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "select * from PRINCIPAL_INVOICE_BOOKING LEFT JOIN SKU_HIERARCHY DT  ON PRINCIPAL_INVOICE_BOOKING.PRINCIPAL_ID = DT.SKU_HIE_ID where PRINCIPAL_INVOICE_BOOKING.IS_DELETED = 0";
        SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);
        DataTable dtRecord = new DataTable();
        sqlDataAdap.Fill(dtRecord);
        GridPrincipalInvoiceBk.DataSource = dtRecord;
        GridPrincipalInvoiceBk.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

    }

    protected void GridPrincipalInvoiceBk_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Pri_Inv_Booking_ID = int.Parse(GridPrincipalInvoiceBk.Rows[e.NewEditIndex].Cells[0].Text);
        ddlPrincipal.SelectedValue = GridPrincipalInvoiceBk.Rows[e.NewEditIndex].Cells[5].Text;
        LoadPrincipal();
        txtInvoiceNumber.Text = GridPrincipalInvoiceBk.Rows[e.NewEditIndex].Cells[2].Text;
        txtInvoiceDate.Text = GridPrincipalInvoiceBk.Rows[e.NewEditIndex].Cells[3].Text;
        txtInvoicAmount.Text = GridPrincipalInvoiceBk.Rows[e.NewEditIndex].Cells[4].Text;
        txtRemarks.Text = GridPrincipalInvoiceBk.Rows[e.NewEditIndex].Cells[6].Text;
        btnSave.Text = "Update";
    }

    protected void GridPrincipalInvoiceBk_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridPrincipalInvoiceBk.PageIndex = e.NewPageIndex;
        this.LoadGrid();
    }

    private void ClearAll()
    {
        txtInvoiceNumber.Text = "";
        txtInvoiceDate.Text = "";
        txtInvoicAmount.Text = "";
        txtRemarks.Text = "";
        txtPIBID.Text = "";
        btnSave.Text = "Save";
    }
    private void LoadPrincipal()
    {
        try
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            clsWebFormUtil.FillDropDownList(this.ddlPrincipal, m_dt, 0, 1, false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void LoadLocation()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlLocation, dt, 0, 2, true);
    }

    protected void GridPrincipalInvoiceBk_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Pri_Inv_Booking_ID = int.Parse(GridPrincipalInvoiceBk.Rows[e.RowIndex].Cells[0].Text);
        try
        {
            using (SqlConnection con = new SqlConnection(Configuration.ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("UPDATE PRINCIPAL_INVOICE_BOOKING SET IS_DELETED = 1 WHERE PRI_INVOICE_BOOKING_ID=" + Pri_Inv_Booking_ID, con))
                {
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command2 = new SqlCommand("UPDATE VENDOR_LEDGER SET IS_DELETED = 1 WHERE DOCUMENT_NO=" + Pri_Inv_Booking_ID, con))
                {
                    command2.ExecuteNonQuery();
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {

        }

        this.LoadGrid();
    }
}