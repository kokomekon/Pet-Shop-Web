namespace PetShopWeb.Controllers
{
    public class CatalogController : Controller
    {
        private readonly DataContext _dataContext;
        public CatalogController(DataContext _dataContext)
        {
            this._dataContext = _dataContext;
        }
        public IActionResult Index()
        {
            var topAnimals = _dataContext.Animals
          .Include(a => a.Comment)
          .ToList();

            return View(topAnimals);
        }

        public IActionResult Filter(int categoryId)
        {
            if (categoryId == 0)
            {
                var allAnimals = _dataContext.Animals
                    .Include(a => a.Category)
                    .ToList();

                return View("Index", allAnimals);
            }
            else
            {
                var filteredAnimals = _dataContext.Animals
                    .Include(a => a.Category)
                    .Where(a => a.CategoryId == categoryId)
                    .ToList();

                return View("Index", filteredAnimals);
            }
        }
    }
}
