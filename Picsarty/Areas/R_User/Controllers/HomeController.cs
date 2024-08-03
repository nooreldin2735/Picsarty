using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;
using Picsarty.Models;
using Models.Vms;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Authentication;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity;

namespace Picsarty.Areas.R_User.Controllers
{
    [Area("R_User")]


    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<AppUser> _signinManger;
        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager;
        private readonly UnitOfWork _unitOfWork;
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public string ReturnUrl { get; set; }
        public HomeController(Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager, SignInManager<AppUser> manger, ILogger<HomeController> logger,DpcontextApp dbContext)
        {
            _logger = logger;
            _signinManger = manger;
            this._userManager = userManager;
            this._unitOfWork = new UnitOfWork(dbContext);
        }

        public IActionResult Index()
        {

            UserOPModel userOPModel = new UserOPModel();
            userOPModel.catergories=_unitOfWork.Catergory.GetAll("").ToList(); ;
            List<RecomendCategories> recomendCategories= _unitOfWork.RecomendeCategories.GetSome(u=>u.UserId==User.Identity.GetUserId()).ToList();
            recomendCategories.Sort(new CompareTwoCats());
            userOPModel.posts = _unitOfWork.Post.GetAll("picture;catergories").ToList();
            return View(userOPModel);
        }
        public async Task<IActionResult> Category_add(List<string> Categories_select)
            
        {
            List<Catergory> catergories = new List<Catergory>();
            foreach (var cat in Categories_select) 
            {
                var category=_unitOfWork.Catergory.Get(u => u.Catergory_id == cat, "");
                catergories.Add(category);
            }
            var user = await _userManager.GetUserAsync(User);
            user.Catergories = catergories;
            _unitOfWork.User.Update(user);
            _unitOfWork.Save();
            TempData["FirstTime"] = null ;
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public async Task<IActionResult> Login()
        {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(UserOPModel userOPModel)
        {


            var result = await _signinManger.PasswordSignInAsync(userOPModel.LoginVm.UserName, userOPModel.LoginVm.Password, true, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            throw new ArgumentException("Login failed!");
        }
        [HttpPost]
        public async Task<IActionResult> Signup(UserOPModel userOPModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = userOPModel.registerVm.UserName,
                    BDate = userOPModel.registerVm.BDate,
                    Email = userOPModel.registerVm.Email

                };
                user.Age = user.CalcAge(user.BDate);
                var result = await _userManager.CreateAsync(user, userOPModel.registerVm.Password);
                // To ask the User for his sex and Categories
                TempData["FirstTime"] = "yes";
                if (result.Succeeded)
                {
                    await _signinManger.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var tmp="";
                    foreach (var error in result.Errors)
                    {
                        tmp+=error.Description+";";
                    }
                    TempData["Errors"] =tmp;
                }



                

            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult post(string id)
        {
            // To Check The User signed or not
            if (_signinManger.IsSignedIn(User))
            {
                
                if (_unitOfWork.WatchHistory.Get(u => u.PostId == id&&u.Userid == User.Identity.GetUserId(), "")==null)
                {
                    WatchHistory watchHistory = new WatchHistory();
                    watchHistory.PostId = id;
                    watchHistory.Userid = User.Identity.GetUserId(); 
                    watchHistory.WData = DateTime.Now.ToString();
                    _unitOfWork.WatchHistory.Add(watchHistory);

                }
                else
                {
                    var watchHistory = _unitOfWork.WatchHistory.Get(u => u.PostId == id && u.Userid == User.Identity.GetUserId(), "");

                    watchHistory.WData = DateTime.Now.ToString();
                    _unitOfWork.WatchHistory.Update(watchHistory);
                }
                var post = _unitOfWork.Post.Get(u => u.Post_id == id, "catergories");
                
                foreach(var item in post.catergories)
                {
                    var ReCat = _unitOfWork.
                        RecomendeCategories
                        .Get(u => u.Category_name == item.Catergory_name &&
                        u.UserId == User.Identity.GetUserId(), "");
                    if (ReCat != null)
                    {
                        ReCat.clicks = ReCat.clicks + 1;
                        _unitOfWork.RecomendeCategories.Update(ReCat);
                    }
                    else
                    {
                        ReCat = new RecomendCategories()
                        {
                            UserId = User.Identity.GetUserId(),
                            Category_name = item.Catergory_name,
                            //Id = Guid.NewGuid().ToString(),
                            clicks = 1
                        };
                        _unitOfWork.RecomendeCategories.Add(ReCat);
                        
                    }
                }
                
                _unitOfWork.Save();
            }
            
            
            var Post=_unitOfWork.Post.Get(u=>u.Post_id== id, "picture");

            return View(Post);
        }

    }
}
public class CompareTwoCats : Comparer<RecomendCategories>
{
    public override int Compare(RecomendCategories? x, RecomendCategories? y)
    {
        if (x.clicks > y.clicks) return -1;
        else if (x.clicks < y.clicks) return 1;
        else return -1;
        
    }
}
