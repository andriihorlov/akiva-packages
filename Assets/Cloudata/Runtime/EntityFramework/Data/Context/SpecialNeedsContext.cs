using System.Data.Common;
using System.Data.Entity;

namespace SpecialNeeds.Cloudata.Data
{
    public class SpecialNeedsContext : DbContext
    {
        public SpecialNeedsContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new SpecialNeedsContextInitializer());
        }

        public SpecialNeedsContext(DbConnection existingConnection, bool contextOwnsConnection) : base(
            existingConnection, contextOwnsConnection) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<World> Environments { get; set; }
        public DbSet<Module> Cases { get; set; }
        public DbSet<Mechanic> Anchors { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<TransformSource> TransformSources { get; set; }
        public DbSet<MechanicsBasedTransformSample> AnchorBasedTransformSamples { get; set; }
        public DbSet<ThresholdBasedTransformSample> ThresholdBasedTransformSamples { get; set; }
        public DbSet<VoiceSample> VoiceSamples { get; set; }
        public DbSet<GazeFocusingSample> GazeFocusingSamples { get; set; }
        public DbSet<GazeDefocusingPeriod> GazeDefocusingPeriods { get; set; }
        public DbSet<UserAnswersReport> UserAnswersReports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>()
                .HasRequired(s => s.Module)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mechanic>()
                .HasRequired(a => a.Module)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}