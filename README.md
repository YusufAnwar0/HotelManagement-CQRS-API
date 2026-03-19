# 🏨 Hotel Management System API

A robust and scalable Enterprise-level RESTful API for managing hotel operations. This project demonstrates the practical application of modern software architecture patterns and best practices.

## 🏗 Architecture & Design Patterns
This project is architected using **Clean Architecture** principles to ensure a separation of concerns, maintainability, and testability.

* **Targeted Domain-Driven Design (DDD):** Implemented Rich Domain Models specifically in core entities (like `Reservation` and `Room`) to encapsulate complex business rules, while keeping simpler entities lightweight.
* **CQRS (Command Query Responsibility Segregation):** Segregating read and write operations using **MediatR**.
* **Clean Entity Configuration:** Utilizing EF Core **Fluent API** to configure database schemas, ensuring the Domain entities remain pure and free of data annotations.
* **Repository Pattern:** Abstracting data access and managing transactions.
* **Pipeline Behaviors:** Cross-cutting concerns like Validation and Transaction management are handled centrally.
* **Result Pattern:** Using consistent `ResponseDTO<>` across all layers for standardized responses instead of throwing custom exceptions.

## 🚀 Technologies & Libraries Used
* **Framework:** `.NET 10` (Web API)
* **Database:** `SQL Server` with `Entity Framework Core`
* **Caching:** `Redis` (Distributed Cache)
* **Database Operations:** `EF Core Interceptors` for centralized cross-cutting database operations.
* **CQRS & Mediator:** `MediatR`
* **Validation:** `FluentValidation`
* **Authentication/Authorization:** `JWT` with **Permission-Based Access Control**
* **Mapping:** `AutoMapper`

## ✨ Key Features
* **High-Performance Authorization:** Advanced Permission-based access control with JWT, utilizing **Redis** to cache user permissions and dramatically reduce database hits on restricted endpoints.
* **Advanced EF Core Features:** Leveraging **Interceptors** for automated database operational logic, and **Fluent API** for precise schema definition.
* **Room Management:** CRUD operations for rooms, room types, and dynamic pricing.
* **Reservation System:** Complex business logic for booking, rescheduling, and cancellation with availability checking.
* **Offers & Discounts:** Applying and managing promotional offers on specific room types.
* **Standardized Responses:** Unified API responses utilizing `ResponseDTO<>` for all successful operations and error handling.
* **Transactional Behavior:** Automatically managing database transactions for Commands using Pipeline Behaviors.

## 📁 Project Structure (Clean Architecture)
1. **`Domain`**: Contains Enterprise business rules, Entities, and Enums. (No dependencies).
2. **`Application`**: Contains Application business rules, CQRS Handlers, DTOs, and Interfaces. (Depends only on Domain).
3. **`Infrastructure`**: Contains Data Access (EF Core DbContext, Single Migration, Fluent API Configurations, Interceptors), Repositories, and External Services.
4. **`Presentation`**: The Web API project containing Controllers, Middlewares, and Dependency Injection configurations.

## 🛠 Getting Started

### Prerequisites
* .NET 10 SDK
* SQL Server (LocalDB or standard instance)
* Redis Server (Running locally or via Docker)

### Installation
1. Clone the repository:
   ```bash
   git clone [https://github.com/YusufAnwar0/HotelManagement-CQRS-API.git](https://github.com/YusufAnwar0/HotelManagement-CQRS-API.git)