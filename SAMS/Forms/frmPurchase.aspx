<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchase.aspx.cs" Inherits="Forms_frmPurchase" Title="SAMS :: Purchase " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {

            $("select").searchable();
        }


        function ValidateForm() {
            var str;


            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            var str2 = document.getElementById('<%=txtCtn.ClientID%>').value;
            if ((str == null || str.length == 0) && (str2 == null || str2.length == 0)) {
                alert('Must Enter Quantity');
                return false;
            }

            str = document.getElementById('<%=txtDocumentNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Invoice/DC No');
                return false;
            }

            return true;
        }
    </script>
    <script type="text/javascript" language="javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            var ddlName = oControl.name;
            if (oControl.value == "Save Document"
           || oControl.value == "Add Sku") {
                oControl.value = "Wait...";
                oControl.disabled = true;
            }
        }
    </script>
    <div id="right_data">
        <div style="z-index: 101; left: 655px; width: 100px; position: absolute; top: 70px; height: 100px">
            <asp:Panel ID="Panel1" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                    <ProgressTemplate>
                        &nbsp;<asp:ImageButton ID="btnImage" runat="server" Height="33px" Width="31px" ImageUrl="~/App_Themes/Granite/Images/image003.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="height: 24px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" CssClass="lblbox" Height="14px" Text="Transaction Type"
                                                        Width="113px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 24px">
                                                <asp:DropDownList ID="DrpDocumentType" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged" Width="200px">
                                                    <asp:ListItem Value="2">Purchase</asp:ListItem>
                                                    <%-- <asp:ListItem Value="5">Transfer Out</asp:ListItem>
                                                    <asp:ListItem Value="3">Purchase Return</asp:ListItem>
                                                    <asp:ListItem Value="4">Transfer In</asp:ListItem>
                                                    <asp:ListItem Value="6">Damage</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 24px">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="5px"></asp:Label></strong>
                                            </td>
                                            <td style="width: 316px;" align="center" colspan="1" rowspan="8" valign="middle">
                                                <asp:Panel ID="pnlTotal" runat="server" Width="300px" Height="100%" BorderWidth="1px">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label6" runat="server" Height="16px" Text="Gross Amount"></asp:Label></strong>


                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTotalGrossAmount2" runat="server" Width="150px"
                                                                    ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label14" runat="server" Height="16px" Text="Item Wise Discount"></asp:Label></strong>


                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTotalPurDiscount2" runat="server" Width="150px"
                                                                    ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label15" runat="server" Height="16px" Text="Invoice Discount"></asp:Label></strong>


                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTotalDiscount" runat="server" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label13" runat="server" Height="16px" Text="Freight Amount"></asp:Label></strong>


                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtFreightAmount" runat="server" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label12" runat="server" Height="16px" Text="Net Amount"></asp:Label></strong>


                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtNetAmount" runat="server" Width="150px"
                                                                    ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" Width="109px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Principal" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpPrincipal" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 25px">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" CssClass="lblbox" Text="Purchase For"
                                                        Width="94px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" CssClass="lblbox" Text="Transfer To" Visible="False"
                                                        Width="82px"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpTransferFor" runat="server" CssClass="DropList" Visible="False"
                                                    Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="INV/DC  No" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Remarks" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBuiltyNo" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" valign="top" align="left"></td>
                                            <td style="height: 25px; width: 1px;" valign="top">
                                                <asp:CheckBox ID="ChbFreeSKU" runat="server" Width="121px" Text="Apply Free SKU"
                                                    AutoPostBack="True" OnCheckedChanged="ChbFreeSKU_CheckedChanged"></asp:CheckBox>
                                            </td>
                                            <td style="width: 1px; height: 25px" valign="top"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel5" runat="server" DefaultButton="btnSave">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblskuCode" runat="server" Width="100%" Text="  SKU Description" CssClass="lblDetail"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Width="100%" Text="Ctn" CssClass="lblDetail"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblquantity" runat="server" Width="100%" Text="Unit" CssClass="lblDetail"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFreeSKU" runat="server" Width="100%" Text="Free SKU" CssClass="lblDetail"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" Width="100%" Text="Discount" CssClass="lblDetail"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Width="100%" Text="Amount" CssClass="lblDetail"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" Width="100%" Text="Expiry Date" CssClass="lblDetail"></asp:Label>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlSKuCde" runat="server" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCtn" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQuantity" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFreeSKU" runat="server" Width="70px" CssClass="txtBox" Enabled="False">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurDiscount" runat="server" Width="70px" CssClass="txtBox">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAmount" runat="server" Width="75px" ReadOnly="true" CssClass="txtBox"></asp:TextBox>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExpiryDate" runat="server" Width="75px" CssClass="txtBox"></asp:TextBox>
                                                    <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                        Width="16px" />
                                                </td>
                                                <td>
                                                    <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="90px"
                                                        Font-Size="8pt" Text="Add Sku" CssClass="Button" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
                                                    <script runat="server">


                                                    </script>

                                                    <ajaxToolkit:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy"
                                                        PopupButtonID="ibtnStartDate" TargetControlID="txtExpiryDate">
                                                    </ajaxToolkit:CalendarExtender>



                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="8">
                                                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="130px" ScrollBars="Vertical"
                                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                        <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                            BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                            OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                                            ShowHeader="False" Width="100%">
                                                            <RowStyle ForeColor="Black" />
                                                            <Columns>
                                                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="50px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="190px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="QuantityCtn" HeaderText="Ctn">
                                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="FREE_SKU" HeaderText="Free SKU">
                                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PUR_DISCOUNT" HeaderText="Pur.Discount" DataFormatString="{0:n}">
                                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="BATCH_NO" HeaderText="Batch No">
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" DataFormatString="{0:n}">
                                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EXPIRY_DATE" HeaderText="Expiry" DataFormatString="{0:dd-MMM-yyyy}">
                                                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                                </asp:BoundField>

                                                                <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                                    <ItemStyle CssClass="grdDetail" Width="40px" HorizontalAlign="Center" />
                                                                </asp:CommandField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                            Text="Delete"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grdDetail" Width="45px" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="White" />
                                                            <PagerStyle BackColor="Transparent" />
                                                            <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                                VerticalAlign="Middle" />
                                                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="right">
                                                    <strong>
                                                        <asp:Label ID="Label7" runat="server" Height="16px" Text="Total:"></asp:Label></strong>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTotalCtn" runat="server" Width="70px"
                                                        ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTotalQuantity" runat="server" Width="70px"
                                                        ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTotalFreeSku" runat="server" Width="70px"
                                                        ReadOnly="True"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtTotalPurDiscount" runat="server" Width="70px"
                                                        ReadOnly="True"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtTotalGrossAmount" runat="server" Width="70px"
                                                        ReadOnly="True"></asp:TextBox></td>

                                                <td></td>
                                            </tr>

                                            </tr>
                                            <tr>

                                                <td colspan="3">
                                                    <asp:Button AccessKey="S" ID="btnCalcualte" runat="server" Width="100px" Font-Size="8pt"
                                                        Text="Calculate" UseSubmitBehavior="False" OnClick="btnCalcualte_Click"
                                                        CssClass="Button" />
                                                    <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="110px" Font-Size="8pt"
                                                        Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                                                        CssClass="Button" />
                                                    <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="100px" Font-Size="8pt"
                                                        Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                                                </td>

                                                <td colspan="2" align="right"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="3"></td>

                                                <td colspan="4"></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
