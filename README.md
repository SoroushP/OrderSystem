# OrderSystem

A layered **ASP.NET Web API** order management system built with **.NET Framework 4.8**, **Dapper**, and **Autofac**.

The project is designed as a practical backend project to demonstrate clean separation of concerns, dependency injection, data-access abstraction, and RESTful API design in a classic .NET Framework environment.

## Architecture

The solution is organized into four main projects:

```text
OrderSystem
│
├── Domain
│   ├── Models
│   ├── DTOs
│   └── Enums
│
├── Service
│   ├── Interfaces
│   └── Business Services
│
├── DapperDataAccess
│   ├── Repositories
│   └── Database Access
│
└── OrderSystem
    ├── Controllers
    ├── Dependency Injection
    └── Web API Configuration
```

### Layer Responsibilities

| Layer                | Responsibility                                                  |
| -------------------- | --------------------------------------------------------------- |
| **Domain**           | Domain models, DTOs, enums and shared contracts                 |
| **Service**          | Business logic and application-level operations                 |
| **DapperDataAccess** | SQL queries, database access and Dapper-based persistence       |
| **OrderSystem**      | HTTP API, controllers, configuration and dependency composition |

The dependency direction keeps the Web API layer independent from the concrete data-access implementation.

```text
HTTP Request
     │
     ▼
┌───────────────┐
│   Web API     │
│ Controllers   │
└───────┬───────┘
        │
        ▼
┌───────────────┐
│    Service    │
│ Business Logic│
└───────┬───────┘
        │
        ▼
┌───────────────┐
│ DapperDataAccess │
│  Persistence   │
└───────┬───────┘
        │
        ▼
   SQL Server
```

## Tech Stack

* **.NET Framework 4.8**
* **ASP.NET Web API**
* **C#**
* **Dapper**
* **SQL Server**
* **Autofac**
* **Dependency Injection**
* **RESTful API**

## Key Design Decisions

### Dependency Injection

The application uses **Autofac** as the dependency injection container.

Controllers depend on abstractions rather than concrete implementations:

```csharp
public OrderController(IOrderService orderService)
{
    _orderService = orderService;
}
```

This keeps the API layer decoupled from the business and persistence implementations and makes the components easier to replace and test.

### Separation of Business Logic and Data Access

Business rules are kept in the Service layer while database-specific operations remain inside `DapperDataAccess`.

This prevents controllers from becoming tightly coupled to SQL queries and database implementation details.

```text
Controller
    ↓
IOrderService
    ↓
OrderService
    ↓
Repository / Data Access
    ↓
Dapper
    ↓
SQL Server
```

### Dapper

Dapper is used as the data-access layer instead of a full ORM.

This provides:

* Explicit SQL queries
* Low abstraction overhead
* Fine-grained control over database operations
* Lightweight object mapping
* Clear visibility into database interaction

## Domain Model

The core domain currently revolves around the `Order` entity.

An order contains information such as:

```text
Order
├── Id
├── CustomerId
├── TotalAmount
├── Status
├── CreatedAt
└── UpdatedAt
```

Order state is represented using an enum rather than relying on magic strings or numeric values.

## API

The Web API exposes endpoints for working with orders.

Typical operations include:

```text
GET     /api/order
GET     /api/order/{id}
POST    /api/order
PUT     /api/order/{id}
DELETE  /api/order/{id}
```

The controller is intentionally kept thin. Request handling is delegated to the Service layer where application and business logic belongs.

## Dependency Injection Flow

The composition root is responsible for registering application dependencies.

Conceptually:

```text
IOrderService
      │
      ▼
OrderService
      │
      ▼
IOrderRepository
      │
      ▼
OrderRepository
```

The Web API layer does not need to know which concrete implementation is used for each abstraction.

This allows implementations to be replaced without changing the controller contract.

## Database

The project uses **SQL Server** as its relational database.

The connection is configured through the application's connection string:

```xml
<connectionStrings>
    <add
        name="Order"
        connectionString="Server=.;Database=Order;Trusted_Connection=True;"
        providerName="System.Data.SqlClient" />
</connectionStrings>
```

> Update the connection string according to your local SQL Server configuration.

## Getting Started

### Prerequisites

Make sure the following are installed:

* Visual Studio
* .NET Framework 4.8
* SQL Server
* NuGet

### Clone the Repository

```bash
git clone https://github.com/SoroushP/OrderSystem.git
cd OrderSystem
```

### Configure the Database

Create a SQL Server database named:

```text
Order
```

Then update the connection string in the Web API application's configuration if necessary.

### Restore Dependencies

Restore NuGet packages from Visual Studio or using NuGet Package Restore.

### Build

Build the solution:

```text
OrderSystem.sln
```

### Run

Start the ASP.NET Web API project from Visual Studio.

The API can then be accessed through the configured local IIS Express / ASP.NET Web API endpoint.

## Project Structure

### Domain

Contains the application's core contracts and domain definitions.

The Domain project should remain independent from infrastructure-specific concerns.

### Service

Contains application and business logic.

Responsibilities include:

* Validating business rules
* Coordinating application operations
* Calling data-access abstractions
* Returning application-level results

### DapperDataAccess

Contains database-specific implementation.

Responsibilities include:

* SQL queries
* Connection management
* Dapper execution
* Mapping database records
* CRUD operations

### OrderSystem

The API entry point.

Responsibilities include:

* HTTP request handling
* Controllers
* Dependency injection configuration
* Web API configuration
* Application composition

## Why Dapper?

Dapper was selected to keep database interaction explicit and lightweight.

Unlike a full ORM, Dapper does not hide SQL behind a large abstraction layer. This project intentionally keeps SQL visible so that database behavior, query structure, and performance characteristics remain easy to reason about.

## Design Goals

The main goals of this project are:

* Clear separation of responsibilities
* Loose coupling through interfaces
* Dependency Injection
* Thin controllers
* Explicit business logic
* Lightweight data access
* Maintainable project structure
* Practical ASP.NET Web API implementation

## Future Improvements

Potential improvements for future iterations include:

* Unit and integration tests
* Global exception handling
* Request/response validation
* Structured logging
* Authentication and authorization
* API versioning
* Pagination and filtering
* Transaction management
* Repository abstractions and query optimization
* Dockerized SQL Server development environment
* CI/CD pipeline
* Swagger / OpenAPI documentation

## Learning Focus

This project is intentionally implemented using **classic .NET Framework 4.8** rather than ASP.NET Core.

The purpose is to demonstrate understanding of backend fundamentals that remain relevant across .NET versions:

* Dependency Injection
* SOLID principles
* Layered architecture
* HTTP API design
* Database access
* SQL performance
* Async programming
* Separation of concerns
* Application composition

---

## License

This project is intended as a learning and portfolio project.
