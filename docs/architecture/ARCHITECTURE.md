# Architecture

## Overview

Distributed Biometric Platform is a biometric enrollment and identification system built using Clean Architecture principles.

The platform is designed to support:

* Biometric enrollment
* Biometric identification
* Distributed processing
* Multiple biometric engines
* Event-driven workflows

---

## Solution Structure

src/

* BiometricPlatform.Api
* BiometricPlatform.Application
* BiometricPlatform.Domain
* BiometricPlatform.Infrastructure

---

## Layer Responsibilities

### Domain

Contains:

* Entities
* Aggregates
* Business rules
* Domain behavior

The Domain layer does not depend on any external framework.

### Application

Contains:

* Use cases
* Commands
* Handlers
* Interfaces
* Application workflows

### Infrastructure

Contains:

* Database access
* Storage providers
* Messaging providers
* External integrations

### API

Contains:

* Controllers
* Request/Response contracts
* Dependency injection configuration

---

## Current Enrollment Flow

1. Client sends enrollment request
2. API creates Enrollment
3. Biographic data is stored
4. Image is uploaded
5. BiometricSample is created
6. EnrollmentRequested message is published

Future versions will introduce asynchronous processing through Kafka.
