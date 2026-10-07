<%@ WebHandler Language="C#" Class="OrderEntryHandler" %>

using System;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public class OrderEntryHandler : IHttpHandler, IRequiresSessionState
{
    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    Dictionary<string, object> row;

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string method = context.Request.QueryString["MethodName"].ToString();
        context.Response.ContentType = "text/json";
        switch (method)
        {
            case "GetSessionData":
                context.Response.Write(GetSessionData(context));
                break;
            case "LoadCustomerData":
                context.Response.Write(LoadCustomerData(context));
                break;
            case "LoadSKUDetail":
                context.Response.Write(LoadSKUDetail(context));
                break;
            case "LoadPendingOrder":
                context.Response.Write(LoadPendingOrder(context));
                break;
        }
    }
    protected string GetSessionData(HttpContext context)
    {
        DataTable dtSessionData = new DataTable();
        dtSessionData.Columns.Add("OrderNo", typeof(string));
        dtSessionData.Columns.Add("Route", typeof(string));
        dtSessionData.Columns.Add("SaleMan", typeof(string));
        dtSessionData.Columns.Add("OrderDate", typeof(string));

        DataRow sessionRow = dtSessionData.NewRow();
        sessionRow["OrderNo"] = context.Session["OrderNo"].ToString();
        sessionRow["Route"] = context.Session["Route"].ToString();
        sessionRow["SaleMan"] = context.Session["SaleMan"].ToString();
        sessionRow["OrderDate"] = context.Session["OrderDate"].ToString();
        dtSessionData.Rows.Add(sessionRow);

        foreach (DataRow dr in dtSessionData.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dtSessionData.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
    protected string LoadPendingOrder(HttpContext context)
    {
        OrderEntryController or = new OrderEntryController();
        int OrderNo = (int)context.Session["OrderNo"];
        DataTable dtOrder = new DataTable();
        if (OrderNo == -2)
        {
            dtOrder = or.SelectPendingOrder(int.Parse(context.Session["DistributorId"].ToString()), int.Parse(context.Session["AreaId"].ToString()), Constants.IntNullValue, int.Parse(context.Session["OrderBookerId"].ToString()), int.Parse(context.Session["DeliveryManId"].ToString()), Constants.Order_Pending_Id, Constants.IntNullValue, int.Parse(context.Session["UserId"].ToString()), Convert.ToDateTime(context.Session["OrderDate"]), 2);
        }
        else
        {
            dtOrder = or.SelectPendingOrder(int.Parse(context.Session["DistributorId"].ToString()), int.Parse(context.Session["AreaId"].ToString()), Constants.IntNullValue, int.Parse(context.Session["OrderBookerId"].ToString()), int.Parse(context.Session["DeliveryManId"].ToString()), Constants.Order_Pending_Id, Constants.IntNullValue, int.Parse(context.Session["UserId"].ToString()), Convert.ToDateTime(context.Session["OrderDate"]), 0);
        }

        foreach (DataRow dr in dtOrder.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dtOrder.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    protected string LoadSKUDetail(HttpContext context)
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(context.Session["DistributorId"].ToString()), int.Parse(context.Session["UserId"].ToString()), Constants.IntNullValue, 4, DateTime.Parse(context.Session["CurrentWorkDate"].ToString()));
        foreach (DataRow dr in Dtsku_Price.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in Dtsku_Price.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
    protected string LoadCustomerData(HttpContext context)
    {
        CustomerDataController mController = new CustomerDataController();
        DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(context.Session["DistributorId"].ToString()), int.Parse(context.Session["AreaId"].ToString()), Constants.IntNullValue, Constants.IntNullValue);

        foreach (DataRow dr in dtCustomer.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dtCustomer.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        if (rows.Count > 0)
        {
            return serializer.Serialize(rows);
        }
        else
        {
            return "";
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}