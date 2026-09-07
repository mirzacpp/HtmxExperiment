using HtmxProject.Domain;

namespace HtmxProject.Application
{
	public interface IUserManager
	{
		ValueTask<Account> GetCurrentAccountAsync();
	}

	public class SystemUserManager : IUserManager
	{
		private readonly Account _account = new()
		{
			Id = Guid.NewGuid(),
			PhotoName = "",
			UserId = Guid.NewGuid(),
		};

		public ValueTask<Account> GetCurrentAccountAsync() => ValueTask.FromResult<Account>(_account);
	}
}