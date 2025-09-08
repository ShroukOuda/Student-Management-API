using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;


namespace Student_Management_API.Authentication;


public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {

    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Implement your authentication logic here
        if (!Request.Headers.ContainsKey("Authorization"))
            return await Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
        
        var authHeader = Request.Headers["Authorization"].ToString();
        if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        
        var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
        var decodedCredentials = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
        var parts = decodedCredentials.Split(':', 2);
        if (parts.Length != 2)
            return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        
        var username = parts[0];
        var password = parts[1];

        if (username == "admin" && password == "password") // Replace with your validation logic
        {
            var claims = new[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, username) };
            var identity = new System.Security.Claims.ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Claims.ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
        else
        {
            return await Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));
        }   
    }
}
