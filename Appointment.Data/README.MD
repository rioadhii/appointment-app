## Migrations Command

- Change directory to Appointment.Data
- dotnet ef migrations add InitialCreate --startup-project ..\Appointment.Api\ --context AppDbContext
- dotnet ef database update --startup-project ..\Appointment.Api\ --context AppDbContext