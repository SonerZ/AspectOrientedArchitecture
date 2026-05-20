using System;
using System.Collections.Generic;
using System.Text;
using Application.Packages.JWT.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Application.Packages.JWT.Configuration
{
    public class JWTConfiguration : IJWTConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public int ExpiryHour { get; set; }
        public EnumTokenSecurityAlgorithms TokenSecurityAlgorithms { get; set; }
    }
}
