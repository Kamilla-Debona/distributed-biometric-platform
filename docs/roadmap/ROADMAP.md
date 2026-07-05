# Roadmap

## v0.1.0 - Foundation

* [x] Clean Architecture
* [x] PostgreSQL
* [x] Entity Framework Core
* [x] Enrollment API
* [x] Image Upload
* [x] Local Storage
* [x] Repository Pattern
* [x] Unit of Work

## v0.2.0 - Enrollment Processing

* [x] FakeBiometricEngine
* [x] Enrollment Processing
* [x] Subject Creation
* [x] Biometric Template Creation
* [x] Enrollment Completion
* [x] Person Enrollment Status Update
* [x] Initial InMemoryMessageBus

## v0.3.0 - Messaging Infrastructure

* [x] Wolverine Integration
* [x] Message Bus Migration
* [x] Direct Command Dispatch through Wolverine
* [x] ProcessEnrollmentCommand Handling
* [x] Remove Custom Message Bus Abstraction
* [x] Remove InMemoryMessageBus
* [ ] Enrollment Failure Handling
* [ ] Background Processing

## v0.4.0 - Identification Processing Foundation

* [x] Probe-compatible BiometricSample
* [x] Identification Repository
* [x] Identification Candidate Repository
* [x] Biometric Engine Search Contract
* [x] SearchResult and SearchCandidate
* [x] Subject Lookup by ExternalSubjectId
* [x] Subject Lookup by GalleryId
* [x] ProcessIdentificationCommand
* [x] ProcessIdentificationHandler
* [x] Candidate Mapping Foundation

## v0.5.0 - Identification API

* [ ] Identification API
* [ ] CreateIdentificationCommand
* [ ] CreateIdentificationHandler
* [ ] CreateIdentificationResponse
* [ ] Identification Request Endpoint
* [ ] Candidate Result Endpoint
* [ ] End-to-End Identification Flow
* [ ] Candidate Ranking Validation
* [ ] Score Calculation Refinement
* [ ] Top-K Candidate Selection

## v0.6.0 - Distributed Processing

* [ ] Kafka Integration
* [ ] Kafka Producers
* [ ] Kafka Consumers
* [ ] Worker Services
* [ ] Durable Messaging
* [ ] Distributed Processing

## v0.7.0 - Vector Search

* [ ] Vector Database Integration
* [ ] Qdrant or PGVector
* [ ] Template Search
* [ ] Similarity Search
* [ ] Identification Optimization

## v1.0.0 - Distributed Biometric Platform MVP

* [ ] Enrollment Workflow
* [ ] Identification Workflow
* [x] Event-Driven Architecture
* [ ] Distributed Processing
* [ ] Automated Tests
* [ ] Documentation
* [ ] Production-ready MVP
