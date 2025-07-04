
using GameZone.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string imagepath;
        public GamesController(AppDbContext db, IWebHostEnvironment webHostEnvironment )
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
            imagepath = $"{webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        public IActionResult Index()
        {
            var game = db.Games.Include(d => d.Device).ThenInclude(d => d.device).Include(g => g.category).ToList();
            return View(game);
        }
        public IActionResult Details(int id)
        {
            var game = db.Games.Include(d => d.Device).ThenInclude(d => d.device).Include(g => g.category).SingleOrDefault(g=>g.Id==id);
            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new CreateGameFormViewModel()
            {
                Categorise = db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).OrderBy(x=>x.Text).ToList(),
                Devices=db.Devices.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(c => c.Text).ToList()

            };


            return View(viewModel);
        }
        
        [HttpPost]
        // to do use repesotry pattern
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categorise = db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).OrderBy(x => x.Text).ToList();
                model.Devices = db.Devices.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(c => c.Text).ToList();
                return View(model);
            }
            string CoverName = "";
            
            if(model.Cover!=null)
            {
                CoverName = await SaveCover(model.Cover);
            }    
           

            if (ModelState.IsValid)
            {
                Game game = new()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Categoryid = model.Categoryid,
                    Cover = CoverName,
                    Device = model.SelectedDevices.Select(x => new GameDevice { DeviceId = x }).ToList()

                };
                db.Games.Add(game);
                db.SaveChanges();

            }


            return RedirectToAction("Index");
        }
      
        public IActionResult edit(int id)
        {
            CreateGameFormViewModel viewModel = new CreateGameFormViewModel()
            {
                Categorise = db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).OrderBy(x => x.Text).ToList(),
                Devices = db.Devices.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(c => c.Text).ToList()

            };


            var game1 =db.Games.Include(g=>g.Device).SingleOrDefault(x => x.Id == id);
            var game2=db.Games.Select(x=> new CreateGameFormViewModel { 
                Description=game1.Description ,
                Categoryid=game1.Categoryid,
                Categorise=viewModel.Categorise,
                Devices = viewModel.Devices,
                SelectedDevices = game1.Device.Select(x => x.DeviceId).ToList(),
                CurrentCover=game1.Cover,


                Name = game1.Name
            }
            ).FirstOrDefault();

            return View(game2);
        }
        [HttpPost]
        public async Task<IActionResult> edit(CreateGameFormViewModel model ,int id)
        {
            if (!ModelState.IsValid)
            {
                model.Categorise = db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).OrderBy(x => x.Text).ToList();
                model.Devices = db.Devices.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).OrderBy(c => c.Text).ToList();
                return View(model);
            }
            var hasNewCover = model.Cover is not null;
           
          

            if (ModelState.IsValid)
            {
                Game game = db.Games.Include(g => g.Device).SingleOrDefault(x => x.Id == id);

                game.Name = model.Name;
                game.Description = model.Description;
                game.Categoryid = model.Categoryid;
                string oldcover = "";
                if (hasNewCover)
                {
                     oldcover = game.Cover;
                    game.Cover = await SaveCover(model.Cover!);
                    
                }
                
                game.Device = model.SelectedDevices.Select(x => new GameDevice { DeviceId = x }).ToList();

              
                db.Update(game);
                var changes=db.SaveChanges();
                if (changes>0)
                {
                    if (hasNewCover)
                    {
                        var cover = Path.Combine(imagepath, oldcover);
                        System.IO.File.Delete(cover);
                    }
                }
               

            }


            return RedirectToAction("Index");
        }

        public  IActionResult Delete(int id)
        {
            var game =db.Games.Include(db => db.Device).SingleOrDefault(g => g.Id == id);
            db.Games.Remove(game);
            db.SaveChanges();
            if(game.Cover != "")
            {
                var cover = Path.Combine(imagepath, game.Cover);
                System.IO.File.Delete(cover);
            }
           
            return  RedirectToAction("Index");
   
        }
        public async Task<string> SaveCover(IFormFile Cover)
        {

            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(imagepath, CoverName);
            using var stream = System.IO.File.Create(path);
            await Cover.CopyToAsync(stream);
            return CoverName;
        }
    }
}
