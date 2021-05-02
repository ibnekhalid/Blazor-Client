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
			   .AddScoped<IAlertService, AlertService>()
			   .AddScoped<ILocalStorageService, LocalStorageService>()
			   .AddScoped<IAccountService, AccountService>();

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
