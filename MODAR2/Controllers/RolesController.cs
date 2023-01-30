using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using API.Base;

namespace API.Controllers;

[Route("api/roles")]
[ApiController]

public class RolesController : BaseController<int, RoleRepositories, Role>
{
    public RolesController(RoleRepositories repo) : base(repo)
    {
    }
}


