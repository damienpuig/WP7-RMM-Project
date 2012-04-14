using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using System.Data.Linq;
using RMM.Business.Helpers;
using RMM.Data.Model;
using System.Linq.Expressions;

namespace RMM.Business.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private RmmDataContext dataContext;

        public RmmDataContext DataContext
        {
            get { return dataContext; }
            set
            {
                dataContext = value;
                dataContext.ObjectTrackingEnabled = true;
                dataContext.Log = Console.Out;
            }
        }

        public Result<Category> DeleteCategorieById(int categoryId)
        {
            return Result<Category>.SafeExecute<CategoryService>(result =>
                {
                    using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var category = (from t in DataContext.Category
                                    where t.ID == categoryId
                                    select t).First();

                    if (category != null)
                    {
                        DataContext.Category.Log().DeleteOnSubmit(category);

                        DataContext.SubmitChanges();
                    }

                    result.Value = category;
                    }

                },() => "error");
        }

        public Result<Category> GetCategoryById(int categoryId, bool OnMinimal)
        {
            return Result<Category>.SafeExecute<CategoryService>(result =>
            {
                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    if(!OnMinimal)
                        DataContext.LoadOptions = DBHelpers.GetConfigurationLoader<Category>(c => c.TransactionList);

                    var category = DataContext.Category.Log().Where(a => a.ID == categoryId).First();




                    result.Value = category;
                }

            }, () => "error");
        }

        public Result<Category> CreateCategory(CreateCategoryCommand newCategoryCommand)
        {
            return Result<Category>.SafeExecute<CategoryService>(result =>
            {
                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var newCategoryEntity = new Category()
                    {
                        Balance = 0,
                        Color = newCategoryCommand.Color,
                        CreatedDate = DateTime.Now,
                        Name = newCategoryCommand.Name
                    };



                    DataContext.Category.Log().InsertOnSubmit(newCategoryEntity);

                    DataContext.SubmitChanges();

                    var AddedCategory = DataContext.Category.Log().Where(a => a.CreatedDate == newCategoryEntity.CreatedDate).First();

                    result.Value = AddedCategory;

                }

            }, () => "error");
        }

        public Result<Category> UpdateCategory(EditCategoryCommand editCategoryCommand)
        {
            return Result<Category>.SafeExecute<CategoryService>(result =>
            {

                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    var entityToUpdate = DataContext.Category.Log().Where(t => t.ID == editCategoryCommand.id).First();

                    entityToUpdate.ID = editCategoryCommand.id;
                    entityToUpdate.Name = editCategoryCommand.Name;
                    entityToUpdate.Color = editCategoryCommand.Color;
                    entityToUpdate.CreatedDate = DateTime.Now;


                    DataContext.SubmitChanges();

                    result.Value = entityToUpdate;
                }

            }, () => "error");
        }

        public Result<List<Category>> GetAllCategories(bool OnMinimal)
        {
            return Result<List<Category>>.SafeExecute<CategoryService>(result =>
            {
                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    if (!OnMinimal)
                    {
                        DataContext.LoadOptions = DBHelpers.GetConfigurationLoader<Category>(cat => cat.TransactionList);
                    }

                    var categories = DataContext.Category.Log().ToList();


                    result.Value = categories;
                }
            }, () => "error");
        }
    }
}
