<%@ page language="C#" masterpagefile="~/Forms/PageMaster.master" autoeventwireup="true" CodeFile = "frmPromotionWizardStep3Slab.aspx.cs" inherits="Forms_frmPromotionWizardStep3Slab" title="SAMS :: Promotion Wizard Step 3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" Runat="Server">
   
    <script language="JavaScript" type="text/javascript">
        function ValidateForm()
		{
			var str;
			if(document.getElementById('<%=rbSKUGroup.ClientID%>').checked == true)
			{
				str = document.getElementById('<%=ddSKUSelectedGroup.ClientID%>').value;
				if(str == null || str.length == 0)
				{
					alert('Must select SKU Group');
					return false;
				}
			}
						
			if(document.getElementById('<%=chIsMultiple.ClientID%>').checked == true)
			{
				str  = document.getElementById('<%=txtMultipleOf.ClientID%>').value;
				if(str == null || str.length == 0)
				{
					alert('Must enter Multiple of');
					return false;
				}
			}
			str  = document.getElementById('<%=txtFrom.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter From Range');
				return false;
			}
			str  = document.getElementById('<%=txtTo.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter To Range');
				return false;
			}
			if( !(document.getElementById('<%=chDiscount.ClientID%>').checked || document.getElementById('<%=chSKU.ClientID%>').checked) )
			{
				alert('Must Supply values either of Discount or SKU or both'); 
				return false;
			}
			if(document.getElementById('<%=chDiscount.ClientID%>').checked)
			{
			    if(document.getElementById('<%=rbtnDiscount.ClientID%>').index == 0)
				{
				    str = document.getElementById('<%=txtDiscountRate.ClientID%>').value;
					if(str == null || str.length == 0)
					{
						alert('Must enter Discount Rate');
						return false;
					}
					var num = parseFloat(str);
					if(num >= 100.0)
					{
						alert('Discount Rate must not be greater than 100');
						return false;
					}
				}
				if(document.getElementById('<%=rbtnDiscount.ClientID%>').index == 1)
				{
					str = document.getElementById('<%=txtDiscountAmount.ClientID%>').value;
					if(str == null || str.length == 0)
					{
						alert('Must enter Discount Amount');
						return false;
					}
				} 
			}
			return true;			
		}
</script>

<div id="right_data">
    
    <div >
        <table width="100%">
        <tr>
            <td>
                <h2>Promotion Wizard Step 3</h2>
            </td>
        </tr>
            <tr>
                <td>
        <asp:UpdatePanel id="UpdatePanel2" runat="server">
            <contenttemplate>
<TABLE><TBODY><TR><TD align=left><asp:RadioButton id="rbSKUHierarchy" runat="server" Width="139px" Text="By SKU Hierarchy" AutoPostBack="True" Checked="True" OnCheckedChanged="rbSKUHierarchy_CheckedChanged"></asp:RadioButton></TD><TD align=left></TD><TD>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </TD><TD align=left colSpan=2>
<strong> <asp:Label id="lblPromotionOffer" runat="server" Width="120px" CssClass="entryErrorMessage"> Promotion Offer</asp:Label></strong></TD></TR><TR><TD style="HEIGHT: 20px" align=left>
<strong><asp:Label id="lblSKUCatagory" runat="server" Width="100px" Text="SKU Category" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:DropDownList id="ddSKUCatagory" runat="server" Width="175px" AutoPostBack="True" CssClass="DropList" OnSelectedIndexChanged="ddSKUCatagory_SelectedIndexChanged">
                                </asp:DropDownList></TD><TD></TD><TD align=left colSpan=2 rowSpan=9><asp:Panel id="Panel1" runat="server" Width="100%" Height="100%" BorderWidth="1px" BorderStyle="Groove" BorderColor="Gray"><TABLE><TBODY><TR><TD style="WIDTH: 100px"><asp:CheckBox id="chDiscount" runat="server" Width="95px" Font-Size="8pt" Text="Discount" AutoPostBack="True"></asp:CheckBox></TD><TD style="WIDTH: 100px"></TD></TR><TR><TD style="WIDTH: 100px" rowSpan=2><asp:RadioButtonList id="rbtnDiscount" runat="server" Width="128px" Height="34px" Font-Size="8pt" AutoPostBack="True" CssClass="lblbox" OnSelectedIndexChanged="rbtnDiscount_SelectedIndexChanged"><asp:ListItem Value="0">Discount Rate</asp:ListItem>
<asp:ListItem Value="1">Discount Amount</asp:ListItem>
</asp:RadioButtonList></TD><TD style="WIDTH: 100px"><asp:TextBox id="txtDiscountRate" runat="server" Width="168px" CssClass="txtBox" Enabled="False"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 100px"><asp:TextBox id="txtDiscountAmount" runat="server" Width="168px" CssClass="txtBox " Enabled="False"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 100px"><asp:CheckBox id="chSKU" runat="server" Width="49px" Font-Size="8pt" Text="SKU"></asp:CheckBox></TD><TD style="WIDTH: 100px"></TD></TR><TR><TD style="WIDTH: 100px">
<strong><asp:Label id="lblSKUCategory2" runat="server" Width="101px" CssClass="lblbox">SKU Category</asp:Label></strong></TD><TD style="WIDTH: 100px"><asp:DropDownList id="ddPromotionCatagory" runat="server" Width="174px" AutoPostBack="True" CssClass="DropList" OnSelectedIndexChanged="ddPromotionCatagory_SelectedIndexChanged">
                                                </asp:DropDownList></TD></TR><TR><TD style="WIDTH: 100px">
                                                <strong><asp:Label id="lblSKU2" runat="server" Width="61px" CssClass="lblbox">SKU</asp:Label></strong></TD><TD style="WIDTH: 100px"><asp:DropDownList id="ddPromotionSKU" runat="server" Width="174px" AutoPostBack="True" CssClass="DropList">
                                                </asp:DropDownList></TD></TR><TR><TD style="WIDTH: 100px">
                                                <strong><asp:Label id="lblQuantity2" runat="server" Width="99px" CssClass="lblbox"> Quantity</asp:Label></strong></TD><TD style="WIDTH: 100px"><asp:TextBox id="txtPromotionQuantity" runat="server" Width="168px" CssClass="txtBox "></asp:TextBox></TD></TR><TR><TD style="WIDTH: 100px"></TD><TD style="WIDTH: 100px"></TD></TR></TBODY></TABLE></asp:Panel> </TD></TR><TR><TD style="HEIGHT: 20px" align=left>
                                                <strong><asp:Label id="lblSKUBrand" runat="server" Width="100px" Text="SKU Brand" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 18px" align=left><asp:DropDownList id="ddSKUBrand" runat="server" Width="175px" AutoPostBack="True" CssClass="DropList" OnSelectedIndexChanged="ddSKUBrand_SelectedIndexChanged">
                                </asp:DropDownList></TD><TD style="HEIGHT: 18px"></TD></TR><TR><TD style="HEIGHT: 20px" align=left>
                                <strong><asp:Label id="lblSKU" runat="server" Width="100px" Text="SKU" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:DropDownList id="ddSKU" runat="server" Width="175px" CssClass="DropList"></asp:DropDownList></TD><TD></TD></TR><TR><TD align=left><asp:RadioButton id="rbSKUGroup" runat="server" Width="139px" Text="By SKU Group" AutoPostBack="True" OnCheckedChanged="rbSKUGroup_CheckedChanged"></asp:RadioButton></TD><TD align=left></TD><TD></TD></TR><TR><TD style="HEIGHT: 20px" align=left>
                                <strong><asp:Label id="lblSKUGroup" runat="server" Width="100px" Text="SKU Group" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:DropDownList id="ddSKUSelectedGroup" runat="server" Width="175px" CssClass="DropList" Enabled="False">
                                </asp:DropDownList></TD><TD></TD></TR><TR><TD style="HEIGHT: 20px" align=left>
                                <strong><asp:Label id="lblSlabOn" runat="server" Width="100px" Text="Slab On" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:DropDownList id="ddSlabOn" runat="server" Width="175px" AutoPostBack="True" CssClass="DropList" OnSelectedIndexChanged="ddSlabOn_SelectedIndexChanged">
                                    <asp:ListItem Value="82">Quantity</asp:ListItem>
                                    <asp:ListItem Value="83">Amount</asp:ListItem>
                                </asp:DropDownList></TD><TD></TD></TR><TR><TD style="HEIGHT: 20px" align=left><asp:CheckBox id="chIsMultiple" runat="server" Text="Is Multiple" AutoPostBack="True" OnCheckedChanged="chIsMultiple_CheckedChanged"></asp:CheckBox></TD><TD align=left><asp:TextBox id="txtMultipleOf" runat="server" Width="168px" CssClass="txtBox" Enabled="False"></asp:TextBox></TD><TD></TD></TR><TR><TD style="HEIGHT: 20px" align=left>
                                <strong><asp:Label id="lblfromquantity" runat="server" Width="100px" Text="From Quantity" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:TextBox id="txtFrom" runat="server" Width="168px" CssClass="txtBox"></asp:TextBox></TD><TD></TD></TR><TR><TD style="HEIGHT: 20px" align=left>
                                <strong><asp:Label id="lblToQuantity" runat="server" Width="100px" Text="To Quantity" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:TextBox id="txtTo" runat="server" Width="168px" CssClass="txtBox"></asp:TextBox></TD><TD></TD></TR><TR><TD style="HEIGHT: 10px" align=left></TD><TD align=left><cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789." FilterType="Custom" TargetControlID="txtFrom">
                            </cc1:FilteredTextBoxExtender> <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender3" runat="server" ValidChars="0123456789." FilterType="Custom" TargetControlID="txtDiscountAmount">
                            </cc1:FilteredTextBoxExtender> <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender5" runat="server" ValidChars="0123456789." FilterType="Custom" TargetControlID="txtMultipleOf">
                            </cc1:FilteredTextBoxExtender> </TD><TD>&nbsp;</TD><TD><cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender2" runat="server" ValidChars="0123456789." FilterType="Custom" TargetControlID="txtTo">
                            </cc1:FilteredTextBoxExtender> <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtPromotionQuantity">
                            </cc1:FilteredTextBoxExtender> <cc1:FilteredTextBoxExtender id="FilteredTextBoxExtender6" runat="server" ValidChars="0123456789." FilterType="Custom" TargetControlID="txtDiscountRate">
                            </cc1:FilteredTextBoxExtender> &nbsp; &nbsp;&nbsp; </TD><TD>&nbsp; </TD></TR><TR><TD align=right>
                            <asp:Button id="btnAddtoSlab" onclick="btnAddtoSlab_Click" runat="server" Width="90" CssClass="Button" Text="Add To Slab" ValidationGroup="vg"></asp:Button></TD><TD align=left>
                            <asp:Button id="btnCreateNewSlab" onclick="btnCreateNewSlab_Click" runat="server" Width="120" CssClass="Button" Text="Create New Slab" ValidationGroup="vg" CausesValidation="False"></asp:Button></TD><TD></TD><TD align=right>
                            <asp:Button id="btnBack" onclick="btnBack_Click" runat="server" Width="90px" CssClass="Button" Text="Back" ValidationGroup="vg" CausesValidation="False"></asp:Button>&nbsp;</TD><TD align=left>
                            <asp:Button id="btnNext" onclick="btnNext_Click" runat="server" Width="90px" CssClass="Button" Text="Next" ValidationGroup="vg" CausesValidation="False"></asp:Button></TD></TR></TBODY></TABLE>
</contenttemplate>
                    </asp:UpdatePanel>  </td>
            </tr>
        </table>
        
           </div>
    <div>
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <contenttemplate>
<asp:GridView id="grdSlab" runat="server" Width="94%" ForeColor="SteelBlue" CssClass="gridRow2" BorderColor="White" OnRowDeleting="grdSlab_RowDeleting" AutoGenerateColumns="False" HorizontalAlign="Center" BackColor="White" OnRowEditing="grdSlab_RowEditing">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>

<Columns>
<asp:BoundField DataField="SLAB_NO" HeaderText="SLAB_NO">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SLAB NO" HeaderText="Slab No">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Is_Multiple" HeaderText="Is Multiple"></asp:BoundField>
<asp:BoundField DataField="Multiple_of" HeaderText="Multiple Of">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU" HeaderText="SKU">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM" Visible="False">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Slab On" HeaderText="Slab On">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="From" HeaderText="From">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="To" HeaderText="To">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Discount" HeaderText="Discount">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU Offer" HeaderText="SKU Offer">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU Quantity" HeaderText="SKU Quantity">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:CommandField ShowEditButton="True" HeaderText="Edit">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:CommandField>
<asp:TemplateField HeaderText="Delete"><ItemTemplate>
<asp:LinkButton id="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;" CommandName="Delete"></asp:LinkButton> 
</ItemTemplate>

<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:TemplateField>
</Columns>
<HeaderStyle CssClass="tblhead"></HeaderStyle>
</asp:GridView> 
</contenttemplate>
        </asp:UpdatePanel>
        </div>

        </div>
	
</asp:Content>
