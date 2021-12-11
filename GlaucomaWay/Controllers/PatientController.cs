using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GlaucomaWay.Models;
using GlaucomaWay.Repositories.Interfaces;
using GlaucomaWay.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GlaucomaWay.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PatientController : ApiController
    {
        private readonly IPatientRepository _patientRepository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PatientController> _logger;

        public PatientController(IPatientRepository patientRepository, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, ILogger<PatientController> logger)
            : base(userManager, httpContextAccessor)
        {
            _logger = logger;
            _patientRepository = patientRepository;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PatientModel>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var result = await _patientRepository.GetByIdAsync(id, cancellationToken);

            if (!IsAdmin() || !HasPermission(result.User))
            {
                return Forbid();
            }

            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<PatientModel>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _patientRepository.GetAllAsync(cancellationToken);

            if (!IsAdmin())
            {
                return Forbid();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> CreateAsync([FromBody] PatientCreateOrUpdateModel patient, CancellationToken cancellationToken)
        {
            try
            {
                var authenticatedUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var result = await _patientRepository.CreateAsync(patient.ToPatientModel(authenticatedUser.Id), cancellationToken);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result.Id);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var existing = await _patientRepository.GetByIdAsync(id, cancellationToken);

            if (existing == null)
            {
                return NotFound();
            }

            if (!IsAdmin() || !HasPermission(existing.User))
            {
                return Forbid();
            }

            try
            {
                await _patientRepository.DeleteAsync(id, cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] PatientCreateOrUpdateModel resultModel, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var existing = await _patientRepository.GetByIdAsync(id, cancellationToken);

            if (existing == null)
            {
                return NotFound();
            }

            if (IsAdmin() || !HasPermission(existing.User))
            {
                return Forbid();
            }

            UpdateExistingValues(resultModel, existing);

            try
            {
                await _patientRepository.UpdateAsync(existing, cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

            return NoContent();
        }

        private static void UpdateExistingValues(PatientCreateOrUpdateModel newModel, PatientModel existing)
        {
            existing.BithDate = newModel.BithDate;
        }
    }
}
