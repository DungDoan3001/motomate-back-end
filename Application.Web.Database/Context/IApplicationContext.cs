using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Context
{
    public interface IApplicationContext
    {
        DbSet<ResetPassword> ResetPassword { get; set; }
        //DbSet<BillingInfo> BillingInfo { get; set; }
        //DbSet<CompletedTrip> CompletedTrip { get; set; }
        //DbSet<DriverLicense> DriverLicense { get; set; }
        //DbSet<IncompleteTrip> IncompleteTrip { get; set; }
        //DbSet<PaymentMethod> PaymentMethod { get; set; }
        //DbSet<TripRequest> TripRequest { get; set; }
        //DbSet<User> Users { get; set; }
        //DbSet<UserType> UserType { get; set; }
        //DbSet<UserTypeType> UserTypeType { get; set; }
        //DbSet<Vehicle> Vehicle { get; set; }

        DbSet<T> GetSet<T>() where T : class;
    }
}