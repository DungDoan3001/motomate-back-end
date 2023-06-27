using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        // Place DbSet here:
        public DbSet<User> Users { get; set; }
        public DbSet<BillingInfo> BillingInfo { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<UserTypeType> UserTypeType { get; set; }
        public DbSet<DriverLicense> DriverLicense { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<TripRequest> TripRequest { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<IncompleteTrip> IncompleteTrip { get; set; }
        public DbSet<CompletedTrip> CompletedTrip { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTypeType>()
                        .HasKey(utt => new
                        {
                            utt.UserId,
                            utt.UserTypeId
                        });

            modelBuilder.Entity<UserType>(userType =>
            {
                userType.HasMany(ut => ut.UserTypeTypeList)
                    .WithOne(utt => utt.UserType)
                    .HasForeignKey(utt => utt.UserTypeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(user =>
            {
                user.HasMany(u => u.UserTypeTypeList)
                    .WithOne(utt => utt.User)
                    .HasForeignKey(utt => utt.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                user.HasMany(u => u.BillingInfoList)
                    .WithOne(bil => bil.User)
                    .HasForeignKey(bil => bil.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                user.HasMany(u => u.DriverLicenseList)
                    .WithOne(dll => dll.User)
                    .HasForeignKey(dll => dll.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TripRequest>(tripRequest =>
            {
                tripRequest.HasOne(tr => tr.Lessee)
                           .WithMany(lessee => lessee.LesseeTripRequestList)
                           .HasForeignKey(tr => tr.LesseeId)
                           .OnDelete(DeleteBehavior.Cascade);

                tripRequest.HasOne(tr => tr.Lessor)
                           .WithMany(lessor => lessor.LessorTripRequestList)
                           .HasForeignKey(tr => tr.LessorId)
                           .OnDelete(DeleteBehavior.Cascade);

                tripRequest.HasOne(tr => tr.Vehicle)
                           .WithMany(v => v.TripRequests)
                           .HasForeignKey(tr => tr.VehicleId)
                           .OnDelete(DeleteBehavior.Cascade);

                tripRequest.HasOne(tr => tr.PaymentMethod)
                           .WithOne(p => p.TripRequest)
                           .HasForeignKey<TripRequest>(tr => tr.PaymentId)
                           .OnDelete(DeleteBehavior.Cascade);

                tripRequest.HasOne(tr => tr.IncompleteTrip)
                           .WithOne(p => p.TripRequest)
                           .HasForeignKey<IncompleteTrip>(tr => tr.TripId)
                           .OnDelete(DeleteBehavior.Cascade);

                tripRequest.HasOne(tr => tr.CompletedTrip)
                           .WithOne(p => p.TripRequest)
                           .HasForeignKey<CompletedTrip>(tr => tr.TripId)
                           .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<T> GetSet<T>()
            where T : class
        {
            return this.Set<T>();
        }
    }
}
