<%@ Page Title="SAMS :: Order/Invoice Step 2" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">

    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/jscript"></script>    
    <script src="JSFiles/OrderEntry.js" type="text/javascript"></script>
<style>
    #mask {
  position: fixed;
  left: 0;
  top: 0;
  z-index: 9000;
  background-color: grey;
  display: none;
}

#boxes .window {
  position: absolute;
  left: 0;
  top: 0;
  width: 440px;
  height: 200px;
  display: none;
  z-index: 9999;
  padding: 20px;
  border-radius: 15px;
  text-align: center;
}

#boxes #dialog {
  width: 750px;
  height: 300px;
  padding: 10px;
  background-color: #ffffff;
  font-family: 'Segoe UI Light', sans-serif;
  font-size: 15pt;
}

#popupfoot {
  font-size: 16pt;
  position: absolute;
  bottom: 0px;
  width: 250px;
  left: 250px;
}
    </style>
<div id="boxes">
  <div id="mask"></div>
</div>

<div id="right_data">    
<table width="85%">
    <tr>
        <td style="width:50%">
            <div>
                <span class="heading">Order/Invoice Step 2</span>                        
            </div>
        </td>
        <td style="width:40%" align="right">
            <div>
                <span class="heading">Working Date: <asp:Label ID="lblOrderDate" runat="server" Text="abc"></asp:Label> </span>            
            </div>
        </td>
        <td style="width:10%">
                
        </td>
    </tr>
</table>
<table width="70%" cellpadding="4" cellspacing="4">
        <tr>
            <td>
                <strong>Invoice #</strong>
            </td>
            <td>                
                <select id="drpDocumentNo" style="width:231px">                    
                </select>                
            </td>
            <td>
                <input type="checkbox" id="ChbDiscount" checked="checked" style="width:90px;"/>
            </td>
        </tr>
        <tr>
            <td>
                <strong>Invoice Type</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RblPayMode" runat="server" Width="228px" Height="1px" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="214">Cash</asp:ListItem>
                    <asp:ListItem Value="215">Credit</asp:ListItem>
                    <asp:ListItem Value="216">Advance</asp:ListItem>
                </asp:RadioButtonList>                
            </td>
            <td>
                <asp:CheckBox ID="ChbBatchNo" runat="server" Width="94px" Text="Batch No" Visible="false"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td>
                <strong>Area/Saleman</strong>
            </td>
            <td>                
                <input type="text" id="txtprincipal" style="width:295px" readonly="readonly" />
            </td>
            <td>                
                <input type="text" id="txtDeliveryMan" style="width:180px" readonly="readonly" />
            </td>
        </tr>
        <tr>
            <td>
                <strong>Customer</strong>
            </td>
            <td>
                <asp:DropDownList ID="drpCustomer" runat="server" Width="299px">
                </asp:DropDownList>
            </td>
            <td>
                <input id="txtDiscountType" readonly="readonly" style="width:180px" />
            </td>
        </tr>
        <tr>
            <td>
                <strong><label style="width: 77px;">Remarks</label></strong>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtRemarks" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table width="100%">
                    <tr>
                        <td style="width:15%"></td>
                        <td style="width:35%">
                            <strong>Available Stock:</strong>
                            <asp:TextBox ID="txtStock" runat="server" Text="0-0" Enabled="false" Width="100px"></asp:TextBox>                
                        </td>
                        <td style="width:13%"><strong>Discount In:</strong></td>
                        <td style="width:37%">                            
                            <asp:RadioButtonList ID="rbExtraDiscountType" runat="server" Width="100px" Height="1px"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">%</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True" >Value</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>>
        </tr>
    </table>
<table width="90%">
    <tr>
        <td style="width: 10%" class="lblDetail">
            <strong style="color: White;">SKU Description</strong>
        </td>
        <td style="width: 8%" class="lblDetail">
            <strong style="color: White;">Ctn</strong>
        </td>
        <td style="width: 8%" class="lblDetail">
            <strong style="color: White;">Unit</strong>
        </td>
        <td style="width: 8%" class="lblDetail">
            <strong style="color: White;">Unit Price</strong>
        </td>
        <td style="width: 8%" class="lblDetail">
            <strong style="color: White;">Amount</strong>
        </td>
        <td style="width: 8%" class="lblDetail">
            <strong style="color: White;">Disc.(%)</strong>
        </td>
        <td style="width: 8%" class="lblDetail">
            <strong style="color: White;">Disc.Value</strong>
        </td>
        <td style="width: 8%" class="lblDetail">
            <strong style="color: White;">Net Value</strong>
        </td>
        <td style="width: 8%" colspan="2">
        </td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList ID="ddlSKuCde" runat="server" Width="302px">
            </asp:DropDownList>
        </td>
        <td>
            <asp:TextBox ID="txtCtn" runat="server" Width="100%"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                FilterType="Custom" ValidChars="01234567890." TargetControlID="txtCtn">
            </ajaxToolkit:FilteredTextBoxExtender>
        </td>
        <td>
            <asp:TextBox ID="txtQuantity" runat="server" Width="100%"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                FilterType="Custom" ValidChars="01234567890." TargetControlID="txtQuantity">
            </ajaxToolkit:FilteredTextBoxExtender>
        </td>
        <td>
            <asp:TextBox ID="txtUnitRate" runat="server" Width="100%" Enabled="False"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtAmount" runat="server" Enabled="False" Width="100%"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtExtDiscount" runat="server" Width="100%"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                FilterType="Custom" ValidChars="01234567890." TargetControlID="txtExtDiscount">
            </ajaxToolkit:FilteredTextBoxExtender>
        </td>
        <td>
            <asp:TextBox ID="txtExtDiscountValue" runat="server" Width="100%" Enabled="false"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                FilterType="Custom" ValidChars="01234567890." TargetControlID="txtExtDiscountValue">
            </ajaxToolkit:FilteredTextBoxExtender>
        </td>
        <td>
            <asp:TextBox ID="txtNetValue" runat="server" Width="100%" Enabled="false"></asp:TextBox>
        </td>
        <td>
            <asp:DropDownList runat="server" ID="drpReturnType" Visible="false">
                <asp:ListItem Value="1" Text="Expiry"></asp:ListItem>
                <asp:ListItem Value="2" Text="Damage"></asp:ListItem>
                <asp:ListItem Selected="True" Value="3" Text="Saleable"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td colspan="2">                                               
            <input type="button" id="btnSave" value="Add" style="width:100%" class="Button" />
        </td>
    </tr>
</table>         
<asp:Panel ID="Panel2" runat="server" Height="130px" ScrollBars="Vertical" Width="90%"
    BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
    <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" BackColor="White"
        HorizontalAlign="Center" BorderColor="White" ShowHeader="False"        
        Width="100%">
        <Columns>
            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                <ItemStyle CssClass="HidePanel"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                <ItemStyle HorizontalAlign="Left" CssClass="grdDetail" Width="10%"></ItemStyle>
            </asp:BoundField>            
        </Columns>
        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
    </asp:GridView>
</asp:Panel>
</div>
</asp:Content>