<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSKUGroups.aspx.cs" Inherits="frmSKUGroups" MasterPageFile="~/Forms/PageMaster.master" Title="SAMS :: SKU Group" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" Runat="Server">
<div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel id="UpdatePanel3" runat="server">
                        <contenttemplate>
<TABLE style="WIDTH: 404px"><TBODY><TR><TD></TD><TD><asp:Label id="lblErrormsg" runat="server" Width="262px" ForeColor="Red"></asp:Label></TD></TR><TR><TD style="WIDTH: 47px; HEIGHT: 32px" align=left>
<strong> <asp:Label id="Label1" runat="server" Width="72px" Text="Group Name" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:TextBox id="txtGroupName" runat="server" Width="194px" CssClass="txtBox "></asp:TextBox> <BR /><asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" Width="136px" ControlToValidate="txtGroupName" Display="Dynamic" ErrorMessage="Enter Group Name" ValidationGroup="vg"></asp:RequiredFieldValidator></TD></TR><TR><TD style="WIDTH: 47px; HEIGHT: 22px" align=left>
<strong><asp:Label id="Label5" runat="server" Width="51px" Text="Principal" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:DropDownList id="ddPrincipal" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="ddPrincipal_SelectedIndexChanged">
            </asp:DropDownList></TD></TR><TR><TD style="WIDTH: 47px; HEIGHT: 22px" align=left>
            <strong><asp:Label id="Label8" runat="server" Width="53px" Text="Division" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:DropDownList id="ddDivision" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="ddDivision_SelectedIndexChanged"></asp:DropDownList></TD></TR><TR><TD style="WIDTH: 47px; HEIGHT: 23px" align=left>
            <strong><asp:Label id="Label11" runat="server" Width="52px" Text="Catagory" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:DropDownList id="ddCatagory" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="ddCatagory_SelectedIndexChanged"></asp:DropDownList></TD></TR><TR><TD style="WIDTH: 47px; HEIGHT: 23px" align=left>
            <strong><asp:Label id="Label2" runat="server" Width="50px" Text="Brand" CssClass="lblbox"></asp:Label></strong></TD><TD align=left><asp:DropDownList id="ddBrand" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="ddBrand_SelectedIndexChanged"></asp:DropDownList></TD></TR><TR><TD align=center colSpan=2>
            <strong><asp:Label id="Label4" runat="server" Width="117px" Text="SKU Collection" CssClass="lblbox"></asp:Label></strong> &nbsp; </TD></TR><TR><TD style="HEIGHT: 115px" colSpan=2><TABLE><TBODY><TR><TD style="WIDTH: 107px" rowSpan=4><asp:ListBox id="lstUnAssignSKU" runat="server" Width="200px" Height="150px" CssClass="DropList"></asp:ListBox></TD><TD align="center">
            <asp:Button id="btnAddAll" runat="server" Width="30px" Font-Size="8pt" Text=">>" OnClick="btnAddAll_Click" CssClass="Button" /> </TD><TD style="WIDTH: 102px" rowSpan=4><asp:ListBox id="lstAssignSKU" runat="server" Width="200px" Height="150px" CssClass="DropList"></asp:ListBox></TD></TR><TR><TD align=center>
            <asp:Button id="btnAdd" runat="server" Width="30px" Font-Size="8pt" Text=">" OnClick="btnAdd_Click" CssClass="Button" /> </TD></TR><TR><TD align=center>
            <asp:Button id="btnRemove" runat="server" Width="30px" Font-Size="8pt" Text="<" OnClick="btnRemove_Click" CssClass="Button" /> </TD></TR><TR><TD align="center">
            <asp:Button id="btnRemoveAll" runat="server" Width="30px" Font-Size="8pt" Text="<<" OnClick="btnRemoveAll_Click" CssClass="Button" /> </TD></TR><TR><TD rowSpan=1><asp:CheckBox id="chIsActive" runat="server" Visible="False" Text="Is Active" CssClass="lblbox"></asp:CheckBox></TD><TD align=center>
            <asp:Button id="btnSave" onclick="btnSave_Click" runat="server" Width="62px" Font-Size="8pt" Text="Save" ValidationGroup="vg" CssClass="Button" /> </TD><TD rowSpan=1></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE>
</contenttemplate>
                    </asp:UpdatePanel>
                </td>                
            </tr>
            <tr>
                <td style="width: 100px">
   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
<asp:Panel id="Panel1" runat="server" Width="600px" Height="200px" ScrollBars="Vertical" BackColor="#E0E0E0"><asp:GridView id="SKUGroup_Grid" runat="server" Width="578px" Height="1px" ForeColor="SteelBlue" CssClass="gridRow2" BackColor="White" OnPageIndexChanging="SKUGroup_Grid_PageIndexChanging" AutoGenerateColumns="False" BorderColor="White" HorizontalAlign="Center" OnRowEditing="SKUGroup_Grid_RowEditing">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>
<Columns>
<asp:BoundField DataField="SKU_GROUP_ID" HeaderText="Group Id">
    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
</asp:BoundField>
<asp:BoundField DataField="GROUP_NAME" HeaderText="Group Name">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="SKU Name"><ItemTemplate>
<asp:ListBox id="listbox1" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True"></asp:ListBox> 
</ItemTemplate>
    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
</asp:TemplateField>
<asp:CommandField ShowEditButton="True" HeaderText="Edit">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:CommandField>
</Columns>
<HeaderStyle CssClass="tblhead"></HeaderStyle>
</asp:GridView> </asp:Panel> 
</ContentTemplate>
                    </asp:UpdatePanel></td>                
            </tr>
        </table>       
        </div>
</asp:Content>