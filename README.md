# Timesheet


## This is a timesheet project sample, built with:

- ASP.NET Core and C# for cross-platform server-side code
- Angular and TypeScript for client-side code
- 3rd party libraries, such as Moment.js and PrimeNg
- Bootstrap for layout and styling
- SQL Server with EF Core for database (O/RM)
- XUnit/FluentAssertions/Moq/Fixture for unit tests


## This project was build to solves the following two user stories:

### Story 1
```
As a freelancer
I want to be able to register the time I spend on my projects
so that I can create correct invoices for my customers
```
So, at appointments page you can register the time spent on your projects

### Story 2
```
As a freelancer
I want to be able to get an overview of my time registrations
so that I can create correct invoices for my customers
```
At appointments page you can use the filters to get the list of registrations and get a full overview of your projects

## To get started with docker:

At `root` folder:
```
docker-compose build --no-cache
docker-compose up
```
You can access the app with the following url:
http://localhost:8001/

## To get started without docker:

### PRE REQ:
- Sql Server (Remember to adjust the connection string)
- NodeJS
- .Net Core 2.2

*You can use VS integration to get this app up, otherwise, use the angular-cli with net core cli commands.
 
