using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using API.Base;


[Route("api/accountroles")]
[ApiController]

public class AccountRolesController : BaseController<int, AccountRoleRepositories, AccountRole>
{
    //private AccountRoleRepositories _repo;

    public AccountRolesController(AccountRoleRepositories repo) : base(repo)
    {

    }
}


