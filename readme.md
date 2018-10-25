# Video Galore
This readme is for Video Galore, Project 2 in T-303-HONN at Reykjavik University
Project contributors are Unnsteinn Garðarsson, Ásdís Erna Guðmundsdóttir and Örn Friðriksson

## Environment and programming language
The API was written in C# using .net core and Entity Framework Core along with an SQL database.
We agreed on using camel casing for variables and pascal casing for methods. We begin all curly brackets after method declaration
on a new line.

## Design Principles
We are using the **Layered architecture** principle for our web api where we seperate each layer into an individual project.
We have a Galore.WebApi project as the presentation layer, Galore.Services as the domain layer and Galore.Repositories as the data layer
We also have individual projects for the Models and the tests. 

We are using the **Data Transfer Object(DTO)** principle for our models so that we pass input specific models to the controllers and to the services. The services then use both the input model and the DTO models to transform models between controller and data layer. To do this
we are using AutoMapper which is the **Mapping** principle. Each controller/service/repository are implement an interface so that we can use **Dependency Injection** within our project.

We wrote unit tests for all methods in controllers/services/repositories along with integration tests for all flows. User, Tape, Loan and Review. For the unit tests we are using EF core InMemory which mocks out our database based on our schema so that counts as the **Service Stub**

Our code is written with the **DRY** and **KISS** principles in mind and it is **Loosely Coupled**.

## Database
We chose a SQL database for this project, we wanted to grab the opportunity and get familiar with the azure portal. 
We used the Entity Framework Core Code First principle to genereate database tables based on our entity models.

## Build and run
Navigate to the Galore.WebApi folder and run `dotnet build` and then `dotnet run`

## Swagger
After executing `dotnet build` and `dotnet run` you can navigate to a generated [Swagger Documentation](https://localhost:5001/swagger/index.html)
