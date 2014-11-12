using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;

namespace Web.Private
{
    public partial class Categories : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<Category> LvCategories_GetData()
        {
            return this.DbContext.Categories.OrderBy(c => c.Id);
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void LvCategories_UpdateItem(int id)
        {
            Category item = this.DbContext.Categories.Find(id);

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
                ErrorSuccessNotifier.AddSuccessMessage("Added updated successfully");
            }
            else
            {
                ErrorSuccessNotifier.AddErrorMessage(string.Join(", ", (ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage))));
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void LvCategories_DeleteItem(int id)
        {
            var item = this.DbContext.Categories.Find(id);
            foreach (var article in item.Articles.ToArray())
            {
                this.DbContext.Articles.Remove(article);
            }

            this.DbContext.Categories.Remove(item);
            try
            {
                this.DbContext.SaveChanges();
                ErrorSuccessNotifier.AddSuccessMessage("Category deleted");
            }
            catch (Exception)
            {
                ErrorSuccessNotifier.AddErrorMessage("Could not delete category.");
            }
        }

        public void LvCategories_InsertItem()
        {
            var item = new Category();
            TryUpdateModel(item);

            if (ModelState.IsValid)
            {
                this.DbContext.Categories.Add(item);
                this.DbContext.SaveChanges();
                ErrorSuccessNotifier.AddSuccessMessage("Category added");
            }
            else
            {
                ErrorSuccessNotifier.AddErrorMessage(string.Join(", ", (ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage))));
            }
        }
    }
}