[![codecov](https://codecov.io/gh/Oranged9922/TaskManager/graph/badge.svg?token=R67FS4S56J)](https://codecov.io/gh/Oranged9922/TaskManager)
[![tests](https://github.com/Oranged9922/TaskManager/actions/workflows/test_api.yml/badge.svg)]()
[![build](https://github.com/Oranged9922/TaskManager/actions/workflows/build_api.yml/badge.svg)]()
# TaskManager

## Overview

TaskManager is a task management system designed to simplify project management and team collaboration. Built on a robust architecture, it offers features like task assignment, status updates, user roles, and more.

## Table of Contents

- [Architecture](#architecture)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
- [Features](#features)
- [Contributing](#contributing)
- [License](#license)

## Architecture

### Layers

1. **API Layer**
    - Exposes RESTful endpoints.
    - Handles request validation and authentication.

2. **Application Layer**
    - Contains business logic and application services.
    - Uses MediatR for CQRS implementation.

3. **Domain Layer**
    - Contains domain entities and business rules.

4. **Infrastructure Layer**
    - Manages database context and repositories.
    - Implements external services.

5. **Common Layer**
    - Manages cross-cutting concerns like logging and validation.

### Design Patterns

- Repository Pattern
- Unit of Work Pattern
- Factory Pattern
- Singleton Pattern

## Technologies

- .NET 8
- Entity Framework Core
- MediatR
- FluentValidation
- AutoMapper
- Serilog
- JWT for Authentication

## Getting Started

1. Clone the repository
   ```
   git clone https://github.com/Oranged9922/TaskManager.git
   ```
2. Navigate to the project directory
   ```
   cd TaskManager\src
   ```
3. Restore NuGet packages
   ```
   dotnet restore
   ```
4. Run the application
   ```
   dotnet run
   ```

## Features

- Task CRUD operations
- Task assignment and status updates
- User roles and permissions
- Event-driven notifications
- Reporting

## Contributing

Contributions are welcome! Please read the [contributing guidelines](CONTRIBUTING.md) first.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

Feel free to modify this README to better suit your project's specific needs.
