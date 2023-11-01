namespace PetShopWeb.Controllers
{
    public class AnimalController : Controller
    {
        private readonly DataContext _dataContext;
        public AnimalController(DataContext _dataContext)
        {
            this._dataContext = _dataContext;
        }
        public IActionResult Index(int animalId)
        {
            var comment = _dataContext.Comments.ToList();
            var animals = _dataContext.Animals.Include(a => a.Comment).ToList();
            var animal = _dataContext.Animals.FirstOrDefault(a => a.AnimalId == animalId);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }


        [HttpGet]
        public IActionResult GetComments(int animalId)
        {
            var comments = _dataContext.Comments
                .Where(c => c.AnimalId == animalId)
                .ToList();

            return PartialView("_CommentsPartial", comments);
        }

        [HttpPost]
        public IActionResult AddComment(int animalId, string commentText)
        {
            var newComment = new Comments
            {
                AnimalId = animalId,
                CommentTxt = commentText
            };

            _dataContext.Comments.Add(newComment);
            _dataContext.SaveChanges();

            return RedirectToAction("Index", new { animalId = animalId });
        }

    }
}
