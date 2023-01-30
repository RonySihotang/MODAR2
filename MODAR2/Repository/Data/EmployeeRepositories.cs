using API.Contexts;
using API.Models;
using API.ViewModels;

namespace API.Repository.Data;

public class EmployeeRepositories : GeneralRepository<MyContext, Employee, int>
{
    private MyContext _context;
    public EmployeeRepositories(MyContext context) : base(context)
    {
        _context = context;
    }
}

