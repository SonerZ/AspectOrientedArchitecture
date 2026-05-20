using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.DataAccess.Entities.Concrete;

namespace Application.Core.Entities.Concrete
{
    /// <summary>
    /// User entity.
    /// </summary>
    public class UserClaim : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ClaimId { get; set; }
        public Claim Claim { get; set; }
    }
}
