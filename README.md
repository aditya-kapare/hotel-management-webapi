# hotelmangementwebapi
Use it for Web API project
# Hotel Management System - Web API 
 
## Description 
ASP.NET Core Web API for managing hotel operations including stays, rooms, customers, and cab services. 
 
## Features 
- Stay Management (Check-in / Check-out) 
- Customer Management 
- Room Allocation with validation 
- Drop and Pick Requests 
- Cab Driver Assignment 
 
## Tech Stack 
- ASP.NET Core Web API 
- Entity Framework Core 
- SQL Server 
 
## Endpoints 
/api/stays 
/api/customers 
/api/rooms 
/api/drop-pick-requests 
/api/cab-drivers 
 
## Team 
- Aditi Mane 
- Aditya 
- Aboli 
 
## Project Structure 
 
HotelManagementWebApi 
| 
|-- Contracts 
| 
|-- DTOs 
|    |-- DataTransferObjects 
| 
|-- Entities 
|    |-- Enums 
|    |-- ErrorModels 
|    |-- Exceptions 
|    |-- Models 
| 
|-- HMSWebApiProject 
|    |-- ContextFactory 
|    |-- Extensions 
|    |-- internal_log 
|    |-- appsettings.json 
|    |-- MapperProfile.cs 
|    |-- Program.cs 
| 
|-- Controllers 
|-- Repositories 
|-- Services 
|-- Services.Contracts 
|-- LoggerService 
