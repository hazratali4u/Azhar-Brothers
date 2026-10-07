<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVendorAdd.aspx.cs" Inherits="Forms_frmVendorAdd" Title="SAMS :: Vendor Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <style>
        #right #right_data fieldset legend {
            width: 120px;
        }

        .ajax__tab_xp .ajax__tab_body {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #525252;
        }
    </style>
    <script language="JavaScript" type="text/javascript">
       
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="upCustomer" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <strong>
                                <asp:Label Style="font-size: 14px;" runat="server" ID="lblVendorInfo"></asp:Label></strong>
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" GroupingText="Opening Balance" Width="100%">
                                        <br />
                                        <table width="80%">
                                            <tr>
                                                <td style="width: 55%" valign="top">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 30%">
                                                                <strong>Location</strong>
                                                            </td>
                                                            <td style="width: 5%"></td>
                                                            <td style="width: 65%">
                                                                <asp:DropDownList ID="drpDistributor" runat="server" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="true" Width="202px" CssClass="DropList">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                           <tr>
                                                            <td style="width: 30%">
                                                                <strong>Principal</strong>
                                                            </td>
                                                            <td style="width: 5%"></td>
                                                            <td style="width: 65%">
                                                                <asp:DropDownList ID="DrpPrincipal" runat="server" Width="202px" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" AutoPostBack="true" CssClass="DropList">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%">
                                                                <strong>Type</strong>
                                                            </td>
                                                            <td style="width: 5%"></td>
                                                            <td style="width: 65%">
                                                                <asp:RadioButtonList ID="rblOpening" runat="server" Width="100%" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="0" Text="Credit" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="Debit"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%">
                                                                <strong>Date</strong>
                                                            </td>
                                                            <td style="width: 5%"></td>
                                                            <td style="width: 65%">
                                                                <asp:TextBox ID="txtOpeningDate" runat="server" Width="200px"></asp:TextBox>
                                                                <asp:ImageButton ID="ibOpeningDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                                                                <cc1:CalendarExtender ID="ceOpeningDate" runat="server" TargetControlID="txtOpeningDate"
                                                                    PopupButtonID="ibOpeningDate" Format="dd-MMM-yyyy">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%">
                                                                <strong>Opening Balance</strong>
                                                            </td>
                                                            <td style="width: 5%"></td>
                                                            <td style="width: 65%">
                                                                <asp:TextBox ID="txtOpeningBalance" runat="server" Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%">
                                                                <strong>Remarks</strong>
                                                            </td>
                                                            <td style="width: 5%"></td>
                                                            <td style="width: 65%">
                                                                <asp:TextBox ID="txtOpeningBalanceRemarks" runat="server" TextMode="MultiLine" Width="196px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" align="center">
                                                                <br />
                                                                <asp:Button ID="btnSaveOpeningBalance" runat="server" Font-Size="8pt" OnClick="btnOpeningBalance_Click"
                                                                    CssClass="Button" Text="Save" Width="85px" />
                                                                <asp:Button ID="btnCancelOpeningBalance" runat="server" Font-Size="8pt" OnClick="btnCancelOpeningBalance_Click"                                                                    CssClass="Button" Text="Cancel" Width="85px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 45%" valign="middle"></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
