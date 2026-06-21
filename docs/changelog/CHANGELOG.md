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

* IMessageBus
* InMemoryMessageBus
* EnrollmentRequestedMessage

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
8. Publish EnrollmentRequested event

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

* EnrollmentRequestedMessage
* InMemoryMessageBus orchestration
* Enrollment processing through message publishing

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

## Next Version

### v0.3.0

Planned features:

* Wolverine integration
* Command and message handling through Wolverine
* Identification workflow
* Candidate ranking
* Enrollment failure handling
