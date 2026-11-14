---
goal: Build the Gibson Coffee Works secure reference application incrementally
version: 1
date_created: 2026-11-12
last_updated: 2026-11-14
owner: Gibson Tech Consulting
status: 'On Hold'
tags: [architecture, reference-application, vue, dotnet]
---

# Introduction

![Status: On Hold](https://img.shields.io/badge/status-On%20Hold-orange)

Implement the trustworthy after before deriving scenario-specific befores.
This plan is a proposed implementation sequence, not implemented behavior.
Only the documentation delivery is approved. Application implementation is
on hold until the Phase 1 decisions are recorded and implementation is authorized.

Paths below are proposed new paths relative to the workspace root.
Do not create all projects or features at once.

## 1. Requirements & Constraints

- **REQ-001**: Use Gibson Coffee Works as the fictional business.
- **REQ-002**: Use Vue with TypeScript for customer, barista, and manager views.
- **REQ-003**: Use ASP.NET Core for the BFF and resource API.
- **REQ-004**: Support order placement, fulfillment, and refund approval.
- **REQ-005**: Preserve data across application restarts and enforce transitions
  atomically under concurrent requests.
- **REQ-006**: Demonstrate repeatable builds, tests, and deployment automation.
- **SEC-001**: Keep OAuth access and refresh tokens server-side; never put
  them in local storage, session storage, browser-readable cookies, or SPA responses.
- **SEC-002**: Use Microsoft .NET authentication and validation libraries;
  validate signatures, issuer, audience, and lifetime.
- **SEC-003**: Use same-origin sessions, CSRF defenses, and API-owned
  authorization. UI role visibility is not an authorization boundary.
- **SEC-004**: Use real OIDC code flow with PKCE in the runnable application.
  Keep test authentication handlers restricted to automated test hosts.
- **SEC-005**: Fail closed on refresh failure; protect refresh rotation from
  concurrent use and never log credentials.
- **CON-001**: No insecure before, payment processor, inventory system,
  multi-store tenancy, gateway, mTLS, or reusable package in the initial slice.
- **CON-002**: Do not silently substitute in-memory persistence or fake
  authentication to make a demonstration appear complete.
- **CON-003**: Publish repository code and documentation under MIT. Consume
  Divergent Flow libraries only as dependencies under their own terms.
  Document whether each dependency is open source or proprietary and any
  licensing fees or access requirements; never publish proprietary
  implementations or feed credentials.
- **PAT-001**: Use CQRS with source-generated `martinothamar/Mediator`,
  not MediatR, for .NET application commands and queries.
- **PAT-002**: Use a constrained YARP proxy when connecting the BFF to the API;
  allowlist routes and destinations and replace browser-supplied bearer credentials.
- **GUD-001**: Choose supported dependency versions and verify official
  documentation when implementation begins; this plan does not pin unverified versions.

## 2. Implementation Steps

### Implementation Phase 1

- GOAL-001: Record the implementation contract before scaffolding.
- Dependencies: None. This is the execution gate for all later phases.
- Completion: Every choice below is recorded and application implementation is approved.

| Task | Description | Completed | Date |
|------|-------------|-----------|------|
| TASK-001 | In `docs/reference-application-contract.md`, record .NET, Node, Vue, package manager, local identity provider, database, session/token store, deployment target, and CI provider selections. Record single-instance or distributed deployment assumptions. | No | Not started |
| TASK-002 | In the same contract, define order states, actor permissions, refund requester, eligibility, full or partial refunds, and whether approval is separate from execution. Define idempotency and concurrent-update responses. | No | Not started |
| TASK-003 | In the same contract, define acceptance commands, deployment smoke checks, secret provisioning, and rollback expectations. Mark implementation authorized before proceeding. | No | Not started |

### Implementation Phase 2

- GOAL-002: Establish a reproducible repository skeleton and delivery checks.
- Dependencies: TASK-001, TASK-002, TASK-003.
- Completion: Clean checkout restores, builds, and runs an initial test through
  the documented commands locally and in CI.

| Task | Description | Completed | Date |
|------|-------------|-----------|------|
| TASK-004 | Scaffold `src/GibsonCoffeeWorks.Web/`, `src/GibsonCoffeeWorks.Bff/`, and `src/GibsonCoffeeWorks.Api/` using selected official tools. Pin SDK/runtime requirements and commit dependency lockfiles where supported. | No | Not started |
| TASK-005 | After TASK-004, add focused test projects under `tests/`, a root `README.md`, and the chosen CI configuration. Restore, type-check, build, and run tests using the same documented commands. | No | Not started |

### Implementation Phase 3

- GOAL-003: Implement a secure customer order-placement vertical slice.
- Dependencies: TASK-004, TASK-005.
- Completion: A signed-in customer places and reads their persisted order;
  another customer cannot read it, and unauthenticated writes fail.

| Task | Description | Completed | Date |
|------|-------------|-----------|------|
| TASK-006 | In `src/GibsonCoffeeWorks.Bff/`, implement OIDC login/logout, server-owned sessions/tokens, CSRF protection, secure cookies, allowlisted YARP routes, and credential-safe diagnostics. Implement refresh rotation and coordination appropriate to the selected store. | No | Not started |
| TASK-007 | In `src/GibsonCoffeeWorks.Api/`, implement Microsoft bearer validation, `PlaceOrder` and `GetOrder` handlers, ownership policies, server-calculated totals, durable persistence, and validated order transitions. Derive customer identity from verified claims, not request payloads. | No | Not started |
| TASK-008 | After TASK-006 and TASK-007, implement `src/GibsonCoffeeWorks.Web/src/features/orders/` with menu, cart, order placement, and order status. Show loading, validation, authentication, and authorization failures explicitly. Use same-origin session requests without browser token handling. | No | Not started |
| TASK-009 | After TASK-008, implement the customer, token-validation, CSRF, and refresh tests in Section 6. Document local startup and protocol configuration in `README.md`. | No | Not started |

### Implementation Phase 4

- GOAL-004: Add the barista and manager workflows without weakening boundaries.
- Dependencies: TASK-009.
- Completion: The full business journey passes, including forbidden actions,
  duplicate commands, and concurrent state changes.

| Task | Description | Completed | Date |
|------|-------------|-----------|------|
| TASK-010 | In `src/GibsonCoffeeWorks.Api/`, implement `FulfillOrder`, the approved refund-request workflow, and `ApproveRefund` handlers with operation permissions, eligibility checks, atomic transitions, and actor/time audit records. Do not equate approval with payment execution. | No | Not started |
| TASK-011 | After TASK-010, add barista queue and manager refund views under `src/GibsonCoffeeWorks.Web/src/features/`. Use backend-returned capabilities for presentation; enforce every operation again at the API. | No | Not started |
| TASK-012 | After TASK-011, add automated end-to-end order-to-fulfillment-to-refund-approval coverage and direct API negative tests under `tests/`. Verify state remains unchanged after rejected operations. | No | Not started |

### Implementation Phase 5

- GOAL-005: Demonstrate deployment and document the reference's actual guarantees.
- Dependencies: TASK-012.
- Completion: A clean build deploys to the selected target, survives a restart
  with business data intact, passes smoke checks, and has a verified rollback procedure.

| Task | Description | Completed | Date |
|------|-------------|-----------|------|
| TASK-013 | Add target-specific automation under `deploy/` and the selected CI configuration. Provision secrets outside source control, persist session encryption keys as required, run database migrations safely, and document HTTPS/proxy trust configuration. | No | Not started |
| TASK-014 | After TASK-013, deploy and run login, order placement, fulfillment, refund approval, health, restart, and rollback checks. Record reproducible commands and limitations in `docs/reference-application-operations.md`. | No | Not started |
| TASK-015 | After TASK-014, update `README.md` and this plan with verified capabilities. Keep future before examples and package extraction as separately scoped work. | No | Not started |

## 3. Alternatives

- **ALT-001**: Build the insecure local-storage version first. Rejected for
  this delivery; retain it as a possible future security migration scenario.
- **ALT-002**: Build one universal before application. Rejected because
  client starting points and constraints differ.
- **ALT-003**: Implement every target-architecture component immediately.
  Rejected; add capabilities only when requirements justify them.
- **ALT-004**: Extract a reusable framework before application delivery.
  Rejected until a second use demonstrates the repeated boundary.

## 4. Dependencies

- **DEP-001**: Approved implementation contract and supported toolchain versions.
- **DEP-002**: Selected OIDC provider supporting development identities,
  signing keys, discovery, PKCE, and refresh lifecycle tests.
- **DEP-003**: Microsoft ASP.NET Core authentication libraries, YARP,
  source-generated Mediator, and selected durable storage tooling.
- **DEP-004**: Vue/TypeScript build tooling and focused frontend/backend
  test runners selected during scaffolding.
- **DEP-005**: CI and deployment access with secret provisioning.
  Repository publication does not establish CI or deployment automation;
  those remain implementation work.

## 5. Files

- **FILE-001**: `docs/reference-application-contract.md`: implementation gate.
- **FILE-002**: `src/GibsonCoffeeWorks.Web/`: Vue frontend.
- **FILE-003**: `src/GibsonCoffeeWorks.Bff/`: session and downstream boundary.
- **FILE-004**: `src/GibsonCoffeeWorks.Api/`: resource ownership and business logic.
- **FILE-005**: `tests/`: business, integration, and browser tests.
- **FILE-006**: `deploy/`: selected-target deployment automation.
- **FILE-007**: `README.md`: actual setup, build, test, and run commands.
- **FILE-008**: `docs/reference-application-operations.md`: deployment and rollback.

## 6. Testing

- **TEST-001**: A customer submits valid menu items; the server calculates
  totals, persists an order, and returns it only to its owner or authorized staff.
- **TEST-002**: Reject invalid quantities, unknown products, client-supplied
  prices, and customer identity spoofing without persisting an invalid order.
- **TEST-003**: A barista fulfills an eligible order; customer attempts,
  duplicate fulfillment, and invalid transitions fail without changing state.
- **TEST-004**: A manager approves an eligible refund request; unauthorized,
  duplicate, ineligible, and concurrent approvals cannot apply it twice.
- **TEST-005**: Missing, expired, wrong-issuer, wrong-audience, tampered, and
  unsigned API tokens are rejected. A valid trusted signed token succeeds.
- **TEST-006**: Inspect browser storage, cookies, network responses, and logs
  to confirm OAuth credentials are not exposed; verify secure cookie attributes.
- **TEST-007**: Mutations without valid CSRF protection fail. Anonymous
  requests produce authentication failures; forbidden API actions return 403
  without triggering a login loop.
- **TEST-008**: Concurrent refresh uses rotated credentials safely. Refresh
  failure terminates the affected session; logout prevents further session use.
- **TEST-009**: Proxy route, destination, method, and redirect constraints hold;
  browser Authorization headers cannot replace the server-owned credentials.
- **TEST-010**: The browser journey completes across separate customer,
  barista, and manager sessions. Manipulating UI state cannot grant permission.
- **TEST-011**: CI builds and tests from a clean checkout. Deployment,
  persistence after restart, health checks, and rollback meet the contract.

## 7. Risks & Assumptions

- **RISK-001**: Treating the reference as universally perfect encourages
  unnecessary rewrites. Every capability must state its scope and tradeoffs.
- **RISK-002**: A mock identity switch can conceal protocol failures.
  The runnable reference must retain real signing and validation.
- **RISK-003**: Distributed deployment changes session storage, encryption
  key persistence, and refresh coordination requirements.
- **RISK-004**: Refund approval can be mistaken for money movement.
  Name and display the distinction until payment execution is implemented.
- **ASSUMPTION-001**: The first slice is a single-store business with
  synthetic identities and data, subject to Phase 1 approval.
- **ASSUMPTION-002**: Database, identity provider, hosting, refund semantics,
  and dependency versions remain undecided; no implementation may silently
  choose them by treating this plan as approval.

## 8. Related Specifications / Further Reading

- [Gibson Coffee Works decision](../docs/gibson-coffee-works-decision.md)
- [Secure SPA architecture](../docs/secure-spa-architecture.md)
