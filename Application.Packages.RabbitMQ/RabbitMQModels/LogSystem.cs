using System;

namespace Application.Packages.RabbitMQ.RabbitMQModels;

public class LogSystem  : IRabbitMQBaseModel
{
    public Guid? UserId { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public string Level { get; set; }
    public bool IsItFixed = false;
    public string ServerName { get; set; }
    public string ServerIp { get; set; }
    public DateTime LogDate { get; set; }
    public string Func { get; set; }
}

