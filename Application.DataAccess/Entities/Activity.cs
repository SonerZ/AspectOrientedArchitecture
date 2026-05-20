using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.DataAccess.Entities.Concrete;

namespace Application.Core.Entities.Concrete
{
    /// <summary>
    /// User entity.
    /// </summary>
    public class Activity : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
         public User User { get; set; }
        public string ActivityType { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Description{get;set;}
        public int Count{get;set;}
        public bool CheckValue{get;set;}
    }
}
