---
title: Gibson Coffee Works Reference Application Contract
date: 2026-11-16
status: Accepted
summary: Approved implementation choices and incremental acceptance gates.
---

# Gibson Coffee Works Reference Application Contract

## Scope and delivery

Implementation is authorized under this contract. Deliver one reviewable
GitHub PR at a time and wait for its merge before starting the next.
GitHub is authoritative; merged `main` commits run CI in GitLab.

The first PR establishes a runnable foundation, not a completed security
implementation. It must not simulate login, issue tokens, accept orders, or
claim that the security guarantees below already hold.

## Approved technology choices

| Concern | Decision |
|---|---|
| Browser | Vue 3 with TypeScript and Vite; npm with a committed lockfile |
| Backend | ASP.NET Core on .NET 10 LTS; Microsoft authentication libraries |
| Local development | Aspire AppHost, pinned to 13.6.5 |
| Identity provider | Local Keycloak with real OIDC, not an identity-switch mock |
| Persistence | PostgreSQL, including durable business data |
| First deployment target | Local containers; hosted environment deferred |
| Application dispatch | Source-generated martinothamar/Mediator; not MediatR |
| CI | GitLab Free; GitHub Actions only transports merged source |

Pin the .NET SDK, Node runtime, package versions, and container tags in
source. Use .NET SDK 10.0.401 and Node 24.22.0 for the initial build.
Use the supported .NET 10 LTS line rather than requiring the newest STS runtime.
Update pins deliberately with compatibility tests.

Use the same-origin BFF as the browser's only application entry point.
Keycloak has its own identity-provider origin. Development dashboard and
administration endpoints are local tools, not public application endpoints.
Do not expose database, API, Keycloak administration, or Aspire dashboard
ports publicly.

## Topology and state

Start with one BFF instance and one API instance. Do not claim multi-instance
support until shared-state and refresh-coordination tests demonstrate it.
The eventual token/session store is PostgreSQL; encrypt persisted OAuth
credentials with ASP.NET Core Data Protection. Keep encryption keys durable
and outside the database backups they protect; document provisioning and
recovery before container deployment is accepted.

The foundation starts PostgreSQL and Keycloak but does not yet implement the
business schema or session store. Keycloak development mode is explicitly a
local-development dependency, not a production identity deployment.

Keep independent databases for application data and identity-provider state.
Persist development resource data using named volumes. Do not reset those
volumes automatically as a workaround for startup or migration failures.

## Initial business rules

All data and identities are synthetic. One store and one currency are in scope.
Use USD, integer minor units, server-owned menu prices, and positive integer
quantities. Tax, tips, payment capture, inventory, and delivery are out of scope.

- A customer places an order and reads only orders they own.
- The server derives ownership from the verified issuer and subject, never
  from a customer identifier supplied in a request.
- Order states are `Placed` and `Fulfilled`. A barista with fulfillment
  permission can perform the transition exactly once.
- The owning customer can request one full refund after fulfillment.
- Refund request states are `Requested` and `Approved`.
- A manager with refund approval permission can approve an eligible request
  exactly once. Approval is not payment execution.
- Refund state does not erase the order's fulfillment history.
- Permissions are independent; a manager does not implicitly have barista
  or customer ownership privileges.

Mutations require an idempotency key scoped to the verified actor and operation.
An identical retry returns the original successful result. Reusing the key
with a different payload returns `409 Conflict`. Enforce uniqueness and state
transitions transactionally; a concurrent conflicting transition returns 409
without a second effect. A retry must still pass current authentication and
authorization before reading any stored idempotency response.

Reject invalid input with 400, unauthenticated API access with 401, and
unauthorized operations with 403. Return 404 for another customer's order
to avoid revealing its existence. Use Problem Details for API errors.

## Security acceptance gate

The protected-order PR must demonstrate:

- Authorization code flow with PKCE and Microsoft OIDC middleware.
- Secure HttpOnly host-only session cookies; no tokens in browser storage,
  browser-readable cookies, response bodies, or logs.
- Server-owned token/session storage, expiration, logout, and refresh rotation.
- Concurrent refresh coordination consistent with the chosen session store.
- API signature, trusted issuer, audience, and lifetime validation using
  Microsoft bearer authentication and token-validation libraries.
- CSRF protection for state-changing session-authenticated endpoints.
- YARP destination/path/method allowlists and server-controlled token attachment.
- API enforcement of ownership, permissions, and state transitions.
- Separate 401 and 403 behavior; a 403 does not initiate a login loop.

An unimplemented capability must remain unavailable, not be replaced with
fake success. Test-only identities must not be enabled in the runnable app.
Defer mTLS, certificate-bound tokens, gateways, and reusable package extraction.

## Incremental verification

1. **Foundation:** restore locked dependencies, type-check/build Vue, build
   the .NET solution, run boundary tests, and start the AppHost. Verify
   PostgreSQL, Keycloak discovery, the API liveness route, and the BFF-hosted
   frontend. Reserved backend paths must not return a successful SPA page.
1. **Protected customer slice:** verify real login, place/read an owned order,
   negative token and ownership cases, CSRF, refresh rotation, and logout.
1. **Staff slice:** verify fulfillment and refund approval using independent
   sessions, direct API attacks, retries, and concurrent updates.
1. **Container deployment:** build immutable images, provision secrets and
   TLS outside source control, apply migrations, run smoke checks, restart
   services without losing data, and demonstrate rollback.

Commit exact commands to the README as each phase becomes runnable. CI uses
the same build/test commands. A foundation liveness check does not attest
to authentication, persistence readiness, or deployment readiness.

Before any destructive schema change, define backup/restore and a compatible
application rollback. Do not equate rolling back an image with rolling back
database state.

## Sources and related decisions

- [Domain and reference-first decision](./gibson-coffee-works-decision.md)
- [Secure SPA architecture](./secure-spa-architecture.md)
- [Implementation plan](../plan/architecture-gibson-coffee-works-1.md)
- [Aspire PostgreSQL hosting](https://aspire.dev/integrations/databases/postgres/postgres-host/)
- [Aspire JavaScript hosting](https://aspire.dev/integrations/frameworks/javascript/)
- [Keycloak](https://www.keycloak.org/)
