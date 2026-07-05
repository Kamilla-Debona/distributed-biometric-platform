# Architecture

## Overview

Distributed Biometric Platform is a distributed biometric enrollment and identification platform built with .NET using Clean Architecture and asynchronous message-driven workflows.

The platform is designed to support scalable biometric processing through pluggable storage, messaging, and biometric engine providers.

The platform currently supports:

- Biometric enrollment
- Biometric identification
- Distributed processing
- Multiple biometric engines
- Event-driven workflows
- Pluggable messaging providers
- Pluggable object storage providers

---

## Solution Structure

```text
src/
├── BiometricPlatform.Api
├── BiometricPlatform.Application
├── BiometricPlatform.Contracts
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
- Command handlers
- Application services
- Repository abstractions
- Enrollment workflows
- Identification workflows
- Message-driven orchestration
- Biometric engine abstractions

The Application layer orchestrates use cases without depending on Infrastructure implementations.

### Infrastructure

Contains:

- Entity Framework Core persistence
- PostgreSQL integration
- Local object storage
- Wolverine in-process messaging
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
6. A `ProcessEnrollmentCommand` is dispatched through Wolverine.
7. `ProcessEnrollmentHandler` executes the enrollment workflow.
8. The biometric engine creates an external biometric subject.
9. A platform Subject is created and linked to the external subject.
10. A BiometricTemplate is created.
11. The Person aggregate is marked as **Enrolled**.
12. The Enrollment is marked as **Completed**.

---

## Current Identification Flow

1. Client submits an identification request.
2. The probe image is uploaded to object storage.
3. A probe `BiometricSample` is created.
4. An `Identification` aggregate is persisted.
5. The transaction is committed.
6. A `ProcessIdentificationCommand` is dispatched through Wolverine.
7. `ProcessIdentificationHandler` executes the identification workflow.
8. The biometric engine searches the selected gallery.
9. External subject identifiers are mapped back to platform `Subject` entities.
10. Each `Subject` is resolved to its corresponding `Person`.
11. Ranked `IdentificationCandidate` records are persisted.
12. The `Identification` aggregate is updated as:
    - `Completed` when candidates are found;
    - `NoMatch` when no candidates satisfy the search;
    - `Failed` when an unexpected error occurs.

---

## Identification Query

The platform exposes an identification query endpoint that allows clients to retrieve the complete result of a previously submitted identification.

The query returns:

- Identification metadata
- Processing status
- Failure reason, when applicable
- Ranked candidate list
- Person identifier
- Subject identifier
- Matching score
- Candidate rank

This endpoint reads persisted data only and never invokes the biometric engine.

---

## Biometric Engine

The biometric engine is abstracted through application contracts.

Current operations include:

- Subject creation
- Candidate search
- Subject deletion

The current implementation is a fake biometric engine used for development and architectural validation.

The fake engine produces deterministic search results using enrolled platform subjects, allowing the complete enrollment and identification workflows to be exercised without requiring a commercial biometric SDK.

Future implementations may integrate commercial biometric SDKs or external biometric providers while preserving the application workflow.

---

## Messaging

The platform currently uses Wolverine for in-process command dispatching.

Enrollment and identification processing are executed asynchronously through message handlers, allowing the messaging infrastructure to evolve to external brokers such as Kafka without changing the application layer.

---

## Future Evolution

Planned architectural improvements include:

- Configurable identification thresholds
- Candidate filtering and quality rules
- Identification pagination
- Identification history endpoints
- Enrollment retry policies
- Identification retry policies
- Background workers
- Kafka integration
- Distributed message processing
- Multiple biometric engine providers
- Real biometric SDK integration
- Vector database integration
- Horizontal scaling