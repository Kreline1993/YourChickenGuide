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
            entity.HasOne(c => c.Mother)
                  .WithMany(m => m.ChildrenAsMother)
                  .HasForeignKey(c => c.mother_Id)
                  .OnDelete(DeleteBehavior.SetNull);

            // FatherId FK -> Chickens(Id)
            entity.HasOne(c => c.Father)
                  .WithMany(f => f.ChildrenAsFather)
                  .HasForeignKey(c => c.father_Id)
                  .OnDelete(DeleteBehavior.SetNull);

            // Helpful indexes
            entity.HasIndex(c => c.mother_Id);
            entity.HasIndex(c => c.father_Id);

            // Optional: unique legband (nullable allowed)
            // entity.HasIndex(c => c.LegbandId).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}
