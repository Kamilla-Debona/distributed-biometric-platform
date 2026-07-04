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

The Application layer orchestrates use cases through commands and handlers.

The Infrastructure layer provides concrete implementations for persistence, messaging, storage, and external integrations.

The API layer exposes the application through HTTP endpoints.

---

## Consequences

### Benefits

- Clear separation of concerns
- Improved maintainability
- High testability
- Infrastructure can evolve independently from business logic
- Easier replacement of messaging, storage, or biometric providers
- Well-suited for distributed and event-driven architectures

### Trade-offs

- More projects and abstractions than a monolithic CRUD application
- Additional upfront design effort
- Higher architectural complexity in exchange for long-term flexibility
