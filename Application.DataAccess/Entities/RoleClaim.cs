using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.DataAccess.Entities.Concrete;

namespace Application.Core.Entities.Concrete
{
    /// <summary>
    /// User entity.
    /// </summary>
    public class RoleClaim : BaseEntity<Guid>
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Guid ClaimId { get; set; }
        public Claim Claim { get; set; }
    }
}
