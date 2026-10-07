using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
namespace SAMSDatabaseLayer.Classes
{
	public class spInsertVENDOR_OWNER
	{
		#region Private Members
		private string sp_Name = "spInsertVENDOR_OWNER" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private DateTime m_CNIC_EXPIRY;
        private long m_VENDOR_ID;
		private string m_OWNER_NAME;
		private string m_FATHER_NAME;
		private string m_RESIDENTIAL_ADDRESS;
		private string m_CNIC_NO;
		private string m_ADDRESS;
		private string m_EMAIL_ADDRESS;
		private string m_CONTACT_NO;
		private string m_MOBILE_NO;
		private string m_PTCL;
		private string m_PIC;
		#endregion
		#region Public Properties
		public DateTime CNIC_EXPIRY
		{
			set
			{
				m_CNIC_EXPIRY = value ;
			}
			get
			{
				return m_CNIC_EXPIRY;
			}
		}
        public long VENDOR_ID
		{
			set
			{
                m_VENDOR_ID = value;
			}
			get
			{
                return m_VENDOR_ID;
			}
		}
		public  string OWNER_NAME
		{
			set
			{
				m_OWNER_NAME = value ;
			}
			get
			{
				return m_OWNER_NAME;
			}
		}
		public  string FATHER_NAME
		{
			set
			{
				m_FATHER_NAME = value ;
			}
			get
			{
				return m_FATHER_NAME;
			}
		}
		public  string RESIDENTIAL_ADDRESS
		{
			set
			{
				m_RESIDENTIAL_ADDRESS = value ;
			}
			get
			{
				return m_RESIDENTIAL_ADDRESS;
			}
		}
		public  string CNIC_NO
		{
			set
			{
				m_CNIC_NO = value ;
			}
			get
			{
				return m_CNIC_NO;
			}
		}
		public  string ADDRESS
		{
			set
			{
				m_ADDRESS = value ;
			}
			get
			{
				return m_ADDRESS;
			}
		}
		public  string EMAIL_ADDRESS
		{
			set
			{
				m_EMAIL_ADDRESS = value ;
			}
			get
			{
				return m_EMAIL_ADDRESS;
			}
		}
		public  string CONTACT_NO
		{
			set
			{
				m_CONTACT_NO = value ;
			}
			get
			{
				return m_CONTACT_NO;
			}
		}
		public  string MOBILE_NO
		{
			set
			{
				m_MOBILE_NO = value ;
			}
			get
			{
				return m_MOBILE_NO;
			}
		}
		public  string PTCL
		{
			set
			{
				m_PTCL = value ;
			}
			get
			{
				return m_PTCL;
			}
		}
		public  string PIC
		{
			set
			{
				m_PIC = value ;
			}
			get
			{
				return m_PIC;
			}
		}


		public IDbConnection  Connection
		{
			set
			{
				m_connection = value;
			}
			get
			{
				return m_connection;
			}
		}
		public IDbTransaction  Transaction
		{
			set
			{
				m_transaction = value;
			}
			get
			{
				return m_transaction;
			}
		}
		#endregion
		#region Constructor
		public spInsertVENDOR_OWNER()
		{
		}
		#endregion
		#region public Methods
		public bool  ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				cmd.CommandType =  CommandType.StoredProcedure;
				cmd.CommandText = sp_Name;
				cmd.Connection =   m_connection;
				if(m_transaction!=null)
				{
					cmd.Transaction = m_transaction;
				}
				GetParameterCollection(ref cmd);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch(Exception e)
			{
				throw e;
			}
			finally
			{
			}
		}
		public IDataReader ExecuteReader()
		{
			try
			{
				IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDataReader dr = command.ExecuteReader();
				return dr;
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public DataTable ExecuteTable()
		{
			try
			{
				IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDbDataAdapter da = ProviderFactory.GetAdapter(EnumProviders.SQLClient);
				da.SelectCommand = command;
				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds.Tables[0];
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public string ExecuteScalar()
		{
			try
			{
				IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				object o;
				o = command.ExecuteScalar();


				return o.ToString();
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public void FirstReader(IDataReader dr)
		{
			if(dr.Read())
			{
				m_CNIC_EXPIRY= Convert.ToDateTime(dr["CNIC_EXPIRY"]);
                m_VENDOR_ID = Convert.ToInt64(dr["VENDOR_ID"]);
				m_OWNER_NAME= Convert.ToString(dr["OWNER_NAME"]);
				m_FATHER_NAME= Convert.ToString(dr["FATHER_NAME"]);
				m_RESIDENTIAL_ADDRESS= Convert.ToString(dr["RESIDENTIAL_ADDRESS"]);
				m_CNIC_NO= Convert.ToString(dr["CNIC_NO"]);
				m_ADDRESS= Convert.ToString(dr["ADDRESS"]);
				m_EMAIL_ADDRESS= Convert.ToString(dr["EMAIL_ADDRESS"]);
				m_CONTACT_NO= Convert.ToString(dr["CONTACT_NO"]);
				m_MOBILE_NO= Convert.ToString(dr["MOBILE_NO"]);
				m_PTCL= Convert.ToString(dr["PTCL"]);
				m_PIC= Convert.ToString(dr["PIC"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CNIC_EXPIRY" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_CNIC_EXPIRY==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CNIC_EXPIRY;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@VENDOR_ID"; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_VENDOR_ID == Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
                parameter.Value = m_VENDOR_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@OWNER_NAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_OWNER_NAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_OWNER_NAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@FATHER_NAME" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_FATHER_NAME== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_FATHER_NAME;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@RESIDENTIAL_ADDRESS" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_RESIDENTIAL_ADDRESS== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_RESIDENTIAL_ADDRESS;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CNIC_NO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CNIC_NO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CNIC_NO;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ADDRESS" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_ADDRESS== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ADDRESS;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@EMAIL_ADDRESS" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_EMAIL_ADDRESS== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_EMAIL_ADDRESS;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CONTACT_NO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_CONTACT_NO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CONTACT_NO;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@MOBILE_NO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_MOBILE_NO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_MOBILE_NO;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PTCL" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_PTCL== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PTCL;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PIC" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_PIC== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PIC;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
