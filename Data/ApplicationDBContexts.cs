namespace YourChickenGuide.Data
{
    public class ApplicationDBContexts : DbContext
    {
        public ApplicationDBContexts(DbContextOptions<ApplicationDBContexts> options) : base(options)
        {
        }

        public DbSet<Chicken> Chickens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Chicken>();

            // MotherId FK -> Chickens(Id)
            entity.HasOne<Chicken>()           // no navigation prop
                  .WithMany()
                  .HasForeignKey(c => c.mother_Id);

            // FatherId FK -> Chickens(Id)
            entity.HasOne<Chicken>()
                  .WithMany()
                  .HasForeignKey(c => c.father_Id);

            // Helpful indexes
            entity.HasIndex(c => c.mother_Id);
            entity.HasIndex(c => c.father_Id);

            // Optional: unique legband (nullable allowed)
            // entity.HasIndex(c => c.LegbandId).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}
