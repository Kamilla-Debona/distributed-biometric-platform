# ADR-001 - Adopt Clean Architecture

## Status

Accepted

---

## Context

The Distributed Biometric Platform is intended to become a scalable, distributed biometric enrollment and identification system.

The platform must support:

- Multiple biometric engine providers
- Event-driven workflows
- Distributed processing
- Pluggable infrastructure components
- Independent evolution of business logic and technical implementations

Current and planned integrations include:

- PostgreSQL
- Object Storage
- Wolverine
- Kafka
- Vector Databases
- External Biometric Engines

These requirements demand a clear separation between business rules, application workflows, infrastructure concerns, and external technologies.

---

## Decision

Adopt Clean Architecture as the foundational architecture of the platform.

The solution is organized into the following layers:

- Domain
- Application
- Infrastructure
- API

The dependency flow is inward:

```text
API
    ↓
Application
    ↓
Domain

Infrastructure
    ↑
```

The Domain layer contains only business concepts and must remain independent of frameworks and infrastructure.

The Application layer orchestrates use cases through commands and handlers. This includes the Enrollment and Identification workflows.

The Infrastructure layer provides concrete implementations for persistence, messaging, storage, and external integrations.

The API layer exposes the application through HTTP endpoints.

---

## Current Architectural Decisions

The platform currently uses:

- PostgreSQL and Entity Framework Core for persistence
- Wolverine for in-process command dispatching
- Local object storage for development
- A fake biometric engine for development and workflow validation

The biometric engine is exposed through application-level abstractions. This keeps the domain and application workflows independent from a specific biometric SDK or vendor.

The Identification workflow uses probe-compatible biometric samples because an identification probe does not belong to a known person before matching.

---

## Consequences

### Benefits

- Clear separation of concerns
- Improved maintainability
- High testability
- Infrastructure can evolve independently from business logic
- Easier replacement of messaging, storage, or biometric providers
- Well-suited for distributed and event-driven architectures
- Enrollment and identification workflows can evolve independently

### Trade-offs

- More projects and abstractions than a monolithic CRUD application
- Additional upfront design effort
- Higher architectural complexity in exchange for long-term flexibility
- More explicit modeling is required for biometric concepts such as subjects, templates, probes, and candidates
