using System.Data.Entity;
using System.Linq;

namespace SpecialNeeds.Cloudata.Data
{
    public partial class NewAkivaModel : DbContext
    {
        public NewAkivaModel()
            : base("name=NewAkivaModel")
        {
        }
        public NewAkivaModel(string connectionString) : base(connectionString)
        {
        }

        public virtual DbSet<Avatar> Avatars { get; set; }
        public virtual DbSet<GazeDefocusingPeriod> GazeDefocusingPeriods { get; set; }
        public virtual DbSet<GazeFocusingSample> GazeFocusingSamples { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Localization> Localizations { get; set; }
        public virtual DbSet<Mechanic> Mechanics { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<ThresholdBasedTransformSample> ThresholdBasedTransformSamples { get; set; }
        public virtual DbSet<TransformSource> TransformSources { get; set; }
        public virtual DbSet<UserAnswersReport> UserAnswersReports { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VoiceSample> VoiceSamples { get; set; }
        public virtual DbSet<MechanicsBasedTransformSample> MechanicBasedTransformSamples { get; set; }
        public virtual DbSet<World> Worlds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Gender)
                .HasForeignKey(e => e.ChildGenderId);

            modelBuilder.Entity<Localization>()
                .HasMany(e => e.Modules)
                .WithRequired(e => e.Localization)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mechanic>()
                .HasMany(e => e.VoiceSamples)
                .WithRequired(e => e.Mechanic)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Module>()
                .HasMany(e => e.Mechanics)
                .WithRequired(e => e.Module)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Module>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Module)
                .WillCascadeOnDelete(false);

            // modelBuilder.Entity<Session>()
            //     .HasMany(e => e.Sessions1)
            //     .WithRequired(e => e.Session1)
            //     .HasForeignKey(e => e.WorldID);

            modelBuilder.Entity<TransformSource>()
                .HasMany(e => e.ThresholdBasedTransformSamples)
                .WithRequired(e => e.TransformSource)
                .HasForeignKey(e => e.SourceId);
        }
    }
}
