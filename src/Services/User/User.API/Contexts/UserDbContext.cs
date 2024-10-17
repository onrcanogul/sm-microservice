using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.API.Models;

namespace User.API.Contexts;

public class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<Models.User, Role, string>
{
}