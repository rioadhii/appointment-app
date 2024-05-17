## Prequisite 
- Install .net core SDK 6.0
- Install ms sql Server
- Build Package in root folder `dotnet build`

## Migrations Command
- cmd dir inside Appointment.Data
- dotnet ef migrations add InitialCreate --startup-project ..\Appointment.Api\ --context AppDbContext
- dotnet ef database update --startup-project ..\Appointment.Api\ --context AppDbContext

## Run
- cmd dir inside Appointment.API `dotnet run`

## Project sln
- Appointment.Api -> consume services from Appointment.Core (REST Server)
- Appointment.Core -> all business logic / services and call Repository
- Appointment.Data -> connection Database and Repository (LINQ or ORM logic goes here)
- Appointment.Utils -> all utilities/libs
