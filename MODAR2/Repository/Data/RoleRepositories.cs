using API.Contexts;
using API.Models;
namespace API.Repository.Data;

public class RoleRepositories : GeneralRepository<MyContext, Role, int>
{
    public RoleRepositories(MyContext context) : base(context)
    {
    }
}
