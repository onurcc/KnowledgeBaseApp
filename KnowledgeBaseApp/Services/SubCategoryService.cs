using KnowledgeBaseApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBaseApp.Services
{
    public class SubCategoryService
    {
        private readonly ApplicationDbContext _db;
        public SubCategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public SubCategory GetSubCategory(int subcategoryId)
        {
            SubCategory obj = new SubCategory();            
            return _db.SubCategories.Include(u => u.Category).FirstOrDefault(u => u.Id == subcategoryId);
        }

        public List<SubCategory> GetSubCategories()
        {
            return _db.SubCategories.Include(u => u.Category).ToList();
        }

        public List<Category> GetCategoryList()
        {
            return _db.Categories.ToList();
        }

        public bool CreateSubCategory(SubCategory objSubCategory)
        {
            _db.SubCategories.Add(objSubCategory);
            _db.SaveChanges();
            return true;
        }

        public bool UpdateSubCategory(SubCategory objSubCategory)
        {
            var ExistingSubCategory = _db.SubCategories.FirstOrDefault(u => u.Id == objSubCategory.Id);
            if (ExistingSubCategory != null)
            {
                ExistingSubCategory.Name = objSubCategory.Name;
                _db.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool DeleteSubCategory(SubCategory objSubCategory)
        {
            var ExistingSubCategory = _db.SubCategories.FirstOrDefault(u => u.Id == objSubCategory.Id);
            if (ExistingSubCategory != null)
            {
                _db.SubCategories.Remove(ExistingSubCategory);
                _db.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
