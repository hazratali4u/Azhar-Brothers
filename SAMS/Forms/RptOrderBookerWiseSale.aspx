<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptOrderBookerWiseSale.aspx.cs" Inherits="Forms_RptOrderBookerWiseSale" Title="SAMS :: Order Booker Wise Sale Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();

        }
    </script>
   <%-- <script language="JavaScript" type="text/javascript">
        function ValidateForm() {

            return true;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.cblPrincipal.ClientID %>');
            var TargetChildControl = "cbAll";


            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)

                if (Inputs[n].type == 'checkbox')

                    if (!Inputs[n].disabled) {
                        Inputs[n].checked = CheckBox.checked;
                    }
        }


        function CheckBoxListSelect(cbControl) {
            var chkBoxCount = cbControl.getElementsByTagName("input");
            var cbAll = document.getElementById('<%= this.cbAll.ClientID %>');

            var ListCount = 0;
            
            for (var i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked) {
                    ListCount++;
                }
            }

            if (ListCount > 0 && ListCount < chkBoxCount.length) {
                cbAll.checked = false;                
            }
            else {
                cbAll.checked = true;
            }
        }

    </script>--%>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width: 273px; height: 68px" id="TABLE1" onclick="return TABLE1_onclick()">
                                <tbody>
                                    <tr>
                                        <td style="height: 15px" align="left" colspan="4">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 1px; height: 15px" align="left" colspan="1"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 1px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 29px" align="left">
                                            <strong>
                                                <asp:Label ID="lblfromLocation" runat="server" CssClass="lblbox" Text="Location"
                                                    Width="60px"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:DropDownList ID="DrpDistributor" runat="server" AutoPostBack="True" CssClass="DropList" Width="200px">
                                            </asp:DropDownList></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                            <td align="left" style="width: 1px; height: 25px"></td>
                                            <td align="left" style="width: 29px; height: 25px">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="61px" Text="Principal" CssClass="lblbox"></asp:Label></strong></td>
                                            <td align="left" style="width: 1px; height: 25px"></td>
                                            <td align="left" style="width: 203px; height: 25px">
                                                <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList></td>
                                            <td align="left" style="width: 1px; height: 25px"></td>
                                        </tr>
                                    <tr>
                                        <td style="width: 1px" align="left"></td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="94px" Text="Order Booker" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                        <td style="width: 1px" align="left"></td>
                                            <td style="width: 238px; height: 25px" align="left">
                                                <asp:DropDownList ID="DrpOrderBooker" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                   <%-- <tr>
                                        <td align="left" style="width: 1px; height: 25px"></td>
                                        <td align="left" style="width: 29px; height: 25px">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" Width="61px" Text="Principal" CssClass="lblbox"></asp:Label></strong></td>
                                        <td align="left" style="width: 1px; height: 25px"></td>
                                        <td align="left" style="width: 203px; height: 25px">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList></td>
                                        <td align="left" style="width: 1px; height: 25px"></td>
                                    </tr>--%>

                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Height="13px" Text="From Date" Width="70px"></asp:Label></strong>
                                        </td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10" onkeyup="BlockStartDateKeyPress()"
                                                Width="178px"></asp:TextBox>
                                            <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label></strong>
                                        </td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">
                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10" Width="178px"></asp:TextBox>
                                            <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left"></td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">
                                            <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                                TargetControlID="txtStartDate">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                                TargetControlID="txtEndDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 5px" align="left"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp; &nbsp;
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Style="margin-left: 95px;"
                        Text="View PDF" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" CssClass="Button"
                        OnClick="btnViewExcel_Click" /></td>
            </tr>
        </table>
        <br />
        <br />
        <br />
    </div>
</asp:Content>
