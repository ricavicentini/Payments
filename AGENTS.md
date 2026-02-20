## .NET Development Standards

### APIs
- Use Minimal APIs by default unless controllers are explicitly required.
- Prefer C# 10+ features to keep the code concise and expressive.
- Design APIs for simplicity and developer experience.
- Follow RESTful conventions when applicable.
- Always version public APIs.

### Dependency Injection
- Enforce the Dependency Injection (DI) pattern.
- Avoid static classes for business logic.
- Use constructor injection only (no service locator).

### Configuration & Secrets
- Never store sensitive data in appsettings.json.
- Use:
  - User Secrets (local development)
  - Environment variables (production)
  - Secret managers (e.g., AWS Secrets Manager, Azure Key Vault)

### Error Handling
- Do not use exceptions for flow control.
- Exceptions must be thrown only for unexpected failures.
- Use the Result pattern for expected outcomes. Preferred library: OneOf. But Alternative options can be evaluated when necessary.

## Testing Standards
### Unit Tests
- All public methods/functions must have unit tests.
- Use xUnit as the default testing framework.
- Follow the AAA pattern (Arrange, Act, Assert).
- Tests must be deterministic and independent.

### Integration Tests
- All API endpoints must have integration tests.
- Use WebApplicationFactory for API testing.
- Cover:
  - Success scenarios
  - Validation errors
  - Authorization/authentication when applicable
- Test Naming Convention: 
  MethodName_ShouldExpectedBehavior_WhenCondition
- Exemple
  CreateOrder_ShouldReturnBadRequest_WhenPayloadIsInvalid


## Architecture Guidelines

### General Principles
- Follow SOLID principles.
- Apply Clean Architecture:
  - Domain
  - Application
  - Infrastructure
  - API
- Enforce separation of concerns.
- Ensure high cohesion and low coupling.

### DDD & Hexagonal Architecture
- Organize projects to support:
  - Domain-driven design (DDD)
  - Ports and Adapters (Hexagonal)
- Domain must not depend on Infrastructure.
- Use interfaces for external dependencies.

### Scalability & Performance
- Design for horizontal scalability.
- Prefer stateless services.
- Use async/await properly.
- Avoid blocking calls.

## Documentation

### ADRs & RFCs
#### When to Create
Create an ADR or RFC when:
- Introducing a new architectural pattern
- Adding a new infrastructure dependency
- Making breaking API changes
- Choosing between multiple technical approaches
Location:
```bash
/Docs/adr
/Docs/rfc
```
ADR Template:
```bash
# ADR-XXX: Title

## Status
Proposed | Accepted | Deprecated

## Context
Describe the problem and constraints.

## Decision
Describe the chosen solution.

## Consequences
Pros and cons of the decision.
```

Project Structure
```bash
src/
 ├─ Domain
 ├─ Application
 ├─ Infrastructure
 ├─ Api

tests/
 ├─ UnitTests
 ├─ IntegrationTests

Docs/
 ├─ adr
 ├─ rfc
 ```

## Code Review Checklist
.NET
[] Uses Minimal APIs (if applicable)
[] Uses DI correctly
[] No sensitive data in config files
[] Proper async usage
[] No exceptions for flow control
[] Result pattern applied

Tests
[] Public methods covered by unit tests
[] Endpoints covered by integration tests
[] Meaningful test names
[] Deterministic tests
Architecture
[] Respects Clean Architecture layers
[] Domain has no infrastructure dependencies
[] SOLID principles applied

Every time you find an issue make a Geek joke e.g. if a clea architecture issue "Ancle Bob would cry blod tears"

## Refactoring Guidance
When analyzing code:
- Suggest refactorings aligned with these guidelines.
- - Explain the rationale for each suggestion.

Prioritize:
- Correctness
- Maintainability
- Performance
- Readability

## Definition of Done

A feature is considered done when:
- Code follows this guideline
- Unit tests are implemented
- Integration tests are implemented
- Documentation (ADR/RFC if needed) is created
- Code review checklist is satisfied