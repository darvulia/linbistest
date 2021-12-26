using AutoMapper;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService projectsService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectsService projectsService, IMapper mapper, ILogger<ProjectsController> logger)
        {
            this.projectsService = projectsService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await projectsService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectViewModel request)
        {
            _logger.LogInformation("begin create project");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var model = _mapper.Map<Project>(request);
                await projectsService.CreateProject(model);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR to create project");
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Begin delete for project id {id}", id);
                var project = await projectsService.GetById(id);
                if (project == null)            
                    return NotFound();
             
                await projectsService.DeleteProject(project);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR to eliminar project : {id}", id);
                return BadRequest();
            }
        }



    }
}
