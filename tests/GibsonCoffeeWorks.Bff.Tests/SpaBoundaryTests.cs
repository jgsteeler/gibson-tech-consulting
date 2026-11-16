using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GibsonCoffeeWorks.Bff.Tests;

/// <summary>Verifies that SPA navigation cannot hide missing backend endpoints.</summary>
[TestClass]
public sealed class SpaBoundaryTests
{
    /// <summary>Unimplemented API and authentication endpoints must not return HTML success.</summary>
    [TestMethod]
    [DataRow("/api/orders", "GET")]
    [DataRow("/api/orders", "POST")]
    [DataRow("/api", "GET")]
    [DataRow("/auth/login", "GET")]
    [DataRow("/auth/logout", "POST")]
    public async Task ReservedBackendPathDoesNotReturnSpa(string path, string method)
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost"),
            AllowAutoRedirect = false
        });
        using var request = new HttpRequestMessage(new HttpMethod(method), path);

        using var response = await client.SendAsync(request);

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        Assert.AreEqual("application/problem+json", response.Content.Headers.ContentType?.MediaType);
    }

    /// <summary>Client-side navigation continues to serve the compiled Vue application.</summary>
    [TestMethod]
    public async Task FrontendNavigationReturnsSpa()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });

        using var response = await client.GetAsync("/orders");
        var html = await response.Content.ReadAsStringAsync();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("text/html", response.Content.Headers.ContentType?.MediaType);
        StringAssert.Contains(html, "<div id=\"app\">");
    }
}
