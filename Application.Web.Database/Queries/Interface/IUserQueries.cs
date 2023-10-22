namespace Application.Web.Database.Queries.Interface
{
	public interface IUserQueries
	{
		Task<bool> CheckIfUserExisted(Guid userId);
	}
}
