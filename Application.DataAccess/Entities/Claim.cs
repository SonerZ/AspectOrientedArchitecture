using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.DataAccess.Entities.Concrete;

namespace Application.Core.Entities.Concrete
{
    /// <summary>
    /// User entity.
    /// </summary>
    public class Claim : BaseEntity<Guid>
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
