using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comp229_301052117_Assign3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Comp229_301052117_Assign3.Controllers
{
    public class CrudController : Controller
    {
        private IRecipeRepository repository;
        private IHostingEnvironment environment;
        public CrudController(IRecipeRepository repo, IHostingEnvironment env) { repository = repo;  environment = env; }
        //public ViewResult Index() => View(repository.Recipes);
        [Authorize]
        public ViewResult Edit(string dishName) => View(repository.Recipes.FirstOrDefault(p => p.DishName == dishName));
        [HttpPost]
        public IActionResult Edit(Recipes recipes)
        {
            if (ModelState.IsValid)
            {
                repository.EditRecipe(recipes);
                return RedirectToAction("DataPage", "Crud");
            }
            else
            {
                return View(recipes);
            }
        }
        [HttpGet]
        [Authorize]
        public ViewResult InsertPage()
        {
            return View("InsertPage");
        }

        [HttpPost]
        public async Task<ActionResult> InsertPage(Recipes recipes)
        {
            if (ModelState.IsValid)
            {
                var allFiles = HttpContext.Request.Form.Files;
                foreach (var pic in allFiles)
                {
                    if(pic != null && pic.Length > 0)
                    {
                        var file = pic;
                        var upload = Path.Combine(environment.WebRootPath, "photos");
                        if(file.Length > 0)
                        {
                            var picName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            using (var fileStream = new FileStream(Path.Combine(upload, picName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                recipes.pictureName = picName;
                            }
                        }
                    }
                }
                repository.SaveRecipe(recipes);
                return RedirectToAction("DataPage", "Crud");
            }
            else
            {
                return View("InsertPage");
            }
        }
        [HttpPost]
        [Authorize]
        public IActionResult Delete(string dishName)
        {
            Recipes deletedRecipe = repository.DeleteRecipe(dishName);
            
            return RedirectToAction("DataPage", "Home");

        }
        
        [Authorize]
        public ViewResult Review(string dishName) => View(repository.Recipes.FirstOrDefault(p => p.DishName == dishName));
        [HttpPost]
        public IActionResult Review(Recipes re)
        {
                repository.SaveReview(re);//Same as edit recipe
                return RedirectToAction("DataPage", "Crud");
            
        }
        public ViewResult DataPage()
        {
            return View("DataPage", repository.Recipes);
        }

        public ViewResult DisplayPage(Recipes recipe) // the value gotten from the url
        {
            if (recipe.DishName == null)
            {
                return View();
            }
            else
            {
                return View(repository.Recipes.FirstOrDefault(r => r.DishName == recipe.DishName));
            }
        }
    }
}