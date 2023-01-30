using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using API.Base;
using MODAR2.ViewModels;

namespace API.Controllers;

[Route("api/projectlists")]
[ApiController]

public class ProjectListsController : BaseController<string, ProjectListRepositories, ProjectList>
{
    private ProjectListRepositories _repo;
    public ProjectListsController(ProjectListRepositories repo) : base(repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [Route("masterprojectlist")]
    public ActionResult MasterProjectList()
    {
        try
        {
            var result = _repo.MasterProject();
            return result.Count() == 0
                 ? Ok(new { statusCode = 200, message = "Data Not Found" })
                : Ok(new { statusCode = 200, message = "success", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong : {ex.Message}" });
        }
    }


}




