using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using API.Base;


[Route("api/employeeprojectlists")]
[ApiController]

public class EmployeeProjectListsController : BaseController<int,EmployeeProjectListRepositories, EmployeeProjectList>
{
    public EmployeeProjectListsController(EmployeeProjectListRepositories repo) : base(repo)
    {

    }
  
}




