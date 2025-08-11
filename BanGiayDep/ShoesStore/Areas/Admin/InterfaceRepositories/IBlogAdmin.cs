
using global::ShoesStore.Models;

namespace ShoesStore.Areas.Admin.InterfaceRepositories
{
    public interface IBlogAdmin
    {
        List<Blog> GetBlogs();
        int AddBlog(Blog blog);
        bool UpdateBlog(Blog blog);
        bool DeleteBlog(int blogId);
        Blog GetBlog(int blogId);

    }
}
