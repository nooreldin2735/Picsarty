using Data;
using Microsoft.AspNetCore.Mvc;
using Models.Vms;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Microsoft.AspNetCore.Identity;

namespace Picsarty.Areas.R_User.Controllers
{
    [Area("R_User")]
    public class CreateController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IWebHostEnvironment env;
        private readonly SignInManager<AppUser> _signinManger;

        public CreateController(DpcontextApp unitOfWork, IWebHostEnvironment env = null, SignInManager<AppUser>signInManager=null)
        {
            this.unitOfWork = new UnitOfWork(unitOfWork);
            this.env = env;
            this._signinManger = signInManager;
        }
        public IActionResult Index()
        {
            if (!_signinManger.IsSignedIn(User))
            {
                
                return RedirectToAction("Index", "Home");
            }
            UserOPModel userOPModel = new UserOPModel();
            
            Create_postVm create_PostVm = new Create_postVm();
            MultiSelectList selectLists=new MultiSelectList(unitOfWork.Catergory.GetAll("").ToList(), "Catergory_id", "Catergory_name");
            ViewBag.Categoriess = selectLists;
            return View(create_PostVm);
        }
        
        [HttpPost]
        public IActionResult Insert(Create_postVm prefix, IFormCollection form,IFormFile? file)
        {
            if (!_signinManger.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            prefix.post.Post_id=Guid.NewGuid().ToString ();
            if (ModelState.IsValid)
            {
                
                string wwwRoot = env.WebRootPath;
                if (file != null)
                {
                    string filename=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string PicPath=Path.Combine(wwwRoot, @"images/Pics_Database");

                    using (var filestream = new FileStream(Path.Combine(PicPath, filename),FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    prefix.post.picture = new Picture()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = filename,
                        Location = @"/images/Pics_Database/" + filename
                    };
                   
                }
                var Categories = form["Category_selectors"];
                prefix.post.catergories = new List<Catergory>();
                foreach (var category in Categories) 
                {
                    var Cat = unitOfWork.Catergory.Get(u => u.Catergory_id == category,"");
                    prefix.post.catergories.Add(Cat);
                }
                unitOfWork.Post.Add(prefix.post);
                unitOfWork.Save();
              
            }
            var cat = unitOfWork.Catergory.Get(u => u.Catergory_name.StartsWith("a"),"");

            return RedirectToAction("Index");
        }
       
    }
}
