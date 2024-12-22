using Anime.web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Anime.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Anime.web.Models.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Anime.web.Controllers
{
    [Authorize(Roles = ("SuperAdmin"))]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagInterface tagInterface;
        private readonly IBlogPostRepository blogPostRepository;
        public AdminBlogPostsController(ITagInterface tagrepository,IBlogPostRepository blogPostRepository)
        {
            tagInterface = tagrepository;
            this.blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        [ActionName("Add")]
        public async Task<IActionResult> Add()
        {
            var tags = await tagInterface.GetAllAsync();

            var model = new AddBlogPostRq
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name,Value = x.Id.ToString() })
            };


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRq addBlogPost)
        {
            var blog = new BlogPost
            { 
                PageTitle = addBlogPost.PageTitle,
                Content = addBlogPost.Content,
                PublishedDate = addBlogPost.PublishedDate,
                UrlHandle = addBlogPost.UrlHandle,
                FeaturedImgUrl = addBlogPost.FeaturedImgUrl,
                Heading = addBlogPost.Heading,
                ShortDescription = addBlogPost.ShortDescription,
                Author = addBlogPost.Author,
                Visible = addBlogPost.Visible,

            };

            // map selected tag 
            var selectedtag = new List<Tag>();
            foreach(var SelectedTagId in addBlogPost.SelectedTag)
            {
                var SelectedTagIdAsGuid = Guid.Parse(SelectedTagId);
                var existingTag = await tagInterface.GetAsync(SelectedTagIdAsGuid);
                if (existingTag != null)
                {
                    selectedtag.Add(existingTag);
                }
            }
            blog.Tags = selectedtag;
            await blogPostRepository.AddAsync(blog);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var blogs = await blogPostRepository.GetAllAsync();
            return View(blogs);  
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            var blog = await blogPostRepository.GetAsync(Id);
            var tagmodel = await tagInterface.GetAllAsync();
            if(blog != null)
            {
                var model = new EditBlogPostRq
                {
                    Id = blog.Id,
                    Heading = blog.Heading,
                    PageTitle = blog.PageTitle,
                    Content = blog.Content,
                    UrlHandle = blog.UrlHandle, 
                    ShortDescription = blog.ShortDescription,
                    FeaturedImgUrl = blog.FeaturedImgUrl,
                    PublishedDate = blog.PublishedDate,
                    Author = blog.Author,
                    Visible = blog.Visible,
                    Tags = tagmodel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTag = blog.Tags.Select(x => x.Id.ToString()).ToArray()

                };
                return View(model);
            }


            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRq editBlogPostRq)
        {
            var post = new BlogPost
            {
                Id=editBlogPostRq.Id,
                Heading=editBlogPostRq.Heading,
                PageTitle=editBlogPostRq.PageTitle,
                Content = editBlogPostRq.Content,
                ShortDescription=editBlogPostRq.ShortDescription,
                FeaturedImgUrl=editBlogPostRq.FeaturedImgUrl,
                UrlHandle = editBlogPostRq.UrlHandle,
                PublishedDate=editBlogPostRq.PublishedDate,
                Author=editBlogPostRq.Author,
                Visible=editBlogPostRq.Visible
            };
            // map tags to domain
            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editBlogPostRq.SelectedTag)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundtag = await tagInterface.GetAsync(tag);
                    if (foundtag != null)
                    {
                        selectedTags.Add(foundtag);
                    }
                }
            }

            post.Tags = selectedTags;

            var updatedblog = await blogPostRepository.UpdateAsync(post);
            if(updatedblog != null)
            {
                return RedirectToAction("List");
            }
            return null;

        }

            

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(EditBlogPostRq edit)
        {   
            var deletetag = await blogPostRepository.DeleteAsync(edit.Id);
            if(deletetag != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new {id = edit.Id});
        }
            


    }
}
