using System.Text.Json;
using Dsw2026Ej15.Data.Interfaces;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    public List<Doctor> Doctors { get; private set; }

    public List<Specialty> Specialties { get; private set; }

    public PersistenceInMemory()
    {
        Doctors = new List<Doctor>();
        Specialties = LoadSpecialties();
    }

    public Doctor AddDoctor(Doctor doctor)
    {
        Doctors.Add(doctor);
        return doctor;
    }

    public Doctor? GetDoctor(Guid id)
    {
        return Doctors.FirstOrDefault(d => d.Id == id);
    }

    public List<Doctor> GetActiveDoctors()
    {
        return Doctors
            .Where(d => d.IsActive)
            .ToList();
    }

    public Specialty? GetSpecialty(Guid id)
    {
        return Specialties
            .FirstOrDefault(s => s.Id == id);
    }

    private List<Specialty> LoadSpecialties()
    {
        var filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "specialities.json");

        if (!File.Exists(filePath))
        {
            return new List<Specialty>();
        }

        var json = File.ReadAllText(filePath);

        var specialties = JsonSerializer.Deserialize<List<Specialty>>(json);

        return specialties ?? new List<Specialty>();
    }
}