using Microsoft.AspNetCore.Mvc;
using Product_Catalog_Web_Application.Data;
using Product_Catalog_Web_Application.Models;
using Product_Catalog_Web_Application.Repository.Base;

namespace Product_Catalog_Web_Application.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryController(IRepository<Category> category)
        {
            this.Repository = Repository;
        }
        private IRepository<Category> Repository;
        public IActionResult Index()
        {
            return View();
        }
    }
}
