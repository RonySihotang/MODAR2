using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using API.Base;


[Route("api/reports")]
[ApiController]

public class ReportsController : BaseController<int, ReportRepositories, Report>
{
    public ReportsController(ReportRepositories repo) : base(repo)
    {

    }
}



