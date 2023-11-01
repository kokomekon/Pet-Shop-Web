namespace PetShopWeb.Models
{
    public class Comments
    {
        [Key]
        public int CommentsId { get; set; }
        [MaxLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
        public string CommentTxt { get; set; }
        public int AnimalId { get; set; }
        public Animals Animal { get; set; }
    }
}
