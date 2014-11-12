<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewArticle.aspx.cs" Inherits="Web.Public.ViewArticle" %>

<%@ Register Src="~/Controls/LikeControl/LikeControl.ascx" TagPrefix="uc" TagName="LikeControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:FormView RenderOuterTable="false" ID="FvArticle" ItemType="Web.Models.Article" SelectMethod="FvArticle_GetItem" DataKeyNames="Id" runat="server">
        <ItemTemplate>
            <% if (User.Identity.IsAuthenticated)
            { %>
                <div>
                    <uc:LikeControl runat="server" ID="LikeControl" DataID="<%# Item.Id %>" UserVote="<%# CheckUserLike(Item.Id) %>" LikesCount="<%# Item.Likes.Sum(l => l.Value) %>" OnLike="LikeControl_Like" OnDisLike="LikeControl_DisLike" />
                </div>
            <% }%>

            <h2><%#: Item.Title %> <small>Category: <%#: Item.Category.Name %></small></h2>
            <p><%#: Item.Content %></p>
            <p>
                <span>Author: <%#: Item.Author.UserName %></span>
                <span class="pull-right"><%# Item.DateCreated %></span>
            </p>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
