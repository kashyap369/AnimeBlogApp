using Anime.web.Data;
using Anime.web.Models.Domain;
using Anime.web.Models.ViewModels;
using Anime.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anime.web.Controllers
{
    [Authorize(Roles =("SuperAdmin"))]
    public class AdminTagsController : Controller
    {
        private readonly ITagInterface tagInterface;
        public AdminTagsController(ITagInterface tagInterface )
        {
            this.tagInterface = tagInterface;   
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRq addtagrq)
        {
            var tag = new Tag
            {
                Name = addtagrq.Name,
                DisplayName = addtagrq.DisplayName
            };
            await   tagInterface.AddAsync(tag);
            
            return RedirectToAction("List");
        }

       
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await tagInterface.GetAllAsync();  
            return View(tags);
        }


        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            //var tag = await dbcontext.Tags.FirstOrDefaultAsync(t => t.GetAllAsync == id);
            var tag = await tagInterface.GetAsync(id); 

            if(tag!= null)
            {
                var etag = new EditTagRq
                {
                    Id = tag.Id,   
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(etag);
            }


            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRq edittagrq)
        {
            var tag = new Tag
            { 
                Id=edittagrq.Id,
                Name = edittagrq.Name,
                DisplayName=edittagrq.DisplayName  
            
            };
            await tagInterface.UpdateAsync(tag);
            return RedirectToAction("List");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var tag = await dbcontext.Tags.FirstOrDefaultAsync(t => t.GetAllAsync ==id);    

            //if(tag!= null)
            //{
            //    dbcontext.Tags.Remove(tag);
            //    await dbcontext.SaveChangesAsync();    
            //    return RedirectToAction("List");
            //}
            var deleted = await tagInterface.DeleteAsync(id);

            return RedirectToAction("List");

        }

    }
}
