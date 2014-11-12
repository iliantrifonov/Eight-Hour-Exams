<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LikeControl.ascx.cs" Inherits="Web.Controls.LikeControl.LikeControl" %>
<div runat="server" id="likeWrapper" class="like-panel">

    <asp:UpdatePanel runat="server" ID="UpdatePanelLike" UpdateMode="Conditional">

        <ContentTemplate>

            <%--  <asp:Button ID="ButtonLike" runat="server" Text="+" ToolTip="Like" CssClass="btn like" OnClick="ButtonLike_Click" />

            <asp:Label ID="LabelLikes" runat="server" Width="25"></asp:Label>

            <asp:Button ID="ButtonDislike" runat="server" Text="-" ToolTip="Dislike" CssClass="btn dis-like" OnClick="ButtonDislike_Click" />--%>


            <div class="pull-left">
                <div class="like-control col-md-1">
                    <div>Likes</div>
                    <div>
                        <div>
                            <asp:LinkButton ID="ButtonLike" runat="server" Text="" ToolTip="Like" CssClass="btn btn-default glyphicon glyphicon-chevron-up" OnClick="ButtonLike_Click" />
                        </div>
                        <asp:Label ID="LabelLikes" CssClass="like-value" runat="server" Width="25"></asp:Label>
                        <div>
                            <asp:LinkButton ID="ButtonDislike" runat="server" Text="" ToolTip="Dislike" CssClass="btn btn-default glyphicon glyphicon-chevron-down" OnClick="ButtonDislike_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</div>
