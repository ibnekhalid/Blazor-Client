using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using app.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using MudBlazor.Services;
using MudBlazor;
using app.Core.Helpers;

namespace app
{
	public class Program
	{
		public static string token = "";
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.Services
			   .AddScoped<IScrollInfoService, ScrollInfoService>()
			   .AddScoped<IAlertService, AlertService>()
			   .AddScoped<ILocalStorageService, LocalStorageService>()
			   .AddScoped<IAccountService, AccountService>()
			   .AddScoped<IUserService, UserService>()
			   .AddScoped<IProjectService, ProjectService>();
			builder.Services.AddMudServices(config =>
			{
				
				config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

				config.SnackbarConfiguration.PreventDuplicates = false;
				config.SnackbarConfiguration.NewestOnTop = false;
				config.SnackbarConfiguration.ShowCloseIcon = true;
				config.SnackbarConfiguration.VisibleStateDuration = 5000;
				config.SnackbarConfiguration.HideTransitionDuration = 500;
				config.SnackbarConfiguration.ShowTransitionDuration = 500;
				config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
			});
			builder.Services.AddTmClient();
			builder.Services.AddHttpClient(
				"TmClient",
				(sp, client) =>
				{
					client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
					client.BaseAddress = new Uri("https://localhost:5001/query/");
				});
			var host = builder.Build();

			var accountService = host.Services.GetRequiredService<IAccountService>();
			await accountService.Initialize();
			var localStorage = host.Services.GetRequiredService<ILocalStorageService>();
			token = await localStorage.GetItem<string>("token");
			await host.RunAsync();
		}
	}
}
