using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;
using SAMSDatabaseLayer.ReportClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SAMSBusinessLayer.Classes
{
   public class RptOrderBookerWiseSaleController
    {
        #region Constructor

        /// <summary>
        /// Constructor for RptOrderBookerWiseSaleController
        /// </summary>
        public RptOrderBookerWiseSaleController()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Gets Data For Stock Reconciliation Report
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_FromDate">DateFrom</param>
        /// <param name="p_To_Date">DateTo</param>
        /// <param name="p_OrderBooker_ID">User</param>
        /// <returns>DataSet</returns>
        public DataSet SelectOrderBookerWiseSale(int p_Distributor_ID, int p_Principal_Id, int p_OrderBooker_ID, DateTime p_FromDate, DateTime p_To_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspRptOrderBookerWiseSale ObjPrint = new uspRptOrderBookerWiseSale();
                SAMSBusinessLayer.Reports.dsAccount ds = new SAMSBusinessLayer.Reports.dsAccount();
                ObjPrint.Connection = mConnection;
                ObjPrint.distributor_id = p_Distributor_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.ORDERBOOKER_ID = p_OrderBooker_ID;
                ObjPrint.DateFrom = p_FromDate;
                ObjPrint.dateto = p_To_Date;
                DataTable dt = ObjPrint.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["RptOrderBookerWiseSale"].ImportRow(dr);
                }
                return ds;
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

     
     
        #region Transfer In/Out Report

        /// <summary>
        /// Gets Data For Transfer In/Out Report (In Value)
        /// </summary>
        /// <param name="p_Principal_ID">Principal</param>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_FromTime">DateFrom</param>
        /// <param name="p_ToDate">DateTo</param>
        /// <param name="p_TransferType">Type</param>
        /// <param name="p_type">ReportTyp</param>
        /// <returns>DataSet</returns>
        public DataSet TransferInOutValue(int p_Principal_ID, int p_Distributor_ID, DateTime p_FromTime, DateTime p_ToDate, int p_OrderBooker_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SAMSBusinessLayer.Reports.dsAccount ds = new SAMSBusinessLayer.Reports.dsAccount();

                uspRptOrderBookerWiseSale mTransferIn = new uspRptOrderBookerWiseSale();
                mTransferIn.Connection = mConnection;

                mTransferIn.PRINCIPAL_ID = p_Principal_ID;
                mTransferIn.distributor_id = p_Distributor_ID;
                mTransferIn.DateFrom = p_FromTime;
                mTransferIn.dateto = p_ToDate;
                mTransferIn.ORDERBOOKER_ID = p_OrderBooker_ID;
                DataTable DT = mTransferIn.ExecuteTable();

                foreach (DataRow dr in DT.Rows)
                {
                    ds.Tables["RptOrderBookerWiseSale"].ImportRow(dr);
                }
                return ds;
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

      
       
        #endregion

       

     

        #endregion
    }
}
