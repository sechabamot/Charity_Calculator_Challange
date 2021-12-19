using Charity_Calculator_Challange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charity_Calculator_Challange.Interfaces
{
    public interface IUserService
    {
        Task<string> Create(string email, string password);
        Task<string> GetUserRole(string email);
        Task<string> Authenticate(string email, string password);
    }
}
