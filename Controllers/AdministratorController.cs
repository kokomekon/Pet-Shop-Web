namespace PetShopWeb.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly ILogger<AdministratorController> _logger;
        private readonly DataContext _dataContext;
        public AdministratorController(DataContext _dataContext, ILogger<AdministratorController> logger)
        {
            this._dataContext = _dataContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var topAnimals = _dataContext.Animals
            .Include(a => a.Comment)
            .ToList();

            return View(topAnimals);
        }
        public async Task<IActionResult> DeleteAnimal(int animalId)
        {
            var animal = await _dataContext.Animals.FindAsync(animalId);
            _dataContext.Animals.Remove(animal);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int animalId)
        {
            var existingAnimal = await _dataContext.Animals
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.AnimalId == animalId);

            if (existingAnimal == null)
            {
                return NotFound();
            }

            var categories = _dataContext.Categorises.ToList();
            ViewBag.Categories = categories;

            await _dataContext.SaveChangesAsync();
            return View("Edit", existingAnimal);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBackToCatalog(Animals animalUpdate, IFormFile Photo)
        {
            if (!ModelState.IsValid)
            {
                var categories = _dataContext.Categorises.ToList();
                ViewBag.Categories = categories;
                return View("Edit", animalUpdate);
            }

            var existingAnimal = await _dataContext.Animals
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.AnimalId == animalUpdate.AnimalId);

            if (existingAnimal == null)
            {
                return NotFound();
            }

            existingAnimal.Name = animalUpdate.Name;
            existingAnimal.Age = animalUpdate.Age;
            existingAnimal.Description = animalUpdate.Description;

            if (Photo != null && Photo.Length > 0)
            {
                var uniqueFileName = Photo.FileName;
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");
                var filePath = Path.Combine(uploadDir, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(stream);
                }

                existingAnimal.PictureName = "/Images/" + uniqueFileName;
            }

            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddAnimal()
        {
            var categories = _dataContext.Categorises.ToList();
            ViewBag.Categories = categories;
            return View();
        }
        [HttpPost]
        public IActionResult AddAnimal(Animals newAnimal, IFormFile PictureName)
        {
            try
            {
                _logger.LogInformation("AddAnimal action started.");

                var categories = _dataContext.Categorises.ToList();
                ViewBag.Categories = categories;

                var categoryName = newAnimal.Category?.Name;
                if (!string.IsNullOrEmpty(categoryName))
                {
                    _logger.LogInformation($"Category Name: {categoryName}");

                    var existingCategory = _dataContext.Categorises.FirstOrDefault(c => c.Name == categoryName);

                    if (existingCategory != null)
                    {
                        newAnimal.Category = existingCategory;
                    }
                    else
                    {
                        var newCategory = new Category { Name = categoryName };

                        _logger.LogInformation($"Creating a new category: {newCategory.Name}");

                        _dataContext.Categorises.Add(newCategory);

                        newAnimal.Category = newCategory;
                    }
                }

                if (PictureName != null && PictureName.Length > 0)
                {
                    var uniqueFileName = PictureName.FileName;
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");
                    var filePath = Path.Combine(uploadDir, uniqueFileName);

                    _logger.LogInformation($"Uploading file: {uniqueFileName}");

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        PictureName.CopyTo(stream);
                    }

                    newAnimal.PictureName = "/Images/" + uniqueFileName;
                }

                _dataContext.Animals.Add(newAnimal);
                _dataContext.SaveChanges();

                _logger.LogInformation("Animal added successfully.");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding an animal.");
                return View("Error");
            }
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
