namespace PetShopWeb.ShopData
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option)
        {

        }

        public DbSet<Animals> Animals { get; set; }
        public DbSet<Category> Categorises { get; set; }
        public DbSet<Comments> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasData(
            new { CategoryId = 1, Name = "Carnivores" },
            new { CategoryId = 2, Name = "Reptiles" },
            new { CategoryId = 3, Name = "Mammals" },
            new { CategoryId = 4, Name = "Poultry" },
            new { CategoryId = 5, Name = "Fish" },
            new { CategoryId = 6, Name = "Insects" }
            );


            modelBuilder.Entity<Animals>().HasData(
            new { AnimalId = 1, Name = "Lion", Age = 50, PictureName = "/Images/lion.jpeg", Description = "Ferocious feline", CategoryId = 1 },
            new { AnimalId = 2, Name = "Leopard", Age = 30, PictureName = "/Images/leopard.jpeg", Description = "Spotted beauty", CategoryId = 1 },
            new { AnimalId = 3, Name = "Snake", Age = 20, PictureName = "/Images/snake.jpeg", Description = "Slithery reptile", CategoryId = 2 },
            new { AnimalId = 4, Name = "Snail", Age = 2, PictureName = "/Images/snail.jpg", Description = "Slow mollusk", CategoryId = 2 },
            new { AnimalId = 5, Name = "Dolphin", Age = 50, PictureName = "/Images/dolphin.jpeg", Description = "Graceful marine mammal", CategoryId = 3 },
            new { AnimalId = 6, Name = "Dog", Age = 50, PictureName = "/Images/dog.jpg", Description = "Man's best friend", CategoryId = 3 },
            new { AnimalId = 7, Name = "Wagtail", Age = 50, PictureName = "/Images/wagtail.jpg", Description = "Colorful bird", CategoryId = 4 },
            new { AnimalId = 8, Name = "Hoopoe", Age = 50, PictureName = "/Images/hoopoe.jpg", Description = "Distinctive bird", CategoryId = 4 },
            new { AnimalId = 9, Name = "Shark", Age = 50, PictureName = "/Images/shark.jpg", Description = "Apex predator of the sea", CategoryId = 5 },
            new { AnimalId = 10, Name = "Whale", Age = 50, PictureName = "/Images/whale.jpg", Description = "Gentle giant of the ocean", CategoryId = 5 },
            new { AnimalId = 11, Name = "Cockroach", Age = 50, PictureName = "/Images/cockroach.jpeg", Description = "Resilient insect", CategoryId = 6 },
            new { AnimalId = 12, Name = "Dung Beetle", Age = 50, PictureName = "/Images/dung_beetle.jpg", Description = "Nature's cleanup crew", CategoryId = 6 }
            );

            modelBuilder.Entity<Comments>().HasData(
            new { CommentsId = 1, AnimalId = 1, CommentTxt = "WOW, this lion is so cool" },
            new { CommentsId = 2, AnimalId = 10, CommentTxt = "They are so relaxing" }
            );

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Animals>().ToTable("Animals");
            modelBuilder.Entity<Comments>().ToTable("Comments");
        }
    }
}
