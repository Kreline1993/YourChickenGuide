namespace YourChickenGuide.Data
{
    public class ApplicationDBContexts : DbContext
    {
        public ApplicationDBContexts(DbContextOptions<ApplicationDBContexts> options) : base(options)
        {
        }

        public DbSet<Chicken> Chickens { get; set; }
    }
}
