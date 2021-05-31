using app.Core.Moldes.Account;
using app.Core.Moldes.PageResultModel;
using app.Core.Moldes.User;
using app.Graphql;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using StrawberryShake;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app.Core.Services
{
	public class ProjectService : IProjectService
	{
		private static TmClient _client;
		private static ISnackbar _snackbar;
		public ProjectService(TmClient tmClient, ISnackbar snackbar)
		{
			_client = tmClient;
			_snackbar = snackbar;
		}
		public async Task<PageResult<ProjectVm>> GetProjectsAsync()
		{
			var result = await _client.GetProjects.ExecuteAsync(null);
			if (result.IsErrorResult())
				_snackbar.Add(string.Join('\n', result.Errors.Select(x => x.Message)),Severity.Error);
			return new PageResult<ProjectVm>(result.Data.Projects);
		}
		public async Task<(string result, bool isError)> CreateProjects(ProjectInput vm)
		{
			var result = await _client.CreateProject.ExecuteAsync(new CreateProjectVmInput { Title = vm.Title, Description = vm.Description });
			if (result.IsErrorResult())
			{

				return (string.Join('\n', result.Errors.Select(x => x.Message)), true);
			}

			return ("", false);
		}
		public async Task<(string result, bool isError)> AddUserInProject(string projectId,string email)
		{
			var result = await _client.AddUserInProject.ExecuteAsync(projectId,email);
			if (result.IsErrorResult())
			{

				return (string.Join('\n', result.Errors.Select(x => x.Message)), true);
			}

			return ("", false);
		}
	}
	public interface IProjectService
	{
		Task<PageResult<ProjectVm>> GetProjectsAsync();
		Task<(string result, bool isError)> AddUserInProject(string projectId, string email);
		Task<(string result, bool isError)> CreateProjects(ProjectInput vm);
	}
	public class ProjectVm
	{
		public string Id { get; set; }
		public string CompanyId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Status Status { get; set; }
		public virtual CompanyVm Company { get; protected set; }
	}
	public class ProjectInput
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
	}
	public class CompanyVm
	{
		public string Id { get; set; }
		public string Name { get; protected set; }
		public Status Status { get; protected set; }
	}

}
