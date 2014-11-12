<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Articles.aspx.cs" Inherits="Web.Private.Articles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ListView runat="server" ID="LvArticles" ItemType="Web.Models.Article"
        SelectMethod="LvArticles_GetData" DataKeyNames="Id"
        OnSorting="LvArticles_Sorting"
        UpdateMethod="LvArticles_UpdateItem" DeleteMethod="LvArticles_DeleteItem">
        <LayoutTemplate>
            <div class="row">
                <asp:Button Text="Sort by Title" runat="server" CommandName="Sort" CommandArgument="Title" CssClass="col-md-2 btn btn-default" />
                <asp:Button Text="Sort by Date" runat="server" CommandName="Sort" CommandArgument="DateCreated" CssClass="col-md-2 btn btn-default" />
                <asp:Button Text="Sort by Category" runat="server" CommandName="Sort" CommandArgument="Category.Name" CssClass="col-md-2 btn btn-default" />
                <asp:Button Text="Sort by Likes" runat="server" CommandName="Sort" CommandArgument="Likes.Count" CssClass="col-md-2 btn btn-default" />
            </div>

            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />

            <div>
                <asp:DataPager ID="DataPagerArticles" runat="server"
                    PagedControlID="LvArticles" PageSize="5"
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
            <div class="row">
                <a href="~/Private/InsertArticle.aspx" class="btn btn-info pull-right" runat="server" id="insertID">Insert new Article</a>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="row">
                <h3><%#: Item.Title %>
                    <asp:Button Text="Edit" CssClass="btn btn-info" CommandName="Edit" runat="server" />
                    <asp:Button Text="Delete" CssClass="btn btn-danger" CommandName="Delete" runat="server" />
                </h3>
                <p>Category: <%#: Item.Category.Name %></p>
                <p>
                    <%#: Item.Content.Length > 300 ? Item.Content.Substring(0, 300) + "..." : Item.Content %>
                </p>
                <p>Likes count: <%#: Item.Likes.Count %></p>
                <div>
                    <i>by <%#: Item.Author.UserName %> </i>
                    <i>created on: <%# Item.DateCreated %></i>
                </div>
            </div>
        </ItemTemplate>
        <EditItemTemplate>

            <div class="row">
                <h3>
                    <asp:TextBox runat="server" ID="TextBox1" Text="<%# BindItem.Title %>"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName" runat="server"
                        ControlToValidate="TextBox1" ForeColor="Red" ErrorMessage="Title is required!"
                        >Title is required!</asp:RequiredFieldValidator>
                    <asp:Button Text="Save" CssClass="btn btn-success" CommandName="Update" runat="server" />
                    <asp:Button Text="Cancel" CssClass="btn btn-danger" CommandName="Cancel" CausesValidation="false" runat="server" />
                </h3>
                <p>
                    <asp:DropDownList runat="server" ID="DdlInsert" ItemType="Web.Models.Category" SelectedValue="<%# BindItem.CategoryId %>" SelectMethod="DdlInsert_GetData" DataTextField="Name" DataValueField="Id">
                    </asp:DropDownList>
                </p>
                <p>
                    <asp:TextBox runat="server" ID="TextBox2" TextMode="MultiLine" Text="<%# BindItem.Content %>" Rows="10" Width="100%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="TextBox2" ForeColor="Red" ErrorMessage="Content is required!"
                        >Content is required!</asp:RequiredFieldValidator>

                </p>
                <div>
                    <i>by <%#: Item.Author.UserName %> </i>
                    <i>created on: <%# Item.DateCreated %></i>
                </div>
            </div>

        </EditItemTemplate>
    </asp:ListView>
</asp:Content>
