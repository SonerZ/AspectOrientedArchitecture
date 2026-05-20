using System;

namespace Application.Packages.RabbitMQ;

public static class QueueConst
{
    public static string Error = "Error";
    public static string Performance = "Performance";
    public static string Warning= "Warning";
    
    public static string InternetServerError= "Internet Server Error (500)";
    
    public static string LogSystem = "LogSystem";

    public static Guid UserId = Guid.Parse("aca9546a-195b-45e9-b11f-649fc2dabd34");
}