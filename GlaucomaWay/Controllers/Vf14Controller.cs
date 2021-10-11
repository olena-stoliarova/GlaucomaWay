using GlaucomaWay.Models;
using GlaucomaWay.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GlaucomaWay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Vf14Controller : ControllerBase
    {

        private readonly IVf14Repository _vf14Repository;
        public Vf14Controller(IVf14Repository vf14Repository)
        {
            _vf14Repository = vf14Repository;
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

            var result = await _vf14Repository.GetByIdAsync(id, cancellationToken);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateAsync([FromBody] Vf14ResultModel resultModel, CancellationToken cancellationToken)
        {
            var result = await _vf14Repository.CreateAsync(resultModel, cancellationToken);

            try
            {
                await _vf14Repository.SaveAsync(cancellationToken);
            }
            catch (DbUpdateException)
            {
                //TODO: log exeption
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result.Id);
        }
    }
}
