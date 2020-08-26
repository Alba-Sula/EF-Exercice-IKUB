using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproachExample.Models;

namespace EFDbFirstApproachExample.Controllers
{
    public class CategoriesController : Controller
    {
        EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
        public ActionResult Index(string search = "", string SortColumn = "CategoryName", string IconClass = "fa-sort-asc", int PageNo = 1)
        {
            ViewBag.search = search;
            List<Category> categories = db.Categories.Where(x => x.CategoryName.Contains(search)).ToList();
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (ViewBag.SortColumn == "CategoryID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    categories = categories.OrderBy(x => x.CategoryID).ToList();
                else
                    categories = categories.OrderByDescending(x => x.CategoryID).ToList();
            }
            else if (ViewBag.Sortcolumn == "CategoryName")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    categories = categories.OrderBy(x => x.CategoryName).ToList();
                else
                    categories = categories.OrderByDescending(x => x.CategoryName).ToList();
            }

            int NoOfRecordsPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(categories.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            categories = categories.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();

            return View(categories);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.Where(x => x.CategoryID == id).FirstOrDefault();

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (category.CategoryName == null)
            {
                return Content("You can't add an empty element in database");
            }
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.Where(x => x.CategoryID == id).FirstOrDefault();

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            Category existingCategory = db.Categories.Where(x => x.CategoryID == category.CategoryID).FirstOrDefault();

            existingCategory.CategoryName = category.CategoryName;

            db.SaveChanges();
            return RedirectToAction("Index", "Categories");
        }


        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.Where(x => x.CategoryID == id).FirstOrDefault();

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(Category category, long id)
        {
            Category deleteCategory = db.Categories.Where(x => x.CategoryID == id).FirstOrDefault();
            db.Categories.Remove(deleteCategory);
            db.SaveChanges();
            return RedirectToAction("Index", "Categories");
        }


    }
}


