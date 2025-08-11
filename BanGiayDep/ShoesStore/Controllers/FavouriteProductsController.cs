using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.Models.Session;
using ShoesStore.ViewModels;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShoesStore.Controllers
{
	public class FavouriteProductsController : Controller
	{
		ISanpham sanphamrepo;
		IDongSanpham _product;
		ISize _size;
		ISanphamSize _tkho;
		IMau _mau; IKhuyenMai kmRepo;
		public FavouriteProductsController(ISanpham productDetail, IDongSanpham product, ISize sz, ISanphamSize tkho, IMau mau, IKhuyenMai kmRepo)
		{
			sanphamrepo = productDetail;
			_product = product;
			_size = sz;
			_tkho = tkho;
			_mau = mau;
			this.kmRepo = kmRepo;
		}

		public IActionResult ViewFavouriteProducts()
		{
			var favouriteItems = HttpContext.Session.Get<List<FavouriteProductsItem>>("Favourite") ?? new List<FavouriteProductsItem>();

			return View(favouriteItems);
		}

		public IActionResult AddFavouriteProducts(int id)
		{
			var favouriteItems = HttpContext.Session.Get<List<FavouriteProductsItem>>("Favourite") ?? new List<FavouriteProductsItem>();
			var existingFavouriteItem = favouriteItems.FirstOrDefault(item => item.Id == id);

			if (existingFavouriteItem == null)
			{
				favouriteItems.Add(sanphamrepo.GetFavProById(id));
			}

			HttpContext.Session.Set("Favourite", favouriteItems);

			return RedirectToAction(nameof(ViewFavouriteProducts));
		}
        public IActionResult RemoveFavouriteProduct(int id)
        {
            var favouriteItems = HttpContext.Session.Get<List<FavouriteProductsItem>>("Favourite") ?? new List<FavouriteProductsItem>();
            var itemToRemove = favouriteItems.FirstOrDefault(item => item.Id == id);

            if (itemToRemove != null)
            {
                favouriteItems.Remove(itemToRemove);
                HttpContext.Session.Set("Favourite", favouriteItems);
                TempData["Message"] = "Product was successfully removed from your favourites.";
            }
            else
            {
                TempData["Message"] = "Product not found in your favourites.";
            }

            return RedirectToAction(nameof(ViewFavouriteProducts));
        }

    }
}
