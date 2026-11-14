---
title: Gibson Coffee Works and the Reference-First Toolkit
date: 2026-11-12
status: Accepted
summary: Use a shared fictional business to develop proven solutions and focused migration examples.
---

# Gibson Coffee Works and the Reference-First Toolkit

## Decision

Use **Gibson Coffee Works** as the shared fictional business for this
repository's PoCs and examples.

Build the well-engineered **after** first. Create focused **before**
implementations when a specific teaching or client scenario needs them.
Do not build one universal starting application and expect it to represent
every client's problems.

This decision establishes direction, not a claim that the reference
application or reusable packages already exist.

## Purpose of the repository

The repository is a toolkit for Gibson Tech Consulting:

- Reference implementations that demonstrate sound architectural decisions.
- Focused PoCs that reproduce a particular engineering problem.
- Before/after examples that demonstrate a practical migration.
- Playbooks that explain how to apply the improvement to an existing system.
- Packages and utilities extracted from patterns proven useful in practice.

The reference application answers: "How would we build this from scratch
under these requirements?" A playbook answers: "What is the smallest safe
path from this client's current constraints to the improvement they need?"

These are related questions, not interchangeable ones. Clients should not
need to rewrite their applications to adopt a useful capability.

## Why Gibson Coffee Works

Coffee is a familiar teaching domain with meaningful business behavior.
Gibson Coffee Works can sell coffee, operate cafes, and service equipment,
but an example introduces only the part of the business it needs.

An order is enough to demonstrate validation, ownership, authorization,
state transitions, persistence, concurrency, and deployment. Subscriptions,
inventory, multiple locations, and equipment servicing remain optional
future scenarios rather than initial requirements.

Alternatives considered:

- **Todo application:** simple, but offers limited business rules.
- **Pet store:** viable, but offers less continuity with earlier coffee examples.
- **Northwind or AdventureWorks:** familiar sample domains, but not a distinct
  Gibson Tech Consulting teaching business.

## Shared-domain rule

> Use Gibson Coffee Works as the shared fictional business. Introduce only
> the entities and workflows needed for each example. Within a before/after
> pair, preserve the business scenario, sample data, and expected business
> outcomes so that the technical improvement is clear.

Share terminology and useful fixtures, not a mandatory application template.
Examples may have different architectures when their teaching objectives
require them.

For a security migration, legitimate users retain their intended workflows;
unauthorized actions that worked in the before must fail in the after.
Document those intentional behavior changes explicitly.

## Initial reference application

The initial business scope is:

| Actor | Capability | Required boundary |
|---|---|---|
| Customer | Place an order and view its progress | Access only their own orders |
| Barista | Fulfill an order | Require fulfillment permission and a valid order transition |
| Manager | Approve a refund | Require refund approval permission and an eligible request |

Use a Vue frontend and an ASP.NET Core backend. Apply the
[secure SPA architecture](./secure-spa-architecture.md) incrementally.

The resource-owning backend enforces business rules and authorization.
Hiding controls in the frontend is a usability feature, not enforcement.

Refund eligibility, who initiates a refund request, store boundaries,
and payment integration must be specified before those features are built.
A recorded approval must not be presented as a completed payment refund.

## Reference security baseline

The reference application follows the secure design, not the intentionally
weak implementation initially discussed:

- Same-origin browser access through a Backend for Frontend (BFF).
- Secure HttpOnly session cookies and CSRF protection for state changes.
- OAuth access and refresh tokens owned by the server, not browser storage.
- OIDC authorization code flow with PKCE.
- API validation of token signatures, trusted issuer, audience, and lifetime.
- Server-side refresh with rotation handling and concurrency protection.
- Resource-level and operation-level authorization at the API.
- Microsoft authentication and token-validation libraries for the .NET
  security plumbing; no handwritten token validation.

A local identity provider may support development, but it must exercise
real protocol flows and cryptographic validation. Test doubles do not
replace those checks in the runnable reference application.

Local-storage tokens, disabled signature validation, and unsafe refresh
handling belong only in separately identified before examples if a future
scenario calls for them. They are not fallback implementations.

mTLS, certificate-bound tokens, gateways, and distributed coordination are
added when deployment requirements or the threat model justify them.
The target architecture is composable, not a requirement to use every box.

## Reference-first development

1. Define the concrete business outcome and relevant architectural rules.
1. Implement a small, working reference slice.
1. Verify correctness, security boundaries, and reproducible delivery.
1. Select a specific problem to illustrate.
1. Create a representative before using synthetic code and data.
1. Demonstrate and test the incremental path to the relevant after.
1. Publish the migration playbook with prerequisites and tradeoffs.
1. Extract shared utilities or packages only when repeated use proves them.

A second real use should validate an abstraction before it becomes a
reusable package, following the existing architecture's extraction rule.
Do not introduce a framework merely to anticipate hypothetical reuse.

## Public portfolio and dependency boundary

This repository is a public, MIT-licensed architecture portfolio: Gibson
Coffee Works serves as Gibson Tech Consulting's own AdventureWorks.

Divergent Flow libraries are consumed as dependencies under their own license
terms. Individual libraries may be open source or proprietary and may require
a commercial license or licensing fee. The repository's MIT license does not
relicense its dependencies.

Do not copy proprietary implementations into this repository. Examples must
document dependency licenses, package access, and any purchase requirements
without publishing private-feed credentials.

## Consulting delivery model

1. Assess the client's required outcome, current architecture, and constraints.
1. Identify the smallest relevant improvement rather than prescribing the
   complete reference architecture.
1. Reproduce the problem in a focused Gibson Coffee Works PoC without
   importing confidential client code, credentials, or data.
1. Demonstrate the migration, including failure cases and operational costs.
1. Deliver a playbook the client's team can execute.
1. Support implementation and architectural decisions through a retainer.

For example, a CI/CD-readiness engagement may require decoupling build-time
configuration, isolating external dependencies, or establishing repeatable
tests. It does not automatically require a new identity system or a BFF.
The reference supplies proven techniques; the client context determines
which techniques belong in the engagement.

## Evidence and completion criteria

Each delivered example must identify:

- The problem, audience, prerequisites, and explicitly excluded concerns.
- How to run it from a clean checkout using documented configuration.
- Tests demonstrating the intended behavior and relevant failure boundaries.
- The exact before/after difference and measurable acceptance criteria.
- Deployment assumptions, operational limitations, and known tradeoffs.
- Incremental migration steps, verification steps, and rollback guidance.

The reference must be buildable, testable, and deployable through automation.
Infrastructure selection and a successful deployment must be demonstrated
before describing it as deployment-ready for that environment.

"After" means verified against stated requirements, not universally perfect
or finished forever.

## Current delivery boundary

The approved first delivery is this decision and the
[reference application implementation plan](../plan/architecture-gibson-coffee-works-1.md).
Application code, insecure before examples, and package extraction are not
part of this documentation delivery.
