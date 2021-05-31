using app.Core.Moldes;
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
    public class UserService : IUserService
    {
        private static TmClient _client;
        private static ISnackbar _snackbar;
        public UserService(TmClient tmClient, ISnackbar snackbar)
        {
            _client = tmClient;
            _snackbar = snackbar;
        }
        public async Task<PageResult<UserVm>> GetUsers()
        {
            var result = await _client.GetUsers.ExecuteAsync(null);
            if (result.IsErrorResult())
                _snackbar.Add(string.Join('\n', result.Errors.Select(x => x.Message)), Severity.Error);
            return new PageResult<UserVm>(result.Data.Users);
        }
        public async Task<(string result, bool isError)> CreateUser(UserInput vm)
        {
            var result = await _client.CreateUser.ExecuteAsync(new CreateUserVmInput { Email = vm.Email });
            if (result.IsErrorResult())
            {

                return (string.Join('\n', result.Errors.Select(x => x.Message)), true);
            }

            return ("", false);
        }
    }
    public interface IUserService
    {
        Task<PageResult<UserVm>> GetUsers();
        Task<(string result, bool isError)> CreateUser(UserInput vm);
    }
    public class UserInput
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }


}
