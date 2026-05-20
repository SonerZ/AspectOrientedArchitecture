using System;

namespace Application.Entities.CustomEntities.User;

public class ActivityUpdateDto
{
        public Guid Id { get; set; }
        public string ActivityType { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Description {get;set;}
        public int Count{get;set;}
        public bool Check{get;set;}
}