using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using BasicAuth.Storage.Repository.UserRepository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace BasicAuth.Authentication;


public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserEntityRepository _userRepositoty;

    public BasicAuthHandler(ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IOptionsMonitor<AuthenticationSchemeOptions> monitor, IUserEntityRepository userRepository) : base(monitor, logger, encoder, clock)
    {
        _userRepositoty = userRepository;
    }
    

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {

        // check to see if the request header contains the 'Authorization key'

        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Missing Authorization Header");


        try
        {
            // Get the value of the Authorization key

            // var authValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            var authValue = Request.Headers["Authorization"];
            var parsedAuthValue = AuthenticationHeaderValue.Parse(authValue);



            // check to ensure the parsed auth value is not null, and whether it starts with the correct Authentication scheme (in this case "Basic")
            if (parsedAuthValue?.Scheme is null)
            {
                return AuthenticateResult.Fail("Invalid auth [NULL]");
            }


            // if (string.Equals(parsedAuthValue.Scheme, "basic", StringComparison.OrdinalIgnoreCase))
            //     return AuthenticateResult.Fail("Invalid authentication scheme");

            if (!string.Equals(parsedAuthValue.Scheme, "basic", StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.Fail("Invalid authentication scheme");



            // extract and decode the base64string within Auth value
            var encodedBytes = Convert.FromBase64String(parsedAuthValue.Parameter ?? string.Empty);

            var decodedAuthValue = Encoding.UTF8.GetString(encodedBytes);

            var splitDecodedValue = decodedAuthValue.Split(new[] { ':' }, 2);

            var userId = splitDecodedValue[0];
            var password = splitDecodedValue[1];



            // Use the decoded value to verify the user's existence
            var user = await _userRepositoty.GetUserByIdAsync(userId);

            if (user is null || password != "Password")
                return AuthenticateResult.Fail("Unauthorized");



            // Build the claims for the user
            var claims = new Claim[]
            {
                // new("FirstName", user.FirstName),

                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };



            // build the user's claim Identity and principal
            var claimIdentity = new ClaimsIdentity(claims, Scheme.Name);

            var claimPrincipal = new ClaimsPrincipal(claimIdentity);



            // Create Authenticated ticket
            var authenticatedTicket = new AuthenticationTicket(claimPrincipal, Scheme.Name);


            // return the authenticated ticket
            return AuthenticateResult.Success(authenticatedTicket);
        }

        catch (Exception)
        {
            return AuthenticateResult.Fail("Something went wrong while authenticating user");
        }
    }
}