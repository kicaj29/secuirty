using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JwtGenerator
{
    internal class CreateEncodedJwt
    {
        internal static void Run()
        {
            //var key = "Ym7AD3OT2kpuIRcVAXCweYhV64B0Oi9ETAO6XRbqB8LDL3tF4bMk9x/59PljcGbP5v38BSzCjD1VTwuO6iWA8uzDVAjw2fMNfcT2/LyRlMOsynblo3envlivtgHnKkZj6HqRrG5ltgwy5NsCQ7WwwYPkldhLTF+wUYAnq28+QnU=";
            //var hmac = new HMACSHA512(Convert.FromBase64String(key));

            // The key used for encryption
            string encryptionKey = "YourEncryptionKeyHere"; // Replace with your encryption key

            // Create symmetric security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(encryptionKey));

            var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%1212312321";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.AdditionalHeaderClaims = new Dictionary<string, object>
            {
                { JwtHeaderParameterNames.Typ, "at+jwt" },
                { JwtHeaderParameterNames.X5t, "jOwA9xVoRvRqxlWV4PuCmXSHOw4" },
                { JwtHeaderParameterNames.Kid, "8CEC00F7156846F46AC65595E0FB829974873B0E" }
            };
            tokenDescriptor.TokenType = "at+jwt";

            tokenDescriptor.Claims = new Dictionary<string, object>
            {
                { JwtRegisteredClaimNames.Nbf, "1702893549" },
                { JwtRegisteredClaimNames.Iat, "1702893549" },
                { JwtRegisteredClaimNames.Exp, "1702897149" },
                { JwtRegisteredClaimNames.Amr, "pwd" },
                { "scope", new string[] { "openid", "profile", "sss" } },
                { "client_id", "sss:clients:platform-swagger" },
                { JwtRegisteredClaimNames.Sub, "01ad0911-e6f1-441e-8f37-f93b5264bca9" },
                { JwtRegisteredClaimNames.AuthTime, "1702893549" },
                { "idp", "local" },
                { "sss_account", "5714d708-b092-7430-3f8d-583f4bdca247" },
                { JwtRegisteredClaimNames.Sid, "F203E61E7174C3F02D73F43545FE7727" },
                { JwtRegisteredClaimNames.Jti, "36F752829279378B25369D5F68FA63FB" },
            };
            tokenDescriptor.SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature);
            //tokenDescriptor.EncryptingCredentials = new EncryptingCredentials(key: mySecurityKey, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512);
            tokenDescriptor.EncryptingCredentials = new EncryptingCredentials(key: securityKey, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512);
            tokenDescriptor.CompressionAlgorithm = CompressionAlgorithms.Deflate;

            var jwtHandler = new JwtSecurityTokenHandler();
            string tokenString = jwtHandler.CreateEncodedJwt(tokenDescriptor);
            Console.WriteLine(tokenString);
            Console.WriteLine("CreateEncodedJwt END");
        }
    }
}
