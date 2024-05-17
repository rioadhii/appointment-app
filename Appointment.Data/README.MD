## Migrations Command

- dotnet ef migrations add InitialCreate --startup-project ..\Appointment.Api\ --context AppDbContext
- dotnet ef database update --startup-project ..\Appointment.Api\ --context AppDbContext