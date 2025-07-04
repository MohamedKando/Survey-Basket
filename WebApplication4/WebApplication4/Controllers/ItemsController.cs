using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        public ItemsController(AppDbContext db2)

        {
            db = db2;    
        }
        private readonly AppDbContext db;
        public IActionResult Index()
        {
            var items = db.items.Include(i => i.category).ToList(); // Include related Category data
            return View(items);
        }

        public IActionResult New()
        {
            categorylist();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item items)
        {
           
          
            if (ModelState.IsValid) {
                db.items.Add(items);
                db.SaveChanges();
                TempData["Success"] = "Item Added Successfully";
                
                return RedirectToAction("Index");
            }
            else { return View(); }

        }
        [HttpPost]
        public void categorylist(int selectid=1)
        {
            List<Category> categories = db.categories.ToList();
           
            SelectList selectListItems =new SelectList(categories, "Id", "Name", selectid);
            ViewBag.CategoryList=selectListItems;
        }
        public IActionResult Delete(Item item)
        {
            var Entity =db.items.FirstOrDefault(x => x.Id == item.Id);
            if (Entity != null)
            {
                db.items.Remove(Entity);

                db.SaveChanges();
                
            }
            TempData["Success"] = "Item Deleted Successfully";
            
            
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }    
            var item=db.items.FirstOrDefault(x=>x.Id==id);
            categorylist();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item items)
        {
        
            if (ModelState.IsValid)
            {
                db.items.Update(items);
                db.SaveChanges();

                TempData["Success"] = "Item Edited Successfully";
                return RedirectToAction("Index");
            }
            else { return View(); }

        }
    }
}
