# Distributed Biometric Platform

Distributed biometric enrollment and identification platform built with .NET, Clean Architecture, PostgreSQL, and event-driven processing.
## Architecture

The solution follows Clean Architecture principles and is organized into the following layers:

- Domain
- Application
- Infrastructure
- API

Current enrollment flow:

Enrollment Request
↓
Enrollment Creation
↓
Biometric Sample Storage
↓
Enrollment Processing
↓
Subject Creation
↓
Template Creation
↓
Enrollment Completion

## Technologies

- .NET 10
- PostgreSQL
- Entity Framework Core
- Clean Architecture

## Current Features

- Enrollment API
- Biographic Data Management
- Image Upload
- Enrollment Processing
- Subject Creation
- Biometric Template Creation
- Event-Driven Workflow

## Roadmap

- Enrollment Processor
- Subject Management
- Biometric Templates
- Kafka Integration
- Identification Workflow