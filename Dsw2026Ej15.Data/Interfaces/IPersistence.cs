using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Data.Interfaces;

public interface IPersistence
{
    List<Doctor> Doctors { get; }

    List<Specialty> Specialties { get; }

    Doctor AddDoctor(Doctor doctor);

    Doctor? GetDoctor(Guid id);

    List<Doctor> GetActiveDoctors();

    Specialty? GetSpecialty(Guid id);
}