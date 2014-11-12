using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

using Web.Helpers;
using Web.Models;

namespace Web.Private
{
    public partial class Articles : BasePage
    {
        public bool IsSorting { get; set; }

        public SortDirection sortDirection
        {
            get
            {
                if (Session["sortdirection"] == null)
                {
                    Session["sortdirection"] = SortDirection.Ascending;
                    return SortDirection.Ascending;
                }
                else if(!IsSorting) 
                {
                    return (SortDirection)Session["sortdirection"];
                }
                else if ((SortDirection)Session["sortdirection"] == SortDirection.Ascending)
                {
                    Session["sortdirection"] = SortDirection.Descending;
                    return SortDirection.Descending;
                }
                else
                {
                    Session["sortdirection"] = SortDirection.Ascending;
                    return SortDirection.Ascending;
                }
            }
            set
            {
                Session["sortdirection"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<Article> LvArticles_GetData([Session("OrderBy")]String orderBy = null)
        {
            var list = this.DbContext.Articles.OrderBy(a => a.Id);
            
            if (orderBy == null)
            {
                return list;
            }

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    list = list.OrderByDescending(orderBy);
                    break;
                case SortDirection.Descending:
                    list = list.OrderBy(orderBy);
                    break;
                default:
                    list = list.OrderByDescending(orderBy);
                    break;
            }

            return list;
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void LvArticles_UpdateItem(int id)
        {
            Article item = this.DbContext.Articles.Find(id);

            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(item);

            if (ModelState.IsValid)
            {
                this.DbContext.SaveChanges();
                ErrorSuccessNotifier.AddSuccessMessage("Updated category successfully");
            }
            else
            {
                ErrorSuccessNotifier.AddErrorMessage(string.Join(", ", (ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage))));
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void LvArticles_DeleteItem(int id)
        {
            var item = this.DbContext.Articles.Find(id);
            this.DbContext.Articles.Remove(item);
            try
            {
                this.DbContext.SaveChanges();
                ErrorSuccessNotifier.AddSuccessMessage("Article deleted");
            }
            catch (Exception)
            {
                ErrorSuccessNotifier.AddErrorMessage("Could not delete article.");
            }
        }

        public IQueryable<Category> DdlInsert_GetData()
        {
            return this.DbContext.Categories.OrderBy(c => c.Name);
        }

        protected void LvArticles_Sorting(object sender, ListViewSortEventArgs e)
        {
            IsSorting = true;
            e.Cancel = true;
            Session["OrderBy"] = e.SortExpression;
            LvArticles.DataBind();
        }

        public void Page_PreRender()
        {
            var a = Session["OrderBy"];
            //this.IsSorting = false;
        }
    }
}