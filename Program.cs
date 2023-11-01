namespace PetShopWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PetDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(ConnectionString));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scop = app.Services.CreateScope())
            {
                var ctx = scop.ServiceProvider.GetRequiredService<DataContext>();
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "Default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}