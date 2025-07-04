using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Catalog_Web_Application.Data;
using Product_Catalog_Web_Application.Models;
using Product_Catalog_Web_Application.Repository.Base;
using System.Security.Claims;

namespace Product_Catalog_Web_Application.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
       
        public ProductsController(IRepository<Products> Repository, AppDbContext db)
        {
            this.Repository = Repository;
            this.db = db;
        }
        private IRepository<Products> Repository;
       
        private readonly AppDbContext db;




        public IActionResult Index()
        {
            categorylist();
            
            return View(Repository.GetAll("category","user").Where(x=>x.CreationDate>=x.StartDate && DateTime.Now<= x.StartDate.AddDays(x.Duration)));
        }

        public IActionResult New()
        {
            categorylist();
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Products products)
        {
            if (ModelState.IsValid)
            {
               
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
          

           
                products.CreatedByUserId = userId;

       
                Repository.AddOne(products);

      
                return RedirectToAction("Index");
            }
            else
            {
    
                return View(products);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var products = Repository.GetById(id.Value);
            categorylist();
            return View(products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Products products)
        {
            //if (!ModelState.IsValid)
            //{
            //    foreach (var state in ModelState)
            //    {
            //        var key = state.Key; // The name of the input field
            //        var errors = state.Value.Errors; // Validation errors for this field

            //        foreach (var error in errors)
            //        {
            //            Console.WriteLine($"Field: {key}, Error: {error.ErrorMessage}");
            //        }
            //    }
            //}
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                products.CreatedByUserId = userId;
                Repository.UpdateOne(products);
                return RedirectToAction("Index");

            }
            else { return View(products); }
        }

        [HttpPost]

        public IActionResult Delete(Products products)
        {
            var Entity = Repository.GetById(products.Id);
            if (Entity != null)
            {
                Repository.DeleteOne(Entity);




                TempData["Success"] = "Item Deleted Successfully";
                return RedirectToAction("Index");
            }
            else
                return View();


        }
        public IActionResult Search(int id)
        {
            categorylist(); 

            var products = Repository.GetAll();
            if (id > 0) 
            {
                products = products.Where(x => x.CategoryId == id).ToList();
            }

            return View(products.Where(x => x.CreationDate >= x.StartDate && DateTime.Now <= x.StartDate.AddDays(x.Duration)));
        }
        public void categorylist(int selectid = 1)
        {
            List<Category> categories = db.categories.ToList();
            

            SelectList selectListItems = new SelectList(categories, "Id", "Name", selectid);
            ViewBag.CategoryList = selectListItems;
        }


    }
}
