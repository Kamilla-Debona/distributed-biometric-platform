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

```text
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
- Biometric engine abstractions

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
4. A BiometricSample is created and persisted as an enrollment sample.
5. The Enrollment is committed to the database.
6. A `ProcessEnrollmentCommand` is sent through Wolverine.
7. `ProcessEnrollmentHandler` executes the enrollment workflow.
8. The biometric engine creates a Subject.
9. A BiometricTemplate is created.
10. The Person is marked as **Enrolled**.
11. The Enrollment is marked as **Completed**.

---

## Identification Processing Foundation

The platform now has the foundation for biometric identification processing.

Current identification-related capabilities include:

- Probe-compatible BiometricSample model
- Identification aggregate
- IdentificationCandidate entity
- Identification repositories
- ProcessIdentificationCommand
- ProcessIdentificationHandler
- Biometric engine search contracts

The identification workflow is being prepared to support:

1. Client sends an identification request.
2. API stores the probe image as a probe sample.
3. An Identification record is created.
4. A `ProcessIdentificationCommand` is dispatched through Wolverine.
5. The biometric engine searches candidates in the selected gallery.
6. External subject identifiers are mapped back to platform Subjects.
7. IdentificationCandidate records are created.
8. The Identification is completed with candidates or completed with no match.

The Identification API is planned as the next step.

---

## Biometric Engine

The biometric engine is abstracted through application contracts.

Current operations include:

- Create subject
- Search candidates
- Delete subject

The current implementation is a fake engine used for development and architectural validation. Future implementations may integrate real biometric SDKs or external providers.

---

## Messaging

The platform currently uses Wolverine for in-process command dispatching.

This architecture allows the messaging infrastructure to evolve to external brokers such as Kafka without changing the application workflow.

---

## Future Evolution

Planned architectural improvements include:

- Identification API
- Candidate ranking
- Enrollment failure handling
- Kafka integration
- Background workers
- Distributed message processing
- Vector database integration
- Multiple biometric engine providers
- Scalable identification workflow
