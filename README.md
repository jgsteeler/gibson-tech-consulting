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
implementation plan. There is no runnable application or application deployment
pipeline yet.

The planned Vue and ASP.NET Core reference application will demonstrate:

- A customer placing an order.
- A barista fulfilling the order.
- A manager approving a refund.
- A same-origin BFF with server-owned OAuth credentials and API-enforced
  authorization.

Optional capabilities are added when requirements justify them, not because
every application needs every architectural component.

## GitHub source of truth and GitLab CI

Edit code and merge pull requests on
[GitHub](https://github.com/jgsteeler/gibson-tech-consulting).
The [forwarding workflow](./.github/workflows/sync-gitlab.yml) sends merged
`main` commits to the private
[GitLab project](https://gitlab.com/gibson-tech-works/consulting/gibson-coffee-works).
This uses GitLab Free, not Premium pull mirroring.

Only `main` is synchronized. Tags, feature branches, fork PRs, issues, and
reviews are not copied. Do not edit GitLab `main`: synchronization rejects
divergent history rather than overwriting it. The original GitLab onboarding
history is preserved as `archive/gitlab-initial-2026-11-16`.

The [GitLab pipeline](./.gitlab-ci.yml) checks that the expected documentation
is present and includes GitLab's SAST and secret-detection templates.
SAST jobs depend on supported source files being present; a successful
documentation smoke check is not proof of application correctness or deployment readiness.
GitLab runs and their findings remain in GitLab; status reporting to GitHub
and pre-merge application checks are not configured.

The GitHub repository secret `GITLAB_SYNC_SSH_KEY` holds a dedicated
project-scoped, write-enabled GitLab deploy key. It is not a personal API token.
Keep deployment credentials in GitLab, not in this transport workflow.
Run the GitHub workflow manually on `main` to retry a failed synchronization;
inspect the Actions logs for transport failures and GitLab pipelines for CI failures.
An already-synchronized commit does not create another push pipeline.
Do not force-push to resolve divergence without first reviewing and preserving it.

To rotate the deploy key, add a replacement public key in GitLab, grant it
access to protected `main`, replace `GITLAB_SYNC_SSH_KEY` in GitHub, verify
synchronization, then revoke the old key. Host-key verification uses
[GitLab's published SSH host key](https://docs.gitlab.com/user/gitlab_com/#ssh-known_hosts-entries);
review official key changes before updating the workflow.

Free hosted-runner usage remains subject to GitLab's compute quota and account
verification requirements. Do not expose protected variables to untrusted code.

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
