namespace FaaS.Application.Commands
{
	public sealed class LoginCommand : ICommand
	{
		public string Login { get; }

		public string Password { get; }

		public LoginCommand(string login, string password)
		{
			Login = login;
			Password = password;
		}
	}
}
