using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Petzfinder.Util
{
    public class DecodeJWT
    {
        public static JwtSecurityToken GetDecodedJWT(string token)
        {
            token = token.Replace("Bearer ", string.Empty);
            return new JwtSecurityToken(token);
        }

        public static string GetAccountEmail(string token)
        {
            var decoded = GetDecodedJWT(token);
            return decoded.Payload.Where(w => w.Key == "email").Select(s => s.Value).FirstOrDefault().ToString();
        }
    }

    
}
