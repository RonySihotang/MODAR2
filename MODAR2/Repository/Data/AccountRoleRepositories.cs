using API.Contexts;
using API.Models;

namespace API.Repository.Data;

public class AccountRoleRepositories : GeneralRepository<MyContext, AccountRole,int>
{
    private readonly MyContext _context;
    public AccountRoleRepositories(MyContext context) : base(context)
    {
        _context = context;
    }
}

