using API.Contexts;
using API.Models;
using API.ViewModels;

namespace API.Repository.Data;

public class EmployeeProjectListRepositories : GeneralRepository<MyContext, EmployeeProjectList, int>
{
    private MyContext _context;
    public EmployeeProjectListRepositories(MyContext context) : base(context)
    {
        _context = context;
    }
}
