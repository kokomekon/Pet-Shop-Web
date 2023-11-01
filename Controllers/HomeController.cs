namespace PetShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;
        public HomeController(DataContext _dataContext)
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

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("api/animals/{animalId}")]
        public async Task GetAnimalById(int animalId)
        {
            await _dataContext.Animals.FindAsync(animalId);
        }


        public async Task UpdateAnimal(Animals animal)
        {
            _dataContext.Animals.Update(animal);
            await _dataContext.SaveChangesAsync();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}