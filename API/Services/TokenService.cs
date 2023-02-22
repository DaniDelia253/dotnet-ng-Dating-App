using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        //store the super secret key that is used to store the tokens in the configuration
        //in order to get the configuration and inject it into this service...
        //I need to inject IConfiguration through my parameters in my constructor
        private readonly SymmetricSecurityKey _key;
        //^ this I get from the Microsoft IdentityModel.Tokens nuget package I installed in this project
        //there are 2 types of keys:
        //symetric-same key used to encrypt as decrypt meaning server encrypts and decrypts 
        //...vs....
        // asymetric- server encrypts and client decripts (requires both public and private keys) (used for https and ssl)
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            //need to encode this into bytes bc symetric securtiy key takes bytes
        }
        public string CreateToken(AppUser user)
        {
            //inside the token we want to include claims
            var claims = new List<Claim>
            //using a list even though there is only one right now because later it will be desirable to have more
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
                //a clain in this usage is a claim about a user
                //the user might claim a name, bday, etc...
                //I can use this username for many different things (starting with seeing if this user is who they say they are)
            };

            //next I need signing credentials (what I am going to sign this token with)
            var creads = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //describe the token we will return
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creads
            };

            //now need a token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            //create token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}