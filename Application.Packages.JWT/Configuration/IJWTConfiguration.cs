using System;
using System.Collections.Generic;
using System.Text;
using Application.Packages.JWT.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Application.Packages.JWT.Configuration
{
    /// <summary>
    /// Jwt configuration provides information for signing token.
    /// </summary>
    public interface IJWTConfiguration
    {
        /// <summary>
        /// Audience
        /// </summary>
        string Audience { get; set; }
        /// <summary>
        /// Issuer
        /// </summary>
        string Issuer { get; set; }
        /// <summary>
        /// Secret key.
        /// </summary>
        string SecretKey { get; set; }
        /// <summary>
        /// Expiry hour.
        /// </summary>
        int ExpiryHour { get; set; }
        /// <summary>
        /// Security algorithms
        /// </summary>
        EnumTokenSecurityAlgorithms TokenSecurityAlgorithms { get; set; }
    }
}
