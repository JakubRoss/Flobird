# Flobird API

## About the project

Flobird API is a **learning-focused backend project** created to practice building REST APIs in **ASP.NET Core**.

The main goal of this project was to:

- understand how backend systems are structured,
- learn how to design REST endpoints,
- work with authentication, authorization and databases,
- apply basic clean code and layering principles.

The project is **not a production system**, but a practical playground for backend development.

---

## What I focused on

During development I focused mainly on backend fundamentals:

- designing RESTful APIs
- JWT authentication and role-based authorization
- separation of responsibilities (controllers, services, repositories)
- working with relational databases using Entity Framework Core
- basic error handling using middleware
- writing readable and maintainable code

---

## Key features

- User authentication (JWT)
- Boards, lists and cards management (Kanban-style domain)
- Role-based access to resources
- CRUD operations with validation
- File uploads (Azure Blob Storage)
- API documentation using Swagger

---

## Tech stack

### Backend

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- FluentValidation
- AutoMapper

### Security

- JWT authentication
- Role-based authorization

### Other

- Swagger / OpenAPI
- Azure Blob Storage
- SQL Server

---

## Project structure

The application is divided into several logical layers:

- **API** – controllers, middleware, application configuration
- **Application** – business logic, services, DTOs, validation
- **Domain** – domain models and contracts
- **Infrastructure** – database access and external services

This structure was chosen to better understand how backend projects are commonly organized in real applications.

---

## API overview

The API exposes endpoints for:

- authentication (`/accounts`)
- managing boards, lists and cards
- managing users and permissions
- attachments and comments

Full endpoint documentation is available via **Swagger UI** after running the application.

---

## Development notes

This project was developed **individually** and served as a backend learning project.
I am aware that:

- real-world projects involve teamwork,
- code reviews,
- shared standards and communication.

That is why I am currently looking for a **junior backend position**, where I can gain team experience and grow further.

---

## Running the project

### Requirements

- .NET 8 SDK
- SQL Server
- Azure Storage account (optional – for file uploads)

### Configuration

Application settings are stored in `appsettings.json` and environment variables.

---

## Author

Jakub Ros  
GitHub: https://github.com/JakubRoss

## License

This repository is **source-available**.
The code is provided for **review and evaluation purposes only**.

© 2023–2025 Jakub Klonowski-Rosploch. All rights reserved.
