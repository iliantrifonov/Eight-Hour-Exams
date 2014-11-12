using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.AspNet.Identity;

using Web.Models;

namespace Web.Private
{
    public partial class InsertArticle : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void FvInsertArticle_InsertItem()
        {
            var item = new Article();

            var authorId = User.Identity.GetUserId();
            var author = this.DbContext.Users.Find(authorId);
            item.Author = author;
            item.DateCreated = DateTime.Now;

            TryUpdateModel(item);

            if (ModelState.IsValid)
            {
                // Save changes here
                this.DbContext.Articles.Add(item);
                this.DbContext.SaveChanges();
                ErrorSuccessNotifier.AddSuccessMessage("Category added");
            }
            else
            {
                ErrorSuccessNotifier.AddErrorMessage(string.Join(", ", (ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage))));
            }
        }

        public IQueryable<Category> DdlInsert_GetData()
        {
            return this.DbContext.Categories.OrderBy(c => c.Name);
        }
    }
}