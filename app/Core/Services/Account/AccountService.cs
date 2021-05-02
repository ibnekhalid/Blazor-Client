using app.Core.Moldes.Account;
using app.Core.Moldes.User;
using app.Graphql;
using Microsoft.AspNetCore.Components;
using StrawberryShake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Core.Services
{
	public class AccountService : IAccountService
	{
		private static TmClient _client;
		private static UserClaims userClaims;
		private static bool IsAuthenticated;
		private static ILocalStorageService _storageService;
		private NavigationManager _navigationManager;
		public AccountService(TmClient tmClient, ILocalStorageService storageService, NavigationManager navigationManager)
		{
			_client = tmClient;
			_navigationManager = navigationManager;
			_storageService = storageService;
		}
		public async Task Initialize()
		{
			var token = await _storageService.GetItem<string>("token");
			if (!string.IsNullOrWhiteSpace(token))
			{
				userClaims = new UserClaims(token);
				IsAuthenticated = true;
			}
		}

		public async Task<(string result, bool isError)> LoginAsync(LoginModel model)
		{
			var result = await _client.Login.ExecuteAsync(new LoginModelInput { Username = model.Username, Password = model.Password });
			if (result.IsErrorResult())
			{

				return (string.Join('\n', result.Errors), true);
			}
			var token = result.Data.Account.Login.ToString();
			await _storageService.SetItem<string>("token", token);
			userClaims = new UserClaims(token);
			IsAuthenticated = true;
			return (token, false);
		}
		public async Task<(string result, bool isError)> RegisterAsync(RegisterModel model)
		{
			var result = await _client.Register.ExecuteAsync(new RegisterModelInput { CompanyName = model.CompanyName, Email = model.Email, Username = model.Username, Password = model.Password, PasswordConfirmation = model.ConfirmPassword });
			if (result.IsErrorResult())
			{

				return (string.Join('\n', result.Errors), true);
			}

			return ("", false);
		}
		public async Task Logout()
		{
			await _storageService.RemoveItem("token");
			userClaims = null;
			IsAuthenticated = false;
			_navigationManager.NavigateTo("account/login");
		}
		public bool IsLoggedIn()
		{
			return IsAuthenticated;
		}
		public UserClaims GetUserClaims()
		{
			return userClaims;
		}
	}
	public interface IAccountService
	{
		Task Initialize();
		bool IsLoggedIn();
		UserClaims GetUserClaims();
		Task<(string result, bool isError)> LoginAsync(LoginModel model);
		Task<(string result, bool isError)> RegisterAsync(RegisterModel model);
		Task Logout();
	}
}
