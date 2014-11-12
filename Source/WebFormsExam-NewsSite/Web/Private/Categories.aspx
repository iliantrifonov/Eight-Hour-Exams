<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="Web.Private.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView runat="server" ID="LvCategories" ItemType="Web.Models.Category"
        SelectMethod="LvCategories_GetData" DataKeyNames="Id"
        UpdateMethod="LvCategories_UpdateItem" DeleteMethod="LvCategories_DeleteItem"
        InsertItemPosition="LastItem"
        InsertMethod="LvCategories_InsertItem">
        <EmptyDataTemplate>
            No data
        </EmptyDataTemplate>
        <LayoutTemplate>
            <div class="row">
                <asp:Button Text="Category Name" runat="server" CssClass="btn btn-default" CommandName="Sort" CommandArgument="Name" />
            </div>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
            <div class="row">
                <asp:DataPager ID="DataPagerCat" runat="server"
                    PagedControlID="LvCategories" PageSize="5"
                    QueryStringField="page">
                    <Fields>
                        <asp:NextPreviousPagerField ShowFirstPageButton="true"
                            ShowNextPageButton="false" ShowPreviousPageButton="false" />
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField ShowLastPageButton="true"
                            ShowNextPageButton="false" ShowPreviousPageButton="false" />
                    </Fields>
                </asp:DataPager>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="row">
                <div class="col-md-3">
                    <%#: Item.Name %>
                </div>
                <asp:Button runat="server" CssClass="btn btn-info" CausesValidation="false" Text="Edit" CommandName="Edit" />
                <asp:Button runat="server" CssClass="btn btn-danger" CausesValidation="false" Text="Delete" CommandName="Delete" />
            </div>
        </ItemTemplate>

        <EditItemTemplate>
            <div class="row">
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="EditCatName" Text=" <%# BindItem.Name %>" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="EditCatName" ForeColor="Red" ErrorMessage="Category name is required!">Category name is required!</asp:RequiredFieldValidator>
                </div>

                <asp:Button runat="server" CssClass="btn btn-success" Text="Save" CommandName="Update" />
                <asp:Button runat="server" CausesValidation="false" CssClass="btn btn-danger" Text="Cancel" CommandName="Cancel" />
            </div>
        </EditItemTemplate>
        <InsertItemTemplate>
            <div class="row">
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="InsertCatName" Text=" <%# BindItem.Name %>" />
                </div>
                <asp:Button runat="server" CssClass="btn btn-info" Text="Save" CommandName="Insert" />
                <asp:Button runat="server" CssClass="btn btn-danger" CausesValidation="false" Text="Cancel" CommandName="Cancel" />
            </div>
        </InsertItemTemplate>
    </asp:ListView>
</asp:Content>
