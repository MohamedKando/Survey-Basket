using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;
using WebApplication4.Repository.Base;

namespace WebApplication4.Controllers { 

    [Authorize]
    public class CategoryController:Controller
    {

        public CategoryController(IRepository<Category> Repository)
        {
            this.Repository = Repository;
        }
        private IRepository<Category> Repository;

        //public IActionResult Index()
        
        //{
        //    return View(Repository.GetAll());
        //}
        public async Task<IActionResult> Index()
        
        {
            Category category = Repository.SelectOne(x => x.Name=="Computer");
            var allCat = await Repository.GetAllAsync("item");
            return View(allCat);
        }
        //GET
        public IActionResult New()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Category category)
        {

            if (ModelState.IsValid)
            {
                Repository.AddOne(category);
                return RedirectToAction("Index");

            }
            else { return View(category); }


        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = Repository.GetById(id.Value);
           
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
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
                Repository.UpdateOne(category);
                return RedirectToAction("Index");

            }
            else { return View(category); }
        }

       
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(Category category)
        {
            var Entity = Repository.GetById(category.Id);
            if (Entity != null)
            {
                Repository.DeleteOne(Entity);

                


                TempData["Success"] = "Item Deleted Successfully";
                return RedirectToAction("Index");
            }
            else
                return View();


           
        }


    }
}
