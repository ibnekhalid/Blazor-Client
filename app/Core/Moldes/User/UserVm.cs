using app.Core.Services;
using app.Graphql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Core.Moldes
{
    public class UserVm
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public Status Status { get; set; }
        public CompanyVm Company { get; set; }
    }
}
