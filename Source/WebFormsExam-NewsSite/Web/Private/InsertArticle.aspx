<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsertArticle.aspx.cs" Inherits="Web.Private.InsertArticle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:FormView ID="FvInsertArticle" ItemType="Web.Models.Article" InsertMethod="FvInsertArticle_InsertItem" DefaultMode="Insert" runat="server">
        <HeaderTemplate>
            <h2>Insert Article:</h2>
        </HeaderTemplate>
        <ItemTemplate>
        </ItemTemplate>
        <InsertItemTemplate>
            <div class="row">
                <h3>Title: 
                    <asp:TextBox runat="server" ID="TextBox1" Text="<%# BindItem.Title %>" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="TextBox1" ForeColor="Red" ErrorMessage="Article title is required!"
                        >Article title is required!</asp:RequiredFieldValidator>
                </h3>
                <p>
                    Category: 
					<asp:DropDownList runat="server" ID="DdlInsert" ItemType="Web.Models.Category" SelectedValue="<%# BindItem.CategoryId %>" SelectMethod="DdlInsert_GetData" DataTextField="Name" DataValueField="Id">
                    </asp:DropDownList>
                </p>
                <p>
                    Content: 
                    <asp:TextBox runat="server" ID="TextBox2" TextMode="MultiLine" Text="<%# BindItem.Content %>" Rows="10" Width="100%"></asp:TextBox>	
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="TextBox2" ForeColor="Red" ErrorMessage="Article content is required!"
                        >Article content is required!</asp:RequiredFieldValidator>			    
                </p>
                <div>
                    <asp:Button Text="Insert" runat="server" CommandName="Insert" CssClass="btn btn-success" />
                    <asp:Button Text="Cancel" runat="server" CausesValidation="false" CommandName="Cancel" CssClass="btn btn-danger" />
                </div>
            </div>
        </InsertItemTemplate>
    </asp:FormView>

</asp:Content>
