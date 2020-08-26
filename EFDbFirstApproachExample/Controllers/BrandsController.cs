using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproachExample.Models;

namespace EFDbFirstApproachExample.Controllers
{
    public class BrandsController : Controller
    {

        EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
        public ActionResult Index(string search = "", string SortColumn = "BrandName", string IconClass = "fa-sort-asc", int PageNo = 1)
        {
            ViewBag.search = search;
            List<Brand> brands = db.Brands.Where(x => x.BrandName.Contains(search)).ToList();
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (ViewBag.SortColumn == "BrandID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    brands = brands.OrderBy(x => x.BrandID).ToList();
                else
                    brands = brands.OrderByDescending(x => x.BrandID).ToList();
            }
            else if (ViewBag.Sortcolumn == "BrandName")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    brands = brands.OrderBy(x => x.BrandName).ToList();
                else
                    brands = brands.OrderByDescending(x => x.BrandName).ToList();
            }

            int NoOfRecordsPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(brands.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            brands = brands.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();
            return View(brands);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Brand brand = db.Brands.Where(x => x.BrandID == id).FirstOrDefault();

            if (brand == null)
            {
                return HttpNotFound();
            }

            return View(brand);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            if (brand == null)
            {
                return Content("You can not save a empty brand in database");
            }

            db.Brands.Add(brand);
            db.SaveChanges();
            return RedirectToAction("Index", "Brands");
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Brand brand = db.Brands.Where(x => x.BrandID == id).FirstOrDefault();

            if (brand == null)
            {
                return HttpNotFound();
            }

            return View(brand);
        }

        [HttpPost]
        public ActionResult Edit(Brand brand)
        {
            if (brand == null)
            {
                return Content("Can't edit to null values and than save those nulls to db");
            }

            Brand existingBrand = db.Brands.Where(x => x.BrandID == brand.BrandID).FirstOrDefault();
            existingBrand.BrandName = brand.BrandName;
            db.SaveChanges();
            return RedirectToAction("Index", "Brands");
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Brand brand = db.Brands.Where(x => x.BrandID == id).FirstOrDefault();

            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        [HttpPost]
        public ActionResult Delete(long? id, Brand brand)
        {
            Brand deleteBrand = db.Brands.Where(x => x.BrandID == id).FirstOrDefault();
            db.Brands.Remove(deleteBrand);
            db.SaveChanges();
            return RedirectToAction("Index", "Brands");
        }
    }
}


