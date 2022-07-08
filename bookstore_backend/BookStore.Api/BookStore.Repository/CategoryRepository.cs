
using BookStoreM.models.Models;
using BookStoreM.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class CategoryRepository : BaseRepository
    {
        public ListResponse<Category> GetCategories()
        {
            
            var query = context.Categories.AsQueryable();
            int totalRecords = query.Count();
            IEnumerable<Category> categories = query;

            return new ListResponse<Category>()
            {
                Results = categories,
                TotalRecords = totalRecords,
            };

        }


        public ListResponse<Category> GetCategorie(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = context.Categories.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalRecords = query.Count();
            List<Category> categories = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Category>()
            {
                Results = categories,
                TotalRecords = totalRecords,
            };

        }

        public Category GetCategory(int id)
        {
           return context.Categories.FirstOrDefault(c=> c.Id == id);
        }

        public Category AddCategory(Category category)
        {
            var entry = context.Categories.Add(category);
            context.SaveChanges();
            return entry.Entity;
        }

        public Category UpdateCategory(Category category)
        {
            var entry = context.Categories.Update(category);
            context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCategory(int id)
        {
            var category = context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return false;

            context.Categories.Remove(category);
            context.SaveChanges();
            return true;
        }
    }  
}
