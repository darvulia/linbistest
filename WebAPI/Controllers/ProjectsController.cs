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
        private readonly ILogger logger;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectsService projectsService, IMapper mapper)
        {
            this.projectsService = projectsService;
            _mapper = mapper;
        }

        //[HttpGet("{id}", Name = "GetEmployeeById")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    try
        //    {
        //        var result = await employeeService.GetById(id);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"{BadRequest().StatusCode} : {ex.Message}");
        //    }


        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var model = _mapper.Map<Project>(request);
                await projectsService.CreateProject(model);
                return Ok();
                //return new CreatedAtRouteResult("GetEmployeeById", new { id = employee.id }, employee);
            }
            catch
            {
                return BadRequest();
            }
        }


        //[HttpPut]
        //public async Task<IActionResult> Put([FromBody] Employee employee)
        //{

        //    if (!ModelState.IsValid)
        //    {

        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var result = await employeeService.UpdateEmployee(employee);

        //        return new CreatedAtRouteResult("GetEmployeeById", new { id = employee.id }, employee);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }


        //}



        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var project = await projectsService.GetById(id);
                if (project == null)            
                    return NotFound();
             
                await projectsService.DeleteProject(project);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }



    }
}
