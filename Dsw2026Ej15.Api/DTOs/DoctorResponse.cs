namespace Dsw2026Ej15.Api.DTOs;

public class DoctorResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string LicenseNumber { get; set; } = string.Empty;

    public string SpecialtyName { get; set; } = string.Empty;
}