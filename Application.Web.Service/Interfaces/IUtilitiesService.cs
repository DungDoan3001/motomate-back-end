namespace Application.Web.Service.Interfaces
{
    public interface IUtilitiesService
    {
        Task<bool> CronJobActivator();
    }
}
