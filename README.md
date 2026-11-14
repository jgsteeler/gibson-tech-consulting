---
title: Gibson Tech Consulting
summary: A public architecture portfolio and toolkit built around Gibson Coffee Works.
---

# Gibson Tech Consulting

A public architecture portfolio and evolving toolkit of reference applications,
focused proofs of concept, and migration playbooks.

**Gibson Coffee Works is my AdventureWorks:** a shared fictional business for
demonstrating how I want applications to be built and how existing applications
can improve without requiring a wholesale rewrite.

## Approach

Build the well-engineered **after** first. Create focused **before** examples
when a specific problem needs a practical migration path.

- Establish clear business rules and security boundaries.
- Prove solutions with tests and reproducible delivery.
- Demonstrate incremental improvements under realistic constraints.
- Document prerequisites, tradeoffs, verification, and rollback.
- Extract reusable packages only after repeated use proves the abstraction.

The shared business provides continuity, not a universal starter application.
Each example should introduce only the complexity its subject requires.

## Current status

This repository currently contains architecture documentation and an
implementation plan. There is no runnable application or CI/CD pipeline yet.

The planned Vue and ASP.NET Core reference application will demonstrate:

- A customer placing an order.
- A barista fulfilling the order.
- A manager approving a refund.
- A same-origin BFF with server-owned OAuth credentials and API-enforced
  authorization.

Optional capabilities are added when requirements justify them, not because
every application needs every architectural component.

## Start here

- [Gibson Coffee Works decision](./docs/gibson-coffee-works-decision.md):
  domain, reference-first development, and consulting delivery model.
- [Secure SPA architecture](./docs/secure-spa-architecture.md):
  security boundaries, token lifecycle, and composable capabilities.
- [Reference application implementation plan](./plan/architecture-gibson-coffee-works-1.md):
  proposed phases, acceptance criteria, and decisions required before coding.

## Open-source boundary

The original code and documentation published in this repository are licensed
under the [MIT License](./LICENSE).

Dependencies, including Divergent Flow libraries, retain their own licenses
and are not covered by this repository's MIT license. Individual libraries may
be open source or proprietary and may require a separate commercial license
or licensing fee.

Each example must identify its dependencies, their applicable license terms,
and any access or purchase requirements. Do not assume that a dependency is
free to use or redistribute because the example is MIT-licensed. Proprietary
library source code is not published here.

Examples use synthetic business data. Client code, confidential information,
credentials, and private dependency-feed tokens do not belong in this repository.
