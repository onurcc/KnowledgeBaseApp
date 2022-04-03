using KnowledgeBaseApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBaseApp.Services
{
    public class ArticleService
    {
        private readonly ApplicationDbContext _db;
        public ArticleService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Article GetArticle(int articleId)
        {
            Article obj = new Article();
            return _db.Articles.Include(u => u.SubCategory).FirstOrDefault(u => u.Id == articleId);
        }

        public List<Article> GetArticles()
        {
            return _db.Articles.Include(u => u.SubCategory).ToList();
        }

        public List<Category> GetCategoryList()
        {
            return _db.Categories.ToList();
        }

        public List<SubCategory> GetSubCategoryList()
        {
            return _db.SubCategories.ToList();
        }

        public bool CreateArticle(Article objArticle)
        {
            _db.Articles.Add(objArticle);
            _db.SaveChanges();
            return true;
        }

        public bool UpdateArticle(Article objArticle)
        {
            var ExistingArticle = _db.Articles.FirstOrDefault(u => u.Id == objArticle.Id);
            if (ExistingArticle != null)
            {
                if (objArticle.ArticleImage == null)
                {
                    objArticle.ArticleImage = ExistingArticle.ArticleImage;
                }
                ExistingArticle.Title = objArticle.Title;
                _db.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool DeleteArticles(Article objArticle)
        {
            var ExistingArticle = _db.Articles.FirstOrDefault(u => u.Id == objArticle.Id);
            if (ExistingArticle != null)
            {
                _db.Articles.Remove(ExistingArticle);
                _db.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }

        public List<Article> SearchArticle(string searchText)
        {
            return _db.Articles.Where(p => p.Title.Contains(searchText) || p.Content.Contains(searchText)).ToList();
        }

        public List<Article> NewArticles()
        {
            return ((from a in _db.Articles orderby a.ArticleDate descending select a).Take(5)).ToList();
        }

        public List<Article> RelatedArticles(int ArticleId)
        {
            return ((from a in _db.Articles orderby a.ArticleDate descending select a).Take(3)).ToList();
        }
    }
}
