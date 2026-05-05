using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Models;

using DTO;
using IDAL;
public partial class Category : ICategoryREPO
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public void AddCategory(Class1.CategoryDTO category)
    {
        Category ctegory1 = new Category
        {
            CategoryId = category.CategoryID,
            CategoryName = category.CategoryName
        };
        using (BornDbContext db = new BornDbContext())
        {
            try
            {
                db.Categories.Add(ctegory1);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בהוספת קטגוריה למסד הנתונים", ex);
            }
        }

    }

    //TODO : implement delete category & all the rest of the methods in the interface
    public void DeleteCategory(int id)
    {
        using (BornDbContext db = new BornDbContext())
        {
            try
            {
                var category = db.Categories.Find(id);
                if (category == null)
                    throw new KeyNotFoundException($"Category with id {id} not found.");

                // If tasks reference this category, disassociate them (set CategoryId = null).
                // This avoids FK constraint errors if cascade delete is not configured.
                var relatedTasks = db.Tasks.Where(t => t.CategoryId == id).ToList();
                foreach (var t in relatedTasks)
                {
                    t.CategoryId = null;
                }

                db.Categories.Remove(category);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה במחיקת קטגוריה מהמסד", ex);
            }
        }
    }

    public List<Class1.CategoryDTO> GetAll()
    {
        using (BornDbContext db = new BornDbContext())
        {
            try
            {
                return db.Categories
                         .Select(c => new Class1.CategoryDTO
                         {
                             CategoryID = c.CategoryId,
                             CategoryName = c.CategoryName
                         })
                         .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת כל הקטגוריות מהמסד", ex);
            }
        }
    }

    public Class1.CategoryDTO GetCategoryByID(int id)
    {
        using (BornDbContext db = new BornDbContext())
        {
            try
            {
                var c = db.Categories.FirstOrDefault(x => x.CategoryId == id);
                if (c == null)
                    throw new KeyNotFoundException($"Category with id {id} not found.");

                return new Class1.CategoryDTO
                {
                    CategoryID = c.CategoryId,
                    CategoryName = c.CategoryName
                };
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת קטגוריה מהמסד", ex);
            }
        }
    }

    public void UpdateCategory(Class1.CategoryDTO category)
    {
        using (BornDbContext db = new BornDbContext())
        {
            try
            {
                var existing = db.Categories.Find(category.CategoryID);
                if (existing == null)
                    throw new KeyNotFoundException($"Category with id {category.CategoryID} not found.");

                existing.CategoryName = category.CategoryName;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בעדכון קטגוריה במסד הנתונים", ex);
            }
        }
    }
}