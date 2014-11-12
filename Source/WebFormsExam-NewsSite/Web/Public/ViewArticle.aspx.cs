using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.AspNet.Identity;

using Web.Controls.LikeControl;
using Web.Models;

namespace Web.Public
{
    public partial class ViewArticle : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The id parameter should match the DataKeyNames value set on the control
        // or be decorated with a value provider attribute, e.g. [QueryString]int id
        public Article FvArticle_GetItem([QueryString]int id)
        {
            return this.DbContext.Articles.Find(id);
        }

        protected void LikeControl_Like(object sender, LikeEventArgs e)
        {
            int articleId = e.ArticleId;
            var articles = this.DbContext.Articles.Find(articleId);
            var existingLike = GetCurrentUsersLike(articles);

            existingLike.Value = 1;
            articles.Likes.Add(existingLike);

            this.DbContext.SaveChanges();

            var ctrl = sender as LikeControl;
            ctrl.LikesCount = articles.Likes.Sum(l => l.Value);
            ctrl.UserVote = 1;
        }

        protected void LikeControl_DisLike(object sender, LikeEventArgs e)
        {
            int articleId = e.ArticleId;
            var article = this.DbContext.Articles.Find(articleId);
            var existingLike = GetCurrentUsersLike(article);

            existingLike.Value = -1;
            article.Likes.Add(existingLike);

            this.DbContext.SaveChanges();

            var ctrl = sender as LikeControl;
            ctrl.LikesCount = article.Likes.Sum(l => l.Value);
            ctrl.UserVote = -1;
        }

        private Like GetCurrentUsersLike(Article article)
        {
            string userId = this.User.Identity.GetUserId();
            
            var existingLike = article.Likes.FirstOrDefault(l => l.AuthorId == userId);
            if (existingLike == null)
            {
                existingLike = new Like()
                {
                    Article = article,
                    AuthorId = userId,
                    Value = 0
                };
            }

            return existingLike;
        }

        protected int CheckUserLike(int articleId)
        {
            var userId = this.User.Identity.GetUserId();
            var userLike = this.DbContext.Likes.FirstOrDefault(l => l.AuthorId == userId && l.Article.Id == articleId);

            if (userLike != null)
            {
                return userLike.Value;
            }
            else
            {
                return 0;
            }
        }
    }
}