# PAYMENTS
This is the payments microservice for the e-commerce application. It handles all payment-related operations, including processing payments, managing payment methods, and integrating with third-party payment gateways.

# Project Structure
src/
 ├── Api
 │    ├── Endpoints (expor os endpoints como Minimal APIs)
 │    └── Filters (middlewares e filtros de validação se aplicavel)
 │    └── Security (implementação de autenticação e autorização)
 ├── Application
 │    ├── Services (orquestração da lógica de negócio)
 │    ├── DTOs (estruturas de saída e entrada)
 │    └── Validators (validações de entrada)
 ├── Domain
 │    ├── Entities (Entidades ricas, com invariantes e lógica de negócio)
 │    ├── ValueObjects (Value Objects com validação própria)
 │    └── Interfaces (interfaces de serviços e repositórios)
 └── Infrastructure
      ├── Data
      │    ├── Entities
      │    │     └── PaymentEntity.cs
      │    ├── Repositories
      │    │     └── PaymentRepository.cs
      │    ├── Configurations
      │    │     └── PaymentEntityConfiguration.cs
      │    ├── Mappers
      │    │     └── PaymentMapper.cs
      │    └── ApplicationDbContext.cs
      └── Providers
```
