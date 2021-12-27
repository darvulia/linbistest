using AutoMapper;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class DevelopersController : ControllerBase
    {
        private readonly IDevelopersService developersService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public DevelopersController(IDevelopersService developersService, ILogger<ProjectsController> logger, IMapper mapper)
        {
            this.developersService = developersService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{projectId}/developers", Name = "GetProjectId")]
        public async Task<IActionResult> Get(int projectId)
        {
            try
            {               
                if (await developersService.projectExist(projectId) == null)
                    return NotFound();

                var result = _mapper.Map<ProjectViewModel>(await developersService.GetById(projectId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }


        [HttpPost("{projectId}/developers")]
        public async Task<ActionResult> Post([FromBody] DeveloperViewModel request)
        {
            _logger.LogInformation("begin create developer");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if(await developersService.projectExist(request.projectId)== null)
                return NotFound();

            try
            {
                var model = _mapper.Map<Developer>(request);
                await developersService.CreateDeveloper(model);
                return new CreatedAtRouteResult("GetProjectId", new { projectId = request.id }, model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR to create developer");
                return BadRequest();
            }

        }


    }
}
