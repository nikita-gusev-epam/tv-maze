using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPAM.TvMaze.RestApi.Contracts;
using EPAM.TvMaze.RestApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EPAM.TvMaze.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowsService _showsService;

        public ShowsController(IShowsService showsService)
        {
            _showsService = showsService ?? throw new ArgumentNullException(nameof(showsService));
        }

        [HttpGet]
        public async Task<ActionResult<List<ShowViewModel>>> GetAsync([FromQuery]int pageNumber = 0, [FromQuery]int pageSize = 250)
        {
            if (pageNumber < 0)
            {
                return BadRequest("Page number should be greater or equal than zero");
            }

            if (pageSize < 1 || pageSize > 250)
            {
                return BadRequest("Page size should be between 1 and 250");
            }

            var shows = await _showsService.GetShowsAsync(pageNumber, pageSize);

            if (!shows.Any())
            {
                return NotFound();
            }

            return shows;
        }
    }
}
