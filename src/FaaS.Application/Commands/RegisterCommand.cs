namespace FaaS.Application.Commands
{
	public class RegisterCommand : ICommand
	{
		public string Login { get; }

		public string Password { get; }

		public string Name { get; }

		public RegisterCommand(string login, string password, string name)
		{
			Login = login;
			Password = password;
			Name = name;
		}
	}
}
