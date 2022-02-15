using CRUDASPNETCOREandSQLITE.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRUDASPNETCOREandSQLITE.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private DatabaseContext db;

        public ProductController(DatabaseContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("~/")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.products = db.Products.ToList();
            return View();
        }
    }
}
