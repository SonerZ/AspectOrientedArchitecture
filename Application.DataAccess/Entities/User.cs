using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.DataAccess.Entities.Concrete;

namespace Application.Core.Entities.Concrete
{
    /// <summary>
    /// User entity.
    /// </summary>
    public class User : BaseEntity<Guid>
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
