using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtGenerator
{
    internal class Test
    {
        internal static void Run()
        {
            // The key used for encryption
            string encryptionKey = "YourEncryptionKeyHereYourEncryptionKeyHereYourEncryptionKeyHere"; // Replace with your encryption key

            // Create symmetric security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(encryptionKey));

            // Specify the encryption algorithm
            var encryptingCredentials = new EncryptingCredentials(securityKey, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512);

            // Create some claims
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, "John Doe"),
            new Claim(ClaimTypes.Email, "john.doe@example.com"),
            // Add more claims as needed
        };

        // Create token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
            EncryptingCredentials = encryptingCredentials // Assign the encryption details
        };

        // Create a token handler
        var tokenHandler = new JwtSecurityTokenHandler();

        // Generate token
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Write token to console
        Console.WriteLine(tokenHandler.WriteToken(token));
        }
    }
}
