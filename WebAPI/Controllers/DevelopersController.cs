using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/projects/{id}/Controller")]
    public class DevelopersController : ControllerBase
    {
        private readonly IDevelopersService developersService;
        private readonly ILogger logger;
        public DevelopersController(IDevelopersService developersService)
        {
            this.developersService = developersService;
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //dbContext.Add(model);
                //await developersService();
                //return CreatedAtAction("Get", new { id = model.Id }, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(new { ex }); 
            }

        }


    }
}
