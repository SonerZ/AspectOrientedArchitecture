using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.DataAccess.Entities.Concrete;

namespace Application.Core.Entities.Concrete
{
    /// <summary>
    /// User entity.
    /// </summary>
    public class Role : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
