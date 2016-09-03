using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewWeb.Models;
using NewWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb.Controllers.Api
{
    [Route("api/patients")]
    [Authorize]
    public class PatientsController : Controller
    {
        private ILogger<PatientsController> _logger;
        private IPatientRepository _patientRepository;

        public PatientsController(IPatientRepository patientRepository, ILogger<PatientsController> logger)
        {
            _patientRepository = patientRepository;
            _logger = logger;

        }

        [HttpGet("")]
        public IActionResult GetMyPatients()
        {
            try
            {
                var doctorsPatients = _patientRepository.GetDoctorsPatients(User.Identity.Name);

                return Ok(doctorsPatients);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get all your patients: {ex}");
                return BadRequest("Error occured");
            }
           
        }

        [HttpPost("")]
        public async Task<IActionResult> PostPatient([FromBody]PatientViewModel thePatient)
        {
            if (ModelState.IsValid)
            {
                var newPatient = Mapper.Map<Patient>(thePatient);
                _patientRepository.DoctorAddPatient(newPatient, User.Identity.Name);

                if (await _patientRepository.SaveChangesAsync())
                {
                    return Created($"api/patients/{thePatient.Surname}", Mapper.Map<PatientViewModel>(newPatient));
                }

            }

            return BadRequest("Failed to save changes to the database");
        }
    }
}
