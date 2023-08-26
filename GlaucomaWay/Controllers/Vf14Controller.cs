using System.Collections.Generic;
using System.Linq;
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

namespace GlaucomaWay.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class Vf14Controller : ApiController
{
    private readonly IVf14Repository _vf14Repository;
    private readonly IPatientRepository _patientRepository;

    private readonly ILogger<Vf14Controller> _logger;

    public Vf14Controller(IVf14Repository vf14Repository, IPatientRepository patientRepository, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, ILogger<Vf14Controller> logger)
        : base(userManager, httpContextAccessor)
    {
        _logger = logger;
        _vf14Repository = vf14Repository;
        _patientRepository = patientRepository;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Vf14ResultModel>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _vf14Repository.GetByIdWithPatientAsync(id, cancellationToken);

        return result switch
        {
            null => NotFound(),
            _ => HasPermission(result.Patient.User) ? Ok(result) : Forbid()
        };
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<List<Vf14ResultModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _vf14Repository.GetAllWithPatientsAsync(cancellationToken);

        return IsAdmin() ? Ok(result) : Ok(result.Where(r => r.Patient.UserId == AuthenticatedUser.Id).ToList());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<int>> CreateAsync([FromBody] Vf14CreateOrUpdateModel resultModel, CancellationToken cancellationToken)
    {
        try
        {
            var patient = await _patientRepository.GetByIdAsync(int.Parse(AuthenticatedUser.PatientId), cancellationToken);
            if (patient == null)
            {
                return NotFound(); // TODO: add more detail on what exactly is not found.
            }

            if (!HasPermission(patient.User))
            {
                return Forbid();
            }

            var result = await _vf14Repository.CreateAsync(resultModel.ToVf14ResultModel(patient), cancellationToken);

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
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var existing = await _vf14Repository.GetByIdAsync(id, cancellationToken);

        if (existing == null)
        {
            return NotFound();
        }

        var patient = await _patientRepository.GetByIdAsync(int.Parse(AuthenticatedUser.PatientId), cancellationToken);
        if (!HasPermission(patient.User))
        {
            return Forbid();
        }

        try
        {
            await _vf14Repository.DeleteAsync(id, cancellationToken);
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
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] Vf14CreateOrUpdateModel resultModel, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var existing = await _vf14Repository.GetByIdAsync(id, cancellationToken);

        if (existing == null)
        {
            return NotFound();
        }

        var patient = await _patientRepository.GetByIdAsync(int.Parse(AuthenticatedUser.PatientId), cancellationToken);
        if (patient == null)
        {
            return NotFound(); // TODO: add more detail on what exactly is not found.
        }

        if (!HasPermission(patient.User))
        {
            return Forbid();
        }

        UpdateExistingValues(resultModel.ToVf14ResultModel(patient), existing);

        try
        {
            await _vf14Repository.UpdateAsync(existing, cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }

        return NoContent();
    }

    private static void UpdateExistingValues(Vf14ResultModel newModel, Vf14ResultModel existing)
    {
        existing.Q1Score = newModel.Q1Score;
        existing.Q2Score = newModel.Q2Score;
        existing.Q3Score = newModel.Q3Score;
        existing.Q4Score = newModel.Q4Score;
        existing.Q5Score = newModel.Q5Score;
        existing.Q6Score = newModel.Q6Score;
        existing.Q7Score = newModel.Q7Score;
        existing.Q8Score = newModel.Q8Score;
        existing.Q9Score = newModel.Q9Score;
        existing.Q10Score = newModel.Q10Score;
        existing.Q11Score = newModel.Q11Score;
        existing.Q12Score = newModel.Q12Score;
        existing.Q13Score = newModel.Q13Score;
        existing.Q14Score = newModel.Q14Score;
        existing.Total = newModel.Total;
    }
}