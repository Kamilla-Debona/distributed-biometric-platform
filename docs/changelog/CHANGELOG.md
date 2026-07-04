# Changelog

All notable changes to this project will be documented in this file.

---

# v0.1.0 - Foundation

Date: 2026-06-14

## Architecture

Implemented:

* DistributedBiometricPlatform solution
* Clean Architecture structure
* Layered architecture:

  * API
  * Application
  * Domain
  * Infrastructure
  * Contracts

## Domain

Implemented:

* Client
* Gallery
* BiographicData
* Person
* Enrollment
* BiometricSample
* BiometricTemplate
* Identification
* IdentificationCandidate

## Application

Implemented:

* CreateEnrollmentCommand
* CreateEnrollmentHandler
* CreateEnrollmentResponse

## Persistence

Implemented:

* Entity Framework Core
* PostgreSQL
* DbContext
* Entity configurations
* Repositories
* Unit of Work

## Storage

Implemented:

* IObjectStorage
* LocalObjectStorage

## Messaging

Implemented:

* Initial messaging abstraction
* InMemoryMessageBus

## API

Implemented:

* Swagger
* EnrollmentsController
* POST /api/enrollments endpoint

## Functional Flow

End-to-end enrollment workflow implemented:

1. Receive enrollment request
2. Create BiographicData
3. Create Person
4. Create Enrollment
5. Store biometric image
6. Create BiometricSample
7. Persist data to the database
8. Prepare the enrollment for asynchronous processing

---

# v0.2.0 - Enrollment Processing

Date: 2026-06-21

## Enrollment Processing

Implemented:

* ProcessEnrollmentCommand
* ProcessEnrollmentHandler
* Enrollment processing workflow

## Biometrics

Implemented:

* IBiometricEngine
* FakeBiometricEngine
* CreateSubjectResult

## Subject Management

Implemented:

* Subject creation during enrollment processing
* Subject linkage to Person

## Template Management

Implemented:

* BiometricTemplate creation
* Template persistence
* Vector identifier storage

## Status Management

Implemented:

Enrollment statuses:

* Requested
* Processing
* Completed

Person statuses:

* PendingEnrollment
* Enrolled

## Messaging

Implemented:

* InMemoryMessageBus orchestration
* ProcessEnrollmentCommand
* Enrollment processing through command dispatching

## Functional Flow

Enrollment workflow now executes end-to-end:

1. Create enrollment request
2. Store biometric sample
3. Publish enrollment event
4. Process enrollment
5. Create subject
6. Create biometric template
7. Update quality score
8. Mark person as enrolled
9. Mark enrollment as completed


---

# v0.3.0 - Wolverine Messaging

Date: 2026-07-04

## Messaging

Implemented:

* Replaced the custom messaging abstraction with Wolverine
* Removed the custom IMessageBus abstraction
* Removed the InMemoryMessageBus implementation
* Direct command dispatch through Wolverine
* In-process command handling

## Application

Implemented:

* Refactored CreateEnrollmentHandler to dispatch ProcessEnrollmentCommand through Wolverine
* Simplified messaging infrastructure
* Removed intermediate messaging components

## Functional Flow

Enrollment workflow now executes through Wolverine:

1. Create enrollment request
2. Persist enrollment data
3. Dispatch ProcessEnrollmentCommand through Wolverine
4. Execute ProcessEnrollmentHandler
5. Create Subject
6. Create BiometricTemplate
7. Update biometric quality score
8. Mark Person as Enrolled
9. Mark Enrollment as Completed

## Next Version

### v0.4.0

Planned features:

* Identification workflow
* Candidate ranking
* Enrollment failure handling
* Kafka integration
