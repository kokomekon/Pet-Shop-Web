namespace PetShopWeb.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name should only contain letters and spaces")]
        public string Name { get; set; }
        public List<Animals> Animal { get; set; }
    }
}
