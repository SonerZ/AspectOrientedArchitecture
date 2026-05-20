using System;

namespace Application.Entities.CustomEntities.User;

public class ActivityCreateDto
{
        public string ActivityType { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Description {get;set;}
        public int Count{get;set;}
        public bool CheckValue{get;set;}
}