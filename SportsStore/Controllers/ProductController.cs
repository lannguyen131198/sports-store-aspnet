using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using SportsStore.Models.ViewModels;
namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult List(string Category, int productPage = 1)
            => View(new ProductsListViewModel
            {
                Products = repository.Products
                .Where(P => Category == null || P.Category == Category)
                 .OrderBy(p => p.ProductID)
                 .Skip((productPage - 1) * PageSize)//danh sách product
                 .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,//thông tin phân trang
                    TotalItems = Category == null ?
                     repository.Products.Count() :
                    repository.Products.Where(e =>
                     e.Category == Category).Count()
                },
                CurrentCategory = Category
            });
    }
}