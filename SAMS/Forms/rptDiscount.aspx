<%@ Page Title="SAMS :: Customer Wise Discount & Free SKU Report" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="rptDiscount.aspx.cs" Inherits="Forms_rptDiscount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
        <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
        <script language="JavaScript" type="text/javascript">
            function pageLoad() {
                $("select").searchable();

                var $checkBoxAll = $('[id$="cbAll"]'); // assign main checkbox into a variable
                var $ChkBoxlst = $("table.checkboxlst input:checkbox"); // assign all checkboxes in to a variable
                $checkBoxAll.click(function () { // trigger click event when main checkbox will be clicked
                    $ChkBoxlst
                    .attr('checked', $checkBoxAll // attr() method checks all checkboxes
                    .is(':checked'));
                });
                $ChkBoxlst.click( // trigger an event click
                function (e) {
                    if (!$(this)[0].checked) { // check if all checkboxes is checked
                        $checkBoxAll.attr("checked", false); // un check the all checkbxes using attr() method, passing false as a second parameter
                    }
                });

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
        </script>
        <table width="100%">
            <tr>
                <td>
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td style="width: 10%">
                                        <strong>Location </strong>
                                    </td>
                                    <td style="width: 90%">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                            OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <strong>Principal </strong>
                                    </td>
                                    <td style="width: 90%">
                                        <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                            OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellspacing="2" cellpadding="5">
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
                                        <asp:CheckBox ID="cbAll" runat="server" Text="SKU" BorderColor="Silver" BorderWidth="1"
                                            Checked="true"/>
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
                            <table width="100%">
                                <tr>
                                    <td style="width: 10%">
                                        <strong>Customer Area </strong>
                                    </td>
                                    <td style="width: 90%">
                                        <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <strong>Customer </strong>
                                    </td>
                                    <td style="width: 90%">
                                        <asp:DropDownList ID="DrpCustomer" runat="server" Width="200px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <strong>From Date </strong>
                                    </td>
                                    <td style="width: 90%">
                                        <asp:TextBox ID="txtStartDate" runat="server" MaxLength="10" Width="150px"></asp:TextBox>
                                        <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" />
                                        <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                            TargetControlID="txtStartDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <strong>To Date </strong>
                                    </td>
                                    <td style="width: 90%">
                                        <asp:TextBox ID="txtEndDate" runat="server" MaxLength="10" Width="150px"></asp:TextBox>
                                        <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" />
                                        <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                            TargetControlID="txtEndDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                    </td>
                                    <td style="width: 90%">
                                        <asp:Button ID="btnViewPDF" runat="server" Width="90" Text="View PDF" OnClick="btnViewPDF_Click"
                                            CssClass="Button" />
                                        <asp:Button ID="btnViewExcel" runat="server" Width="90" Text="View Excel" OnClick="btnViewExcel_Click"
                                            CssClass="Button" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnViewPDF" />
                            <asp:PostBackTrigger ControlID="btnViewExcel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
