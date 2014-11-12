<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1>News</h1>
    <h2>Most popular articles</h2>
    <asp:ListView runat="server" ID="LvTopArticles" ItemType="Web.Models.Article"
        SelectMethod="LvTopArticles_GetData" DataKeyNames="Id">

        <ItemTemplate>
            <div class="row">
                <h3><a runat="server" href='<%# "~/Public/ViewArticle.aspx?id=" + Item.Id %>'><%#: Item.Title %></a> <small><%#: Item.Category.Name %></small></h3>
                <p>
                    <%#: Item.Content.Length > 300 ? Item.Content.Substring(0, 300) + "..." : Item.Content %>
                </p>
                <p>Likes: <%#: Item.Likes.Sum(l => l.Value) %></p>
                <div>
                    <i>by <%#: Item.Author.UserName %></i>
                    <i>created on: <%# Item.DateCreated %></i>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>

    <h2>All categories</h2>
    <asp:ListView runat="server" ID="LvCategories" ItemType="Web.Models.Category"
        GroupItemCount="2" SelectMethod="LvCategories_GetData" DataKeyNames="Id">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="groupPlaceholder" />
        </LayoutTemplate>
        <GroupTemplate>
            <div class="row">
                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
            </div>
        </GroupTemplate>
        <ItemTemplate>

            <div class="col-md-6">
                <h3><%#: Item.Name %></h3>


                <asp:ListView runat="server" ID="LvCategories" ItemType="Web.Models.Article" DataSource="<%# Item.Articles.OrderByDescending(a => a.DateCreated).Take(3) %>"
                     DataKeyNames="Id">
                    <LayoutTemplate>
                        <ul>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li>
                            <a runat="server" href='<%#: "~/Public/ViewArticle.aspx?id=" + Item.Id %>'><strong><%#: Item.Title %></strong> <i>by <%#: Item.Author.UserName %></i></a>
                        </li>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        No articles
                    </EmptyDataTemplate>
                </asp:ListView>


            </div>

        </ItemTemplate>
    </asp:ListView>
</asp:Content>
