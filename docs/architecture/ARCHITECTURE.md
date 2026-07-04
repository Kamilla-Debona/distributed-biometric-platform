# Architecture

## Overview

Distributed Biometric Platform is a distributed biometric enrollment and identification system built with .NET using Clean Architecture and message-driven workflows.

The platform is designed to support:

- Biometric enrollment
- Biometric identification
- Distributed processing
- Multiple biometric engines
- Event-driven workflows
- Pluggable messaging and storage providers

---

## Solution Structure

```
src/
├── BiometricPlatform.Api
├── BiometricPlatform.Application
├── BiometricPlatform.Domain
└── BiometricPlatform.Infrastructure
```

---

## Layer Responsibilities

### Domain

Contains:

- Entities
- Aggregates
- Value Objects
- Domain rules
- Domain behavior

The Domain layer is framework-independent and contains only business logic.

### Application

Contains:

- Commands
- Handlers
- Application services
- Repository abstractions
- Enrollment and identification workflows

The Application layer orchestrates use cases without depending on Infrastructure implementations.

### Infrastructure

Contains:

- Entity Framework Core persistence
- PostgreSQL integration
- Local object storage
- Wolverine messaging
- Fake biometric engine
- External integrations

### API

Contains:

- REST controllers
- Request/response contracts
- Dependency injection
- Swagger/OpenAPI configuration

---

## Current Enrollment Flow

1. Client sends an enrollment request.
2. API creates the Enrollment, Person and BiographicData.
3. The biometric image is uploaded to object storage.
4. A BiometricSample is created and persisted.
5. The Enrollment is committed to the database.
6. A `ProcessEnrollmentCommand` is sent through Wolverine.
7. `ProcessEnrollmentHandler` executes the enrollment workflow.
8. The biometric engine creates a Subject.
9. A BiometricTemplate is created.
10. The Person is marked as **Enrolled**.
11. The Enrollment is marked as **Completed**.

---

## Messaging

The platform currently uses Wolverine for in-process command dispatching.

This architecture allows the messaging infrastructure to evolve to external brokers such as Kafka without changing the application workflow.

---

## Future Evolution

Planned architectural improvements include:

- Kafka integration
- Background workers
- Distributed message processing
- Vector database integration
- Multiple biometric engine providers
- Scalable identification workflow
