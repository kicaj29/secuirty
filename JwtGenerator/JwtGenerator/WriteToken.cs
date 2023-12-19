using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtGenerator
{
    internal class WriteToken
    {
        internal static void Run()
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var header = new JwtHeader();
            header.Add(JwtHeaderParameterNames.Typ, "at+jwt");
            header.Add(JwtHeaderParameterNames.X5t, "jOwA9xVoRvRqxlWV4PuCmXSHOw4");
            header.Add(JwtHeaderParameterNames.Kid, "8CEC00F7156846F46AC65595E0FB829974873B0E");

            var payload = new JwtPayload(issuer: "http://logging.capture.hylandqa.net:9870/idp", audience: "sss", new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Nbf, "1702893549"),
                new Claim(JwtRegisteredClaimNames.Iat, "1702893549"),
                new Claim(JwtRegisteredClaimNames.Exp, "1702897149"),
                new Claim(JwtRegisteredClaimNames.Amr, "pwd"),
                new Claim("scope", "openid"),
                new Claim("scope", "profile"),
                new Claim("scope", "sss"),
                new Claim("client_id", "sss:clients:platform-swagger"),
                new Claim(JwtRegisteredClaimNames.Sub, "01ad0911-e6f1-441e-8f37-f93b5264bca9"),
                new Claim(JwtRegisteredClaimNames.AuthTime, "1702893549"),
                new Claim("idp", "local"),
                new Claim("sss_account", "5714d708-b092-7430-3f8d-583f4bdca247"),
                new Claim(JwtRegisteredClaimNames.Sid, "F203E61E7174C3F02D73F43545FE7727"),
                new Claim(JwtRegisteredClaimNames.Jti, "36F752829279378B25369D5F68FA63FB"),
            }, notBefore: null, expires: null);


            var token = new JwtSecurityToken(header, payload);

            string tokenString = jwtHandler.WriteToken(token);
            Console.WriteLine(tokenString);
            Console.WriteLine("WriteToken END");
        }
    }
}
