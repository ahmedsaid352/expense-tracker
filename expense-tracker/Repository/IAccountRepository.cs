using expense_tracker.Data.DTOs;
using Microsoft.AspNetCore.Identity;

namespace expense_tracker.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO registerDto);
        Task<string> LoginAsync(LoginDTO loginDto);
    }
}
