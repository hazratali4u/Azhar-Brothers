using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDatabaseLayer.Classes;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections;
using SAMSDataAccessLayer.Classes;


namespace SAMSBusinessLayer.Classes
{
   public  class VenderEntryController
    {


       #region Constructor

        /// <summary>
        /// Constructor for OrderEntryController
        /// </summary>
       public VenderEntryController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
        #endregion

        // #region Select
        // /// <summary>
        // /// Gets Vendor Credit/Advance Balance
        // /// </summary>
        // /// <remarks>
        // /// Returns Vendor Credit/Advance Balance
        // /// </remarks>
        // /// <param name="p_Customer_Id">Customer</param>
        // /// <param name="p_Principal_Id">Principal</param>
        // /// <param name="p_Distributor_id">Location</param>
        // /// <param name="TranType">Type</param>
        // /// <returns>Vendor Credit/Advance Balance</returns>
        // public DataTable SelectVendorCreditBalance(long p_Customer_Id, int p_Principal_Id, int p_Distributor_id, int TranType)
        // {
        //     IDbConnection mConnection = null;
        //     try
        //     {
        //         Configuration.GetAccountHead();

        //         mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //         mConnection.Open();
        //         UspSelectVendorBalance mCustData = new UspSelectVendorBalance();
        //         mCustData.Connection = mConnection;
        //         mCustData.CUSTOMER_ID = p_Customer_Id;
        //         mCustData.PRINCIPAL_ID = p_Principal_Id;
        //         mCustData.DISTRIBUTOR_ID = p_Distributor_id;
        //         mCustData.TranType = TranType;
        //         mCustData.ACCOUNT_HEAD_ID = long.Parse(Configuration.PayableAccount);

        //         DataTable dt = mCustData.ExecuteTable();
        //         return dt;

        //     }
        //     catch (Exception exp)
        //     {
        //         ExceptionPublisher.PublishException(exp);
        //         return null;
        //     }
        //     finally
        //     {
        //         if (mConnection != null && mConnection.State == ConnectionState.Open)
        //         {
        //             mConnection.Close();
        //         }
        //     }
        // }
        // public DataTable GetVendor(int p_VENDOR_ID)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        spSelectVENDOR mData = new spSelectVENDOR();
        //        mData.Connection = mConnection;
        //        mData.VENDOR_ID = p_VENDOR_ID;
        //        DataTable dt = mData.ExecuteTable();
        //        return dt;

        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        return null;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}

        //public DataTable GetVendorOwner(long p_VENDOR_OWNER_ID, long p_VENDOR_ID)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        var mCustData = new spSelectVENDOR_OWNER
        //        {
        //            Connection = mConnection,
        //            VENDOR_OWNER_ID = p_VENDOR_OWNER_ID,
        //            VENDOR_ID = p_VENDOR_ID
        //        };

        //        DataTable dt = mCustData.ExecuteTable();
        //        return dt;
        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        return null;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}

        //public DataTable SelectAssignVendorSKU(long p_VENDOR_ID, int p_SKU_ID, int TypeId)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        UspGetAssignVendorSKU mGeoHierarchy = new UspGetAssignVendorSKU();
        //        mGeoHierarchy.Connection = mConnection;
        //        mGeoHierarchy.VENDOR_ID = p_VENDOR_ID;
        //        mGeoHierarchy.SKU_ID = p_SKU_ID;
        //        mGeoHierarchy.TYPE = TypeId;
        //        DataTable dt = mGeoHierarchy.ExecuteTable();
        //        return dt;
        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        return null;

        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }
        //}

        //public DataTable GetVendorLedger(long p_VENDOR_LEDGER_ID, long p_VENDOR_ID)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        var mCustData = new spSelectVENDOR_LEDGER2
        //        {
        //            Connection = mConnection,
        //            VENDOR_LEDGER_ID = p_VENDOR_LEDGER_ID,
        //            VENDOR_ID = p_VENDOR_ID
        //        };

        //        DataTable dt = mCustData.ExecuteTable();
        //        return dt;
        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        return null;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}

        public DataTable GetVendorOpening(long p_VENDOR_OPENING_ID, long p_VENDOR_ID, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mCustData = new spSelectVENDOR_OPENING
                {
                    Connection = mConnection,
                    VENDOR_OPENING_ID = p_VENDOR_OPENING_ID,
                    VENDOR_ID = p_VENDOR_ID,
                    TYPE_ID = p_TYPE_ID
                };

                DataTable dt = mCustData.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        //#endregion

        //#region Insert

        //public int InsertVendor(string p_VENDOR_NAME, string p_ADDRESS1, string p_ADDRESS2, string p_ADDRESS3, string p_GST, string p_NTN
        //    , int p_BUSINESS_TYPE_ID, string p_EXEMPTION, DateTime p_EXEMPTION_DATE, string p_CONTACT_PERSON, string p_CONTACT_NO
        //    , int p_DEAL_TYPE, int p_CURRENCY_ID, int p_TYPE, int pCreditorHeadId, int pSaleTaxHeadId, int pTownId, bool pVendorType)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        spInsertVENDOR mVendor = new spInsertVENDOR();
        //        mVendor.Connection = mConnection;

        //        mVendor.VENDOR_NAME = p_VENDOR_NAME;
        //        mVendor.ADDRESS1 = p_ADDRESS1;
        //        mVendor.ADDRESS2 = p_ADDRESS2;
        //        mVendor.ADDRESS3 = p_ADDRESS3;
        //        mVendor.GST = p_GST;
        //        mVendor.NTN = p_NTN;
        //        mVendor.BUSINESS_TYPE_ID = p_BUSINESS_TYPE_ID;
        //        mVendor.EXEMPTION = p_EXEMPTION;
        //        mVendor.EXEMPTION_DATE = p_EXEMPTION_DATE;
        //        mVendor.CONTACT_PERSON = p_CONTACT_PERSON;
        //        mVendor.CONTACT_NO = p_CONTACT_NO;
        //        mVendor.DEAL_TYPE = p_DEAL_TYPE;
        //        mVendor.CURRENCY_ID = p_CURRENCY_ID;
        //        mVendor.TYPE = p_TYPE;
        //        mVendor.CreditorID = pCreditorHeadId;
        //        mVendor.townId = pTownId;
        //        mVendor.vendorType = pVendorType;
        //        mVendor.ExecuteQuery();
        //        return mVendor.VENDOR_ID;

        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        throw exp;
        //        return Constants.IntNullValue;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}

        //public bool UpdateVendor(int p_VENDOR_ID, string p_VENDOR_NAME, string p_ADDRESS1, string p_ADDRESS2, string p_ADDRESS3, string p_GST
        //    , string p_NTN, int p_BUSINESS_TYPE_ID, string p_EXEMPTION, DateTime p_EXEMPTION_DATE, string p_CONTACT_PERSON, string p_CONTACT_NO
        //    , int p_DEAL_TYPE, int p_CURRENCY_ID, int p_TYPE, int pFreightHeadId, int pSaleTaxHeadId, bool pIsActive, int pTownId, bool pVendorType)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        spUpdateVENDOR mVendor = new spUpdateVENDOR();
        //        mVendor.Connection = mConnection;

        //        mVendor.VENDOR_ID = p_VENDOR_ID;
        //        mVendor.VENDOR_NAME = p_VENDOR_NAME;
        //        mVendor.ADDRESS1 = p_ADDRESS1;
        //        mVendor.ADDRESS2 = p_ADDRESS2;
        //        mVendor.ADDRESS3 = p_ADDRESS3;
        //        mVendor.GST = p_GST;
        //        mVendor.NTN = p_NTN;
        //        mVendor.BUSINESS_TYPE_ID = p_BUSINESS_TYPE_ID;
        //        mVendor.EXEMPTION = p_EXEMPTION;
        //        mVendor.EXEMPTION_DATE = p_EXEMPTION_DATE;
        //        mVendor.CONTACT_PERSON = p_CONTACT_PERSON;
        //        mVendor.CONTACT_NO = p_CONTACT_NO;
        //        mVendor.DEAL_TYPE = p_DEAL_TYPE;
        //        mVendor.CURRENCY_ID = p_CURRENCY_ID;
        //        mVendor.TYPE = p_TYPE;
        //        mVendor.CreditorID = pFreightHeadId;
        //        mVendor.SaleTaxID = pSaleTaxHeadId;

        //        mVendor.townId = pTownId;
        //        mVendor.vendorType = pVendorType;
        //        mVendor.IS_ACTIVE = pIsActive;
        //        return mVendor.ExecuteQuery();

        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        throw exp;
        //        return false;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}

        //public bool InsertVendorOwner(long p_VENDOR_ID, string p_OWNER_NAME, string p_FATHER_NAME, string p_RESIDENTIAL_ADDRESS, string p_CNIC_NO, DateTime p_CNIC_EXPIRY, string p_ADDRESS
        //     , string p_EMAIL_ADDRESS, string p_CONTACT_NO, string p_MOBILE_NO, string p_PTCL, string p_PIC)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        spInsertVENDOR_OWNER mCustomer = new spInsertVENDOR_OWNER();
        //        mCustomer.Connection = mConnection;
        //        mCustomer.VENDOR_ID = p_VENDOR_ID;
        //        mCustomer.OWNER_NAME = p_OWNER_NAME;
        //        mCustomer.FATHER_NAME = p_FATHER_NAME;
        //        mCustomer.RESIDENTIAL_ADDRESS = p_RESIDENTIAL_ADDRESS;
        //        mCustomer.CNIC_NO = p_CNIC_NO;
        //        mCustomer.CNIC_EXPIRY = p_CNIC_EXPIRY;
        //        mCustomer.ADDRESS = p_ADDRESS;
        //        mCustomer.EMAIL_ADDRESS = p_EMAIL_ADDRESS;
        //        mCustomer.CONTACT_NO = p_CONTACT_NO;
        //        mCustomer.MOBILE_NO = p_MOBILE_NO;
        //        mCustomer.PTCL = p_PTCL;
        //        mCustomer.PIC = p_PIC;
        //        return mCustomer.ExecuteQuery();

        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        throw exp;
        //        return false;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}

        //public bool UpdateVendorOwner(long p_VENDOR_OWNER_ID, long p_VENDOR_ID, string p_OWNER_NAME, string p_FATHER_NAME, string p_RESIDENTIAL_ADDRESS, string p_CNIC_NO, DateTime p_CNIC_EXPIRY, string p_ADDRESS
        //     , string p_EMAIL_ADDRESS, string p_CONTACT_NO, string p_MOBILE_NO, string p_PTCL, string p_PIC)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        spUpdateVENDOR_OWNER mCustomer = new spUpdateVENDOR_OWNER();
        //        mCustomer.Connection = mConnection;
        //        mCustomer.VENDOR_OWNER_ID = p_VENDOR_OWNER_ID;
        //        mCustomer.VENDOR_ID = p_VENDOR_ID;
        //        mCustomer.OWNER_NAME = p_OWNER_NAME;
        //        mCustomer.FATHER_NAME = p_FATHER_NAME;
        //        mCustomer.RESIDENTIAL_ADDRESS = p_RESIDENTIAL_ADDRESS;
        //        mCustomer.CNIC_NO = p_CNIC_NO;
        //        mCustomer.CNIC_EXPIRY = p_CNIC_EXPIRY;
        //        mCustomer.ADDRESS = p_ADDRESS;
        //        mCustomer.EMAIL_ADDRESS = p_EMAIL_ADDRESS;
        //        mCustomer.CONTACT_NO = p_CONTACT_NO;
        //        mCustomer.MOBILE_NO = p_MOBILE_NO;
        //        mCustomer.PTCL = p_PTCL;
        //        mCustomer.PIC = p_PIC;
        //        return mCustomer.ExecuteQuery();

        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        throw exp;
        //        return false;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}

        //public string InsertVendorSKU(long p_VENDOR_ID, int p_SKU_Id, int p_UserId)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        spInsertVENDOR_SKU mDistributor = new spInsertVENDOR_SKU();
        //        mDistributor.Connection = mConnection;

        //        mDistributor.VENDOR_ID = p_VENDOR_ID;
        //        mDistributor.SKU_ID = p_SKU_Id;
        //        mDistributor.USER_ID = p_UserId;

        //        mDistributor.ExecuteQuery();
        //        return "Record Inserted";

        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        return null;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}

        //public string DeletedVendorSKU(long p_VENDOR_ID, int p_SKU_Id)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        spDeleteVENDOR_SKU mDistributor = new spDeleteVENDOR_SKU();
        //        mDistributor.Connection = mConnection;

        //        mDistributor.VENDOR_ID = p_VENDOR_ID;
        //        mDistributor.SKU_ID = p_SKU_Id;

        //        mDistributor.ExecuteQuery();
        //        return "Record Deleted.";
        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        return null;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}

        public bool InsertVendorLedger(long p_VENDOR_ID, int p_DISTRIBUTOR_ID, DateTime p_LEDGER_DATE, decimal p_BALANCE, string p_REMARKS)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertVENDOR_LEDGER2 mCustomer = new spInsertVENDOR_LEDGER2();
                mCustomer.Connection = mConnection;
                mCustomer.VENDOR_ID = p_VENDOR_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.LEDGER_DATE = p_LEDGER_DATE;
                mCustomer.BALANCE = p_BALANCE;
                mCustomer.REMARKS = p_REMARKS;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool UpdateVendorOpening(long p_VENDOR_OPENING_ID, long p_VENDOR_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID, DateTime p_OPENING_DATE, decimal p_BALANCE, string p_REMARKS)
        {
            IDbConnection mConnection = null;

            //  IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateVENDOR_OPENING mCustomer = new spUpdateVENDOR_OPENING();
                mCustomer.Connection = mConnection;
                //mCustomer.Transaction = mTransaction;

                mCustomer.VENDOR_OPENING_ID = p_VENDOR_OPENING_ID;
                mCustomer.VENDOR_ID = p_VENDOR_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.TYPE_ID = p_TYPE_ID;
                mCustomer.OPENING_DATE = p_OPENING_DATE;
                mCustomer.BALANCE = p_BALANCE;
                mCustomer.REMARKS = p_REMARKS;
                mCustomer.ExecuteQuery();


                //  mTransaction.Commit();
                return true;

            }
            catch (Exception exp)
            {
                //  mTransaction.Rollback();
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        //}

        //public bool UpdateVendorLedger(long p_VENDOR_LEDGER_ID, long p_VENDOR_ID, int p_DISTRIBUTOR_ID, DateTime p_LEDGER_DATE, decimal p_BALANCE, string p_REMARKS)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        spUpdateVENDOR_LEDGER2 mCustomer = new spUpdateVENDOR_LEDGER2();
        //        mCustomer.Connection = mConnection;
        //        mCustomer.VENDOR_LEDGER_ID = p_VENDOR_LEDGER_ID;
        //        mCustomer.VENDOR_ID = p_VENDOR_ID;
        //        mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
        //        mCustomer.LEDGER_DATE = p_LEDGER_DATE;
        //        mCustomer.BALANCE = p_BALANCE;
        //        mCustomer.REMARKS = p_REMARKS;
        //        return mCustomer.ExecuteQuery();

        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        throw exp;
        //        return false;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}
        public void InsertVendorLedger(long p_ACCOUNT_HEAD_ID, int p_Distributor_Id, decimal p_Debit, decimal p_Credit, DateTime p_Ledger_Date, string p_Remarks, int p_PRINCIPAL_ID, long p_DOCUMENT_NO, int p_DOCUMENT_TYPE_ID, int p_UserId, IDbTransaction mTransaction, IDbConnection mConnection, int p_Paymode)
        {

            try
            {
                uspInsertVendorLedger mspInsertLedger = new uspInsertVendorLedger();
                mspInsertLedger.Connection = mConnection;
                mspInsertLedger.Transaction = mTransaction;
                mspInsertLedger.ACCOUNT_HEAD_ID = p_ACCOUNT_HEAD_ID;
                mspInsertLedger.DISTRIBUTOR_ID = p_Distributor_Id;
                mspInsertLedger.DEBIT = p_Debit;
                mspInsertLedger.CREDIT = p_Credit;
                mspInsertLedger.VENDOR_LEDGER_DATE = p_Ledger_Date;
                mspInsertLedger.REMARKS = p_Remarks;
                mspInsertLedger.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mspInsertLedger.DOCUMENT_TYPE_ID = p_DOCUMENT_TYPE_ID;
                mspInsertLedger.DOCUMENT_NO = p_DOCUMENT_NO;
                mspInsertLedger.USER_ID = p_UserId;
                mspInsertLedger.PAYMENT_MODE = p_Paymode;
                mspInsertLedger.ExecuteQuery();
            }

            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                //return null;

            }

        }

        public bool InsertVendorOpening(long p_VENDOR_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID, DateTime p_OPENING_DATE, decimal p_BALANCE, string p_REMARKS)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertVENDOR_OPENING mCustomer = new spInsertVENDOR_OPENING();
                mCustomer.Connection = mConnection;
                mCustomer.Transaction = mTransaction;
                mCustomer.VENDOR_ID = p_VENDOR_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.TYPE_ID = p_TYPE_ID;
                mCustomer.OPENING_DATE = p_OPENING_DATE;
                mCustomer.BALANCE = p_BALANCE;
                mCustomer.REMARKS = p_REMARKS;

                if (p_TYPE_ID == 0)
                {
                    InsertVendorLedger(652, p_DISTRIBUTOR_ID, 0, p_BALANCE, p_OPENING_DATE, "Vendor Opening",Convert.ToInt32(p_VENDOR_ID), 0, Constants.Document_Purchase, 1, mTransaction, mConnection, 25);
                    InsertVendorLedger(456, p_DISTRIBUTOR_ID, p_BALANCE, 0, p_OPENING_DATE, "Vendor Opening", Convert.ToInt32(p_VENDOR_ID), 0, Constants.Document_Purchase, 1, mTransaction, mConnection, 25);
                }
                else
                {
                    InsertVendorLedger(652, p_DISTRIBUTOR_ID, p_BALANCE, 0, p_OPENING_DATE, "Vendor Opening", Convert.ToInt32(p_VENDOR_ID), 0, Constants.Document_Purchase, 1, mTransaction, mConnection, 25);
                    InsertVendorLedger(456, p_DISTRIBUTOR_ID, 0, p_BALANCE, p_OPENING_DATE, "Vendor Opening", Convert.ToInt32(p_VENDOR_ID),0, Constants.Document_Purchase, 1, mTransaction, mConnection, 25);

                }
                mTransaction.Commit();
                return mCustomer.ExecuteQuery();
            }
            catch (Exception exp)
            {
                mTransaction.Rollback();
                ExceptionPublisher.PublishException(exp);
                throw exp;

                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        //public bool UpdateVendorOpening(long p_VENDOR_OPENING_ID, long p_VENDOR_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID, DateTime p_OPENING_DATE, decimal p_BALANCE, string p_REMARKS)
        //{
        //    IDbConnection mConnection = null;

        //  //  IDbTransaction mTransaction = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        spUpdateVENDOR_OPENING mCustomer = new spUpdateVENDOR_OPENING();
        //        mCustomer.Connection = mConnection;
        //        //mCustomer.Transaction = mTransaction;

        //        mCustomer.VENDOR_OPENING_ID = p_VENDOR_OPENING_ID;
        //        mCustomer.VENDOR_ID = p_VENDOR_ID;
        //        mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
        //        mCustomer.TYPE_ID = p_TYPE_ID;
        //        mCustomer.OPENING_DATE = p_OPENING_DATE;
        //        mCustomer.BALANCE = p_BALANCE;
        //        mCustomer.REMARKS = p_REMARKS;
        //        mCustomer.ExecuteQuery();


        //        //  mTransaction.Commit();
        //        return true;

        //    }
        //    catch (Exception exp)
        //    {
        //      //  mTransaction.Rollback();
        //        ExceptionPublisher.PublishException(exp);
        //        return false;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}


        // #region Vendor Invoice

        //public bool Add_VenderInvoice(int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, int p_UserId, DateTime p_DocumentDate
        //    , string pInvoiceNo, DateTime pInvoiceDate, string pREMARKS, decimal pAmount)
        //{

        //    IDbConnection mConnection = null;
        //    IDbTransaction mTransaction = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        mTransaction = ProviderFactory.GetTransaction(mConnection);

        //        spInsertVENDER_INVOICE_MASTER mISom = new spInsertVENDER_INVOICE_MASTER();
        //        mISom.Connection = mConnection;
        //        mISom.Transaction = mTransaction;


        //            mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
        //            mISom.DOCUMENT_DATE = p_DocumentDate;
        //            mISom.IS_DELETED = false;
        //            mISom.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
        //            mISom.TIME_STAMP = DateTime.Now;
        //            mISom.LASTUPDATE_DATE = System.DateTime.Now;
        //            mISom.INVOICE_DATE = pInvoiceDate;
        //            mISom.INVOICE_NO = pInvoiceNo;
        //            mISom.REMARKS = pREMARKS;
        //            mISom.AMOUNT = pAmount;
        //            mISom.DEBIT_AMOUNT = pAmount;
        //            mISom.ExecuteQuery();


        //            #region Account Posting

        //            LedgerController LController = new LedgerController();
        //            Configuration.GetAccountHead();

        //            string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, 1);

        //            LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), 97, p_DISTRIBUTOR_ID, 0, pAmount, p_DocumentDate, "Purchase - " + pREMARKS, DateTime.Now, p_PRINCIPAL_ID, mISom.VENDER_INVOICE_ID, "0", Constants.Document_PrincipalInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, "0");
        //            LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.PayableAccount), p_DISTRIBUTOR_ID, pAmount, 0, p_DocumentDate, "Purchase - " + pREMARKS, DateTime.Now, p_PRINCIPAL_ID, mISom.VENDER_INVOICE_ID, "0", Constants.Document_PrincipalInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, "0");

        //            #endregion

        //            mTransaction.Commit();

        //            return true;
        //    }
        //    catch (Exception exp)
        //    {
        //        mTransaction.Rollback();
        //        ExceptionPublisher.PublishException(exp);
        //        return false;// exp.Message;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }
        //}

        //public bool Update_VenderInvoice(int p_DISTRIBUTOR_ID, long pVendorInvoiceId, int p_PRINCIPAL_ID, int p_UserId, string pInvoiceNo
        //    , DateTime pInvoiceDate, string p_REMARKS, bool pDeleted, decimal pAmount)
        //{
        //    IDbConnection mConnection = null;
        //    IDbTransaction mTransaction = null;

        //    try
        //    {

        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();


        //        mTransaction = ProviderFactory.GetTransaction(mConnection);


        //        LedgerController LController = new LedgerController();
        //        string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, 1);

        //        spUpdateVENDER_INVOICE_MASTER mISom = new spUpdateVENDER_INVOICE_MASTER();
        //        mISom.Connection = mConnection;
        //        mISom.Transaction = mTransaction;

        //        mISom.VENDER_INVOICE_ID = pVendorInvoiceId;
        //        mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;            
        //        mISom.IS_DELETED = pDeleted;
        //        mISom.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
        //        mISom.LASTUPDATE_DATE = System.DateTime.Now;
        //        mISom.INVOICE_DATE = pInvoiceDate;
        //        mISom.INVOICE_NO = pInvoiceNo;

        //        mISom.REMARKS = p_REMARKS;
        //        mISom.AMOUNT = pAmount;
        //        mISom.DEBIT_AMOUNT = pAmount;
        //        mISom.ExecuteQuery();

        //        Configuration.GetAccountHead();


        //        LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), 97, p_DISTRIBUTOR_ID, 0, pAmount, pInvoiceDate, "Purchase Default", DateTime.Now, p_PRINCIPAL_ID,  mISom.VENDER_INVOICE_ID, "0", Constants.Document_PrincipalInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, "0");
        //        LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.PayableAccount), p_DISTRIBUTOR_ID, pAmount, 0, pInvoiceDate, "Purchase Default", DateTime.Now, p_PRINCIPAL_ID, mISom.VENDER_INVOICE_ID, "0", Constants.Document_PrincipalInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, "0");

        //        mTransaction.Commit();
        //        return true;

        //    }
        //    catch (Exception exp)
        //    {
        //        mTransaction.Rollback();
        //        ExceptionPublisher.PublishException(exp);
        //        return false;// exp.Message;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }
        //}

        //public bool Delete_VenderInvoice(int p_DISTRIBUTOR_ID, long pVendorInvoiceId, int p_PRINCIPAL_ID, int p_UserId, string pInvoiceNo
        //  , DateTime pInvoiceDate, string p_REMARKS, bool pDeleted, decimal pAmount)
        //{
        //    IDbConnection mConnection = null;

        //    try
        //    {

        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();


        //        spUpdateVENDER_INVOICE_MASTER mISom = new spUpdateVENDER_INVOICE_MASTER();
        //        mISom.Connection = mConnection;

        //        mISom.VENDER_INVOICE_ID = pVendorInvoiceId;
        //        mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
        //        mISom.IS_DELETED = pDeleted;
        //        mISom.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
        //        mISom.LASTUPDATE_DATE = System.DateTime.Now;
        //        mISom.INVOICE_DATE = pInvoiceDate;
        //        mISom.INVOICE_NO = pInvoiceNo;

        //        mISom.REMARKS = p_REMARKS;
        //        mISom.AMOUNT = pAmount;
        //        mISom.DEBIT_AMOUNT = pAmount;
        //        mISom.ExecuteQuery();


        //        return true;

        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        return false;// exp.Message;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }
        //}

        //public DataTable SelectVendorMaster(int pDistributorId, int p_Principal_Id)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        UspSelectPendingVender mOrder = new UspSelectPendingVender();
        //        mOrder.Connection = mConnection;

        //        mOrder.PRINCIPAL_ID = p_Principal_Id;
        //        mOrder.DISTRIBUTOR_ID = pDistributorId;
        //        DataTable dt = mOrder.ExecuteTable();
        //        return dt;

        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        return null;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}


        // #endregion
        // #endregion
    }
}
