using API.Contexts;
using API.Models;
using API.ViewModels;

namespace API.Repository.Data;

public class ReportRepositories : GeneralRepository<MyContext, Report, int>
{
    private MyContext _context;
    public ReportRepositories(MyContext context) : base(context)
    {
        _context = context;
    }
}
