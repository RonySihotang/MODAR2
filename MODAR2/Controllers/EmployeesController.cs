using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using API.Base;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class EmployeesController : BaseController<int, EmployeeRepositories, Employee>
{
    private EmployeeRepositories _repo;

    public EmployeesController(EmployeeRepositories repo) : base(repo)
    {
        _repo = repo;
    }
}


