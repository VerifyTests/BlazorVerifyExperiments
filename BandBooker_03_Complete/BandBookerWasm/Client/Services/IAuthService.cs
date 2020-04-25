using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BandBookerWasm.Shared.Models;

namespace BandBookerWasm.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel registerModel);
    }
}