namespace PetShopWeb.Models
{
    public class Animals
    {
        [Key]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Age must be a positive number.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Picture URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string PictureName { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Comments> Comment { get; set; }
    }
}

