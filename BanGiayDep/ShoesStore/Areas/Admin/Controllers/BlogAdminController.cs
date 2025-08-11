using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Areas.Admin.ViewModels;
using ShoesStore.Models;
using ShoesStore.Models.Authentication;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShoesStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthenticationM_S]
    public class BlogAdminController : Controller
    {
        private readonly IBlogAdmin _blogRepository; INhanvien nvRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BlogAdminController(IBlogAdmin blogRepository, IWebHostEnvironment hostingEnvironment, INhanvien nvRepo)
        {
            _blogRepository = blogRepository;
            _hostingEnvironment = hostingEnvironment;
            this.nvRepo = nvRepo;
        }

        [HttpGet]
        public IActionResult DetailBlog(int Mablog)
        {
            Blog blog = _blogRepository.GetBlog(Mablog);
            return View(blog);
        }

        [HttpGet]
        public IActionResult ListBlogs()
        {
            List<Blog> blogs = _blogRepository.GetBlogs();
            return View(blogs);
        }

        [HttpGet]
        public IActionResult AddBlog()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBlog(BlogViewModel blogViewModels)
        {

            try
            {

                int manv = nvRepo.getMaNVCurrent(HttpContext.Session.GetString("Email"));

                blogViewModels.Manv = manv;


                string fileName = "";

                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img", "blog");
                fileName = Guid.NewGuid().ToString() + "_" + blogViewModels.BlogImage.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    blogViewModels.BlogImage.CopyTo(fileStream);
                }


                Blog blog = new Blog
                {
                    Manv = blogViewModels.Manv,
                    Noidung = blogViewModels.Noidung,
                    Theloai = blogViewModels.Theloai,
                    Anhdaidien = fileName
                };

                int addedBlogId = _blogRepository.AddBlog(blog);
                if (addedBlogId > 0)
                {
                    return RedirectToAction(nameof(ListBlogs));
                }
                else
                {
                    ModelState.AddModelError("", "Error occurred while adding the blog.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
            }


            return View(blogViewModels);
        }

        [HttpGet]
        public IActionResult UpdateBlog(int Mablog)
        {
            Blog blog = _blogRepository.GetBlog(Mablog);
            if (blog == null)
            {
                return NotFound();
            }

            // Map the Blog model to BlogViewModel
            var blogViewModel = new BlogViewModel
            {
                Mablog = blog.Mablog,
                Manv = blog.Manv,
                Noidung = blog.Noidung,
                Theloai = blog.Theloai
                // You may need to map other properties if necessary
            };

            return View(blogViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateBlog(BlogViewModel blogViewModels)
        {
            if (ModelState.IsValid)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img", "blog");
                string fileName = Guid.NewGuid().ToString() + "_" + blogViewModels.BlogImage.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    blogViewModels.BlogImage.CopyTo(fileStream);
                }


                Blog blog = _blogRepository.GetBlog(blogViewModels.Mablog);
                blog.Anhdaidien = fileName;
                blog.Theloai= blogViewModels.Theloai;
                blog.Noidung = blogViewModels.Noidung;

                bool isUpdated = _blogRepository.UpdateBlog(blog);

                if (isUpdated)
                {
                    return RedirectToAction(nameof(ListBlogs));
                }
                else
                {
                    return View("Error");
                }
            }

            return View(blogViewModels);
        }


        [HttpPost]
        public IActionResult DeleteBlog(int Mablog)
        {
            bool isDeleted = _blogRepository.DeleteBlog(Mablog);
            if (isDeleted)
            {
                return RedirectToAction(nameof(ListBlogs));
            }
            else
            {
                return View("Error");
            }
        }


    }
}
