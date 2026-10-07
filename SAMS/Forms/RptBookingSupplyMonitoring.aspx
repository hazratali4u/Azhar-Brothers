<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptBookingSupplyMonitoring.aspx.cs"
    Inherits="Forms_RptBookingSupplyMonitoring" MasterPageFile="~/Forms/PageMaster.master"
    Title="SAMS :: Booking vs Execution" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="100%" cellspacing="2" cellpadding="5">
                                <tr>
                                    <td style="width: 12%">
                                        <strong>
                                            <asp:Label ID="lblType" runat="server" Width="100%" Text="Report Type"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 88%">
                                        <asp:DropDownList ID="DrpReportType" runat="server" Width="200px" OnSelectedIndexChanged="DrpReportType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Selected="True" Value="0">Date Wise</asp:ListItem>
                                            <asp:ListItem Value="1">SKU Wise (Units)</asp:ListItem>
                                            <asp:ListItem Value="2">SKU Wise (Cartons)</asp:ListItem>
                                            <asp:ListItem Value="3">SKU Wise (Values)</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 12%">
                                        <strong>
                                            <asp:Label ID="Label4" runat="server" Width="100%" Text="Location"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 88%">
                                        <asp:DropDownList ID="DrpLocation" runat="server" Width="200px" OnSelectedIndexChanged="DrpLocation_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellspacing="2" cellpadding="5">
                                <tr>
                                    <td style="width:40%">
                                        <asp:CheckBox ID="cbAll" runat="server" Text="Principal" BorderColor="Silver" 
                                        BorderWidth="1"  Checked="true" AutoPostBack="True" 
                                        oncheckedchanged="cbAll_CheckedChanged" Enabled="false"/>
                                        <asp:Panel ID="Panel3" runat="server" Width="90%" Height="250px" ScrollBars="Vertical"
                                            BorderStyle="Groove" BorderWidth="1px">
                                        <asp:CheckBoxList ID="cblPrincipal" runat="server" Width="90%" AutoPostBack="True" 
                                                onselectedindexchanged="cblPrincipal_SelectedIndexChanged" Enabled="false">                                        
                                        </asp:CheckBoxList>
                                        </asp:Panel>
                                    </td>
                                    <td style="width:40%">
                                        <asp:CheckBox ID="cbAllCategory" runat="server" Text="Category" 
                                        BorderColor="Silver" BorderWidth="1"  Checked="true" AutoPostBack="True" 
                                        oncheckedchanged="cbAllCategory_CheckedChanged" Enabled="false"/>
                                        <asp:Panel ID="Panel2" runat="server" Width="90%" Height="250px" ScrollBars="Vertical"
                                            BorderStyle="Groove" BorderWidth="1px">
                                        <asp:CheckBoxList ID="cblCategory" runat="server" Width="100%" Enabled="false">                                        
                                        </asp:CheckBoxList>
                                        </asp:Panel>
                                    </td>
                                    <td style="width:20%"></td>
                                </tr>
                            </table>
                            <table width="100%" cellspacing="2" cellpadding="5">
                                <tr>
                                    <td style="width: 12%">
                                        <strong>
                                            <asp:Label ID="lblSaleForce" runat="server" Width="100%" Text="Orderbooker"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 88%">
                                        <asp:DropDownList ID="drpSaleForce" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 12%">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" Width="100%" Text="Sale Force"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 88%">
                                        <asp:DropDownList ID="DrpDeliveryMan" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 12%">
                                        <strong>
                                            <asp:Label ID="Label5" runat="server" Width="100%" Text="From Date"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 88%">
                                        <asp:TextBox ID="txtFromDate" runat="server" Width="150px" MaxLength="10"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBntFromCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                        </asp:ImageButton>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                            PopupButtonID="ImgBntFromCalc" Format="dd-MMM-yyyy" EnableViewState="False">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 12%">
                                        <strong>
                                            <asp:Label ID="Label6" runat="server" Width="100%" Text="From Date"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 88%">
                                        <asp:TextBox ID="txtToDate" runat="server" Width="150px" MaxLength="10"></asp:TextBox>
                                        <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                        </asp:ImageButton>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                            PopupButtonID="ImgToDate" Format="dd-MMM-yyyy" EnableViewState="False">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Text="View PDF" Width="90"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="Button1" runat="server" CssClass="Button" Text="View Excel" Width="90"
                        OnClick="btnViewExcel" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
