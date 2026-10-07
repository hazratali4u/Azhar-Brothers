using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;
using SAMSDatabaseLayer.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SAMSBusinessLayer.Classes
{
    public class Principal_Invoice_BookingController
    {
        #region Constructor

        /// <summary>
        /// Constructor for Principal_Invoice_Booking
        /// </summary>
        public Principal_Invoice_BookingController()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        LedgerController LController = new LedgerController();

        IDbTransaction mTransaction;
        IDbConnection mConnection;
        #endregion

        /// <summary>
        /// Inserts Principal_Invoice_Booking
        /// </summary>
        /// <remarks>
        /// Returns Inserted PRINCIPAL_INVOICE_BOOKING_ID
        /// </remarks>
        /// <param name="p_LOCATION_ID">LOCATION</param>
        /// <param name="p_PRINCIPAL_ID">PRINCIPAL</param>
        /// <param name="p_INVOICE_NO">INVOICE NUMBER</param>
        /// <param name="p_INVOICEDATE">INVOICEDATE</param>
        /// <param name="p_INVOICEAMOUNT">INVOICEAMOUNT</param>
        /// <param name="p_REMARKS">REMARKS</param>
        /// <param name="p_USER_ID">USER_ID</param>
        /// <param name="p_PRI_INVOICE_BOOKING_ID">PRINCIPAL_INVOICE_BOOKING_ID</param>
        /// <returns>Inserted PRINCIPAL_INVOICE_BOOKING_ID</returns>
        public string InsertPRINCIPAL_INVOICE_BOOKING(int p_LOCATION_ID, int p_PRINCIPAL_ID, string p_INVOICE_NO, DateTime p_INVOICEDATE, int p_INVOICEAMOUNT, string p_REMARKS, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                //mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPRINCIPAL_INVOICE_BOOKING mPRI_INVOICE_BOOKING = new spInsertPRINCIPAL_INVOICE_BOOKING();
                mPRI_INVOICE_BOOKING.Connection = mConnection;
                //mPRI_INVOICE_BOOKING.Transaction = mTransaction;
                mPRI_INVOICE_BOOKING.LOCATION_ID = p_LOCATION_ID;
                mPRI_INVOICE_BOOKING.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPRI_INVOICE_BOOKING.INVOICE_NO = p_INVOICE_NO;
                mPRI_INVOICE_BOOKING.INVOICEAMOUNT = p_INVOICEAMOUNT;
                mPRI_INVOICE_BOOKING.REMARKS = p_REMARKS;
                mPRI_INVOICE_BOOKING.USER_ID = p_USER_ID;
                mPRI_INVOICE_BOOKING.INVOICEDATE = p_INVOICEDATE;
                mPRI_INVOICE_BOOKING.ExecuteQuery();
                return mPRI_INVOICE_BOOKING.PRI_INVOICE_BOOKING_ID.ToString();
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

        /// <summary>
        /// Update Principal_Invoice_Booking
        /// </summary>
        /// <remarks>
        /// Returns Update PRINCIPAL_INVOICE_BOOKING_ID
        /// </remarks>
        /// <param name="p_LOCATION_ID">LOCATION</param>
        /// <param name="p_PRINCIPAL_ID">PRINCIPAL</param>
        /// <param name="p_INVOICE_NO">INVOICE NUMBER</param>
        /// <param name="p_INVOICEDATE">INVOICEDATE</param>
        /// <param name="p_INVOICEAMOUNT">INVOICEAMOUNT</param>
        /// <param name="p_REMARKS">REMARKS</param>
        /// <param name="p_USER_ID">USER_ID</param>
        /// <param name="p_PRI_INVOICE_BOOKING_ID">PRINCIPAL_INVOICE_BOOKING_ID</param>
        /// <remarks>
        /// <returns>PRINCIPAL_INVOICE_BOOKING_ID</returns>
        public string UpdatePRINCIPAL_INVOICE_BOOKING(int p_LOCATION_ID, int p_PRINCIPAL_ID, string p_INVOICE_NO, DateTime p_INVOICEDATE, int p_INVOICEAMOUNT, string p_REMARKS, int p_PRI_INVOICE_BOOKING_ID, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPRINCIPAL_INVOICE_BOOKING mPRI_INVOICE_BOOKING = new spInsertPRINCIPAL_INVOICE_BOOKING();
                mPRI_INVOICE_BOOKING.Connection = mConnection;
                mPRI_INVOICE_BOOKING.PRI_INVOICE_BOOKING_ID = p_PRI_INVOICE_BOOKING_ID;
                mPRI_INVOICE_BOOKING.LOCATION_ID = p_LOCATION_ID;
                mPRI_INVOICE_BOOKING.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPRI_INVOICE_BOOKING.INVOICE_NO = p_INVOICE_NO;
                mPRI_INVOICE_BOOKING.INVOICEAMOUNT = p_INVOICEAMOUNT;
                mPRI_INVOICE_BOOKING.REMARKS = p_REMARKS;
                mPRI_INVOICE_BOOKING.USER_ID = p_USER_ID;
                mPRI_INVOICE_BOOKING.INVOICEDATE = p_INVOICEDATE;
                mPRI_INVOICE_BOOKING.ExecuteUpdateQuery();
                return mPRI_INVOICE_BOOKING.PRI_INVOICE_BOOKING_ID.ToString();
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
        public int Pri_Invoice_ID()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectMaxPRINCIPAL_INVOICE_BOOKING mPRI_INVOICE_BOOKING = new spSelectMaxPRINCIPAL_INVOICE_BOOKING();
                mPRI_INVOICE_BOOKING.Connection = mConnection;
                mPRI_INVOICE_BOOKING.ExecuteQuery();
                return mPRI_INVOICE_BOOKING.PRI_INVOICE_BOOKING_ID;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return 0;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }


        public string DeletePRINCIPAL_INVOICE_BOOKING(int p_LOCATION_ID, int p_PRINCIPAL_ID, string p_INVOICE_NO, DateTime p_INVOICEDATE, int p_INVOICEAMOUNT, string p_REMARKS, int p_PRI_INVOICE_BOOKING_ID, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPRINCIPAL_INVOICE_BOOKING mPRI_INVOICE_BOOKING = new spInsertPRINCIPAL_INVOICE_BOOKING();
                mPRI_INVOICE_BOOKING.Connection = mConnection;
                mPRI_INVOICE_BOOKING.PRI_INVOICE_BOOKING_ID = p_PRI_INVOICE_BOOKING_ID;
                //mPRI_INVOICE_BOOKING.LOCATION_ID = p_LOCATION_ID;
                //mPRI_INVOICE_BOOKING.PRINCIPAL_ID = p_PRINCIPAL_ID;
                //mPRI_INVOICE_BOOKING.INVOICE_NO = p_INVOICE_NO;
                //mPRI_INVOICE_BOOKING.INVOICEAMOUNT = p_INVOICEAMOUNT;
                //mPRI_INVOICE_BOOKING.REMARKS = p_REMARKS;
                //mPRI_INVOICE_BOOKING.USER_ID = p_USER_ID;
                //mPRI_INVOICE_BOOKING.INVOICEDATE = p_INVOICEDATE;
                mPRI_INVOICE_BOOKING.ExecuteDeleteQuery();

                return mPRI_INVOICE_BOOKING.PRI_INVOICE_BOOKING_ID.ToString();

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
        /// <summary>
        /// Gets Location Data
        /// </summary>
        /// <remarks>
        /// Returns Location Data as Datatable
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_distributor_type">Type</param>
        /// <param name="p_Company_Id">Company</param>
        /// <returns>Location Data as Datatable</returns>
        public DataTable SelectPrincipalInviceBooking()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPRINCIPAL_INVOICE_BOOKING mPRI_INVOICE_BOOKING = new spInsertPRINCIPAL_INVOICE_BOOKING();
                mPRI_INVOICE_BOOKING.Connection = mConnection;
                mPRI_INVOICE_BOOKING.PRI_INVOICE_BOOKING_ID = Constants.IntNullValue;
                mPRI_INVOICE_BOOKING.PRINCIPAL_ID = Constants.IntNullValue;
                mPRI_INVOICE_BOOKING.INVOICEDATE = Constants.DateNullValue;
                mPRI_INVOICE_BOOKING.INVOICEAMOUNT = Constants.IntNullValue;
                mPRI_INVOICE_BOOKING.LOCATION_ID = Constants.IntNullValue;
                //mPRI_INVOICE_BOOKING.SUBZONE_ID = p_distributor_type;
                mPRI_INVOICE_BOOKING.REMARKS = null;
                mPRI_INVOICE_BOOKING.INVOICE_NO = null;
                mPRI_INVOICE_BOOKING.USER_ID = Constants.IntNullValue;
                mPRI_INVOICE_BOOKING.SKU_HIE_NAME = null;
                //mPRI_INVOICE_BOOKING.DISTRIBUTOR_ID = p_Distributor_Id;
                //mPRI_INVOICE_BOOKING.COMPANY_ID = p_Company_Id;
                DataTable dt = mPRI_INVOICE_BOOKING.ExecuteTable();
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
        public DataTable SelectPRI_INVOICE_BOOKING_ID(int p_PRI_INVOICE_BOOKING_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPRINCIPAL_INVOICE_BOOKING mPRI_INVOICE_BOOKING = new spInsertPRINCIPAL_INVOICE_BOOKING();
                mPRI_INVOICE_BOOKING.Connection = mConnection;
                mPRI_INVOICE_BOOKING.PRI_INVOICE_BOOKING_ID = Constants.IntNullValue;
                mPRI_INVOICE_BOOKING.PRINCIPAL_ID = Constants.IntNullValue;
                mPRI_INVOICE_BOOKING.INVOICEDATE = Constants.DateNullValue;
                mPRI_INVOICE_BOOKING.INVOICEAMOUNT = Constants.IntNullValue;
                mPRI_INVOICE_BOOKING.LOCATION_ID = Constants.IntNullValue;
                //mPRI_INVOICE_BOOKING.SUBZONE_ID = p_distributor_type;
                mPRI_INVOICE_BOOKING.REMARKS = null;
                mPRI_INVOICE_BOOKING.INVOICE_NO = null;
                mPRI_INVOICE_BOOKING.USER_ID = Constants.IntNullValue;
                DataTable dt = mPRI_INVOICE_BOOKING.ExecuteTable();
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


        /// <summary>
        /// Gets Employee Data
        /// </summary>
        /// <remarks>
        /// Returns Employee Data as Datatable
        /// </remarks>
        /// <param name="p_Type">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="Companyid">Company</param>
        /// <returns>Employee Data as Datatable</returns>
        public DataTable SelectDistributorUser(int p_Type, int p_Distributor_Id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectDISTRIBUTOR_USERInfo mDistUser = new uspSelectDISTRIBUTOR_USERInfo();
                mDistUser.Connection = mConnection;

                mDistUser.TYPE = p_Type;
                mDistUser.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistUser.COMPANY_ID = Companyid;

                DataTable dt = mDistUser.ExecuteTable();
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

        ///// <summary>
        ///// Get All Employees
        ///// </summary>
        ///// <returns>All Employees Data as Datatable</returns>
        //public DataTable SelectGLUser()
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        UspSelectGLUserDetail mDistUser = new UspSelectGLUserDetail();
        //        mDistUser.Connection = mConnection;
        //        DataTable dt = mDistUser.ExecuteTable();
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
    }
}
