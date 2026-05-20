using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Application.Packages.JWT.Entities;
using Application.Packages.JWT.Result;

namespace Application.Packages.JWT.Service
{
    public interface ITokenService
    {
        /// <summary>
        /// Generate JWT token with specified claims.
        /// </summary>
        /// <param name="claims">Claims</param>
        /// <returns></returns>
        GenerateTokenResult Generate(IEnumerable<Claim> claims);
        /// <summary>
        /// Read specified token. It returns token status. It is valid or is not valid. Return value contains token status, expiry date and claims.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        ResolveTokenResult ResolveToken(string token);
    }
}
