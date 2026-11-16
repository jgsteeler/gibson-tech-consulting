var builder = DistributedApplication.CreateBuilder(args);
const int KeycloakPort = 8180;

var postgres = builder.AddPostgres("postgres")
    .WithImageTag("18.3")
    .WithDataVolume("gibson-coffee-works-postgres");

var database = postgres.AddDatabase("coffee");

var keycloakPassword = builder.AddParameter("keycloak-password", secret: true);

builder.AddContainer("keycloak", "quay.io/keycloak/keycloak", "26.8.2")
    .WithArgs("start-dev")
    .WithEnvironment("KC_BOOTSTRAP_ADMIN_USERNAME", "admin")
    .WithEnvironment("KC_BOOTSTRAP_ADMIN_PASSWORD", keycloakPassword)
    .WithVolume("gibson-coffee-works-keycloak", "/opt/keycloak/data")
    .WithHttpEndpoint(port: KeycloakPort, targetPort: 8080)
    .WithHttpHealthCheck("/realms/master/.well-known/openid-configuration");

builder.AddProject<Projects.GibsonCoffeeWorks_Api>("api")
    .WithReference(database)
    .WaitFor(database)
    .WithHttpHealthCheck("/health/live");

builder.AddProject<Projects.GibsonCoffeeWorks_Bff>("bff")
    .WithHttpHealthCheck("/health/live");

builder.Build().Run();
