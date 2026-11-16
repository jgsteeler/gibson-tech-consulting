<template>
  <main>
    <header>
      <p class="eyebrow">Gibson Tech Consulting / Reference application</p>
      <h1>Gibson Coffee Works</h1>
      <p class="intro">Good coffee. Clear boundaries. Built with intention.</p>
    </header>

    <section aria-labelledby="status-heading">
      <p class="eyebrow">Foundation milestone</p>
      <h2 id="status-heading">The doors are not open yet.</h2>
      <p>
        This Vue application is served by its ASP.NET Core backend for frontend.
        The local Aspire environment starts PostgreSQL and Keycloak alongside it.
      </p>
      <p>
        Sign-in, ordering, fulfillment, and refund approval are not implemented.
        This is a runnable foundation, not a finished secure application.
      </p>
    </section>

    <section aria-labelledby="journey-heading">
      <h2 id="journey-heading">What comes next</h2>
      <ol>
        <li><strong>Customer</strong>Place an order and follow its progress.</li>
        <li><strong>Barista</strong>Fulfill an order with the right permissions.</li>
        <li><strong>Manager</strong>Approve an eligible full-refund request.</li>
      </ol>
    </section>
  </main>
</template>

<style>
:root {
  font-family: system-ui, sans-serif;
  color: #2e261e;
  background: #f5f0e7;
  line-height: 1.65;
}

body {
  margin: 0;
}

main {
  max-width: 62rem;
  margin: auto;
  padding: clamp(1.5rem, 5vw, 4rem);
}

header {
  padding-block: 2rem;
  border-bottom: 2px solid #827055;
}

h1 {
  font-family: Georgia, serif;
  font-size: clamp(2.5rem, 7vw, 4.5rem);
  line-height: 1.1;
  margin-block: 1rem;
}

h2 {
  line-height: 1.3;
}

.eyebrow {
  font-size: 0.8rem;
  font-weight: 700;
  letter-spacing: 0.1em;
  text-transform: uppercase;
}

.intro {
  font-size: 1.2rem;
}

section {
  margin-top: 2.5rem;
}

p {
  max-width: 44rem;
}

ol {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(13rem, 1fr));
  gap: 1rem;
  padding-left: 1.5rem;
}

li {
  padding: 1rem;
}

strong {
  display: block;
}
</style>
