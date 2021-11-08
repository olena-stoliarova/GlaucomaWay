﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GlaucomaWay.Models;
using GlaucomaWay.Repositories;
using GlaucomaWay.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GlaucomaWay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        private readonly ILogger<PatientController> _logger;

        public PatientController(IPatientRepository patientRepository, ILogger<PatientController> logger)
        {
            _logger = logger;
            _patientRepository = patientRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientModel>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var result = await _patientRepository.GetByIdAsync(id, cancellationToken);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PatientModel>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _patientRepository.GetAllAsync(cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateAsync([FromBody] PatientCreateOrUpdateModel patient, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _patientRepository.CreateAsync(patient.ToPatientModel(), cancellationToken);
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