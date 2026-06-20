using Dsw2026Ej15.Api.DTOs;
using Dsw2026Ej15.Data.Interfaces;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost]
    public IActionResult Create(CreateDoctorRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Name es requerido");

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
            throw new ValidationException("LicenseNumber es requerido");

        var specialty = _persistence.GetSpecialty(request.SpecialtyId);

        if (specialty == null)
            throw new ValidationException("SpecialtyId no existe");

        var doctor = new Doctor
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            LicenseNumber = request.LicenseNumber,
            IsActive = true,
            Specialty = specialty
        };

        _persistence.AddDoctor(doctor);

        return StatusCode(201);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var doctors = _persistence
            .GetActiveDoctors()
            .Select(d => new DoctorResponse
            {
                Id = d.Id,
                Name = d.Name,
                LicenseNumber = d.LicenseNumber,
                SpecialtyName = d.Specialty.Name
            });

        return Ok(doctors);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var doctor = _persistence.GetDoctor(id);

        if (doctor == null || !doctor.IsActive)
            return NotFound();

        return Ok(new DoctorResponse
        {
            Id = doctor.Id,
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialtyName = doctor.Specialty.Name
        });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var doctor = _persistence.GetDoctor(id);

        if (doctor == null || !doctor.IsActive)
            return NotFound();

        doctor.IsActive = false;

        return NoContent();
    }
}