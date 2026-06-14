# Changelog

All notable changes to this project will be documented in this file.

---

# v0.1.0 - Foundation

Date: 2026-06-14

## Architecture

* Created the DistributedBiometricPlatform solution
* Implemented a Clean Architecture structure
* Defined the following layers:

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
* ConsoleMessageBus

## API

Implemented:

* Swagger
* EnrollmentsController
* POST /api/enrollments endpoint

## Functional Flow

End-to-end Enrollment workflow implemented:

1. Receive enrollment request
2. Create BiographicData
3. Create Person
4. Create Enrollment
5. Store biometric image
6. Create BiometricSample
7. Persist data to the database
8. Publish EnrollmentRequested event

## Next Version

### v0.2.0

Planned features:

* FakeBiometricEngine
* EnrollmentConsumer
* CreateSubject workflow
* Automatic Enrollment status updates
* Automatic Person status updates
