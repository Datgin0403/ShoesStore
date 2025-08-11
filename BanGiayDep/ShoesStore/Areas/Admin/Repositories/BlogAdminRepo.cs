using Microsoft.EntityFrameworkCore;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoesStore.Areas.Admin.Repositories
{
    public class BlogAdminRepo : IBlogAdmin
    {
        private readonly ShoesDbContext _context;

        public BlogAdminRepo(ShoesDbContext context)
        {
            _context = context;
        }

        public List<Blog> GetBlogs()
        {
            return _context.Blogs.OrderByDescending(x=>x.Mablog).ToList();
        }

        public int AddBlog(Blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return blog.Mablog; // Return the ID of the added blog
        }

        public bool UpdateBlog(Blog blog)
        {
            _context.Entry(blog).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(blog.Mablog))
                {
                    return false; // Blog not found
                }
                else
                {
                    throw;
                }
            }
            return true; // Updated successfully
        }

        public bool DeleteBlog(int blogId)
        {
            var blog = _context.Blogs.Find(blogId);
            if (blog == null)
            {
                return false; // Blog not found
            }

            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return true; // Deleted successfully
        }

        public Blog GetBlog(int blogId)
        {
            return _context.Blogs.Find(blogId);
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Mablog == id);
        }
    }
}
