namespace Application.Core.Configuration.Context
{
    /// <summary>
    /// This interface provides value of configuration properties.
    /// </summary>
    public interface IApplicationConfigurationContext
    {
        string RabbitMQHost { get; set; }
        int RabbitMQPort { get; set; }
        string RabbitMQUsername { get; set; }
        string RabbitMQPassword { get; set; }

        string ConnectionString { get; set; }

        string JWTKey { get; set; }
        string JWTIssuer { get; set; }
        string JWTAudience { get; set; }
        int JWTExpiryHour { get; set; }
    }
}
