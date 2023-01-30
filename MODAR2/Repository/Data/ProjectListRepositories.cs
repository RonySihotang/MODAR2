using API.Contexts;
using API.Models;
using Microsoft.EntityFrameworkCore;
using MODAR2.ViewModels;

namespace API.Repository.Data;

public class ProjectListRepositories : GeneralRepository<MyContext, ProjectList, string>
{
    private MyContext _context;
    private DbSet<ProjectList> _project;
    public ProjectListRepositories(MyContext context) : base(context)
    {
        _context = context;
        _project = context.Set<ProjectList>();
    }

    public IEnumerable<ProjectVM> MasterProject()
    {
        //var manager = _context.Employees.Select(
        //    x => new Employee
        //    {
        //        Id = x.Id,
        //        FirstName = x.FirstName,
        //    }
        //    ).ToList();

        //var nama = _context.Employees.Select(
        // x => new Employee
        // {
        //     Id = x.Id,
        //     FirstName = x.FirstName,
        // }
        // ).ToList();

        //       var employees = _context.ProjectLists.Select(
        //    x => new ProjectList
        //    {
        //    EmployeeId = x.EmployeeId,
        //   }

        //).ToArray();

        //       var eId = "";
        //       for (var i = 0; i < employees.Length; i++)
        //       {

        //           eId += eId[i].
        //       }

        //var employees = _context.ProjectLists.Select(a => new { 
        //    a.EmployeeId
        //    }).ToArray();

        var project = (from a in _project
                       join b in _context.Employees on a.ManagerId equals b.Id
                       join c in _context.Employees on a.EmployeeId equals c.Id
                       select new ProjectVM
                       {
                           Id = a.Id,
                           Name = a.Name,
                           Description = a.Description,
                           Status = a.Status,
                           StartDate = a.StartDate,
                           EndDate = a.EndDate,
                           Manager = b.FirstName + " " + b.LastName,
                           Employee = c.FirstName + " " + c.LastName,
                       }).ToList();

        return project;
    }

}

