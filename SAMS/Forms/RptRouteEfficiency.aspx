<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptRouteEfficiency.aspx.cs" Inherits="Forms_RptRouteEfficiency" Title="SAMS :: Area Efficiency Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
        <script language="JavaScript" type="text/javascript">
            function pageLoad() {
               $("select").searchable();

            }
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);
            function BeginRequestHandler(sender, args) {
                //Shows the modal popup - the update progress
                var popup = $find('<%= modalPopup.ClientID %>');
                if (popup != null) {
                    popup.show();
                }
            }

            function EndRequestHandler(sender, args) {
                //Hide the modal popup - the update progress
                var popup = $find('<%= modalPopup.ClientID %>');
                if (popup != null) {
                    popup.hide();
                }
            }

            function Validateddl() {
                var LocationText = $('#<%=drpDistributor.ClientID %> option:selected').text();
                var PrincipalText = $('#<%=DrpPrincipal.ClientID %> option:selected').text();
                var OBText = $('#<%=drpSaleForce.ClientID %> option:selected').text();
                if (LocationText == '' || PrincipalText == '' || OBText == '') {
                    alert('Please select Location, Principal or Orderbooker.');
                    return false;
                }
                else {
                    return true;
                }
            }
        </script>
    <div id="right_data">
        <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                        Width="31px" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
        </div>
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 81px" align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" Width="73px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 81px" align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Width="78px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                 </tbody>
                            </table>
                            <asp:Panel ID="pnlDetail" runat="server"  >
                            <table width="100%" id="tbldetail" cellspacing="2" cellpadding="5">
                                <tr>
                                    <td style="width: 40%">
                                        <asp:CheckBox ID="cbAllCategory" runat="server" Text="Category" BorderColor="Silver"
                                            BorderWidth="1" Checked="true" AutoPostBack="true" OnCheckedChanged="cbAllCategory_CheckedChanged" />
                                        <asp:Panel ID="Panel2" runat="server" Width="90%" Height="240px" ScrollBars="Vertical"
                                            BorderStyle="Groove" BorderWidth="1px">
                                            <asp:CheckBoxList ID="cblCategory" runat="server" Width="100%" AutoPostBack="true"
                                                OnSelectedIndexChanged="cblCategory_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 40%">
                                        <asp:CheckBox ID="cbAll" OnCheckedChanged="cbAll_CheckedChanged" AutoPostBack="true" runat="server" Text="SKU" BorderColor="Silver" BorderWidth="1"
                                            Checked="true" on/>
                                        <asp:Panel ID="Panel3" runat="server" Width="90%" Height="240px" ScrollBars="Vertical"
                                            BorderStyle="Groove" BorderWidth="1px">
                                            <asp:CheckBoxList ID="cblSKU" runat="server" Width="90%" CssClass="checkboxlst">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 20%">
                                    </td>
                                </tr>
                            </table>
                               </asp:Panel>
                            <table>
                                <tbody>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 81px" align="left">
                                            <strong>
                                                <asp:Label ID="lblOrderBooker" runat="server" Width="79px" Text="Order Booker"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpSaleForce" runat="server" Width="199px">                                                
                                            </asp:DropDownList>
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 81px" align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="70px" Height="13px" Text="From Date"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            &nbsp;<asp:TextBox ID="txtStartDate" onkeyup="BlockStartDateKeyPress()" runat="server"
                                                Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ibtnStartDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 81px" align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Width="80px" Height="13px" Text="To Date"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            &nbsp;<asp:TextBox ID="txtEndDate" onkeyup="BlockEndDateKeyPress()" runat="server"
                                                Width="150px" CssClass="txtBox " MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 81px" align="left">
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                            <cc1:CalendarExtender ID="CEStartDate" runat="server" TargetControlID="txtStartDate"
                                                PopupButtonID="ibtnStartDate" Format="dd-MMM-yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate"
                                                PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnViwPDF" runat="server" CssClass="Button" Text="View PDF" Width="90"
                        OnClick="btnViwPDF_Click" OnClientClick="return Validateddl();"/>
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                        Width="90" OnClick="btnViewExcel_Click"  OnClientClick="return Validateddl();"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
