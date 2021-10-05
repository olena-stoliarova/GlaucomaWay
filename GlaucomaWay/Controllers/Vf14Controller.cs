using GlaucomaWay.Models;
using GlaucomaWay.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GlaucomaWay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Vf14Controller : ControllerBase
    {

        private readonly IVf14Service _vf14Service;
        public Vf14Controller(IVf14Service vf14Service)
        {
            _vf14Service = vf14Service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Vf14ResultModel>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var result = await _vf14Service.GetByIdAsync(id, cancellationToken);

            return result != null ? Ok(result) : NotFound();
        }
    }
}
