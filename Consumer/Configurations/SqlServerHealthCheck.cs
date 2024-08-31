using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Data.SqlClient;

namespace Consumer.Configurations;

/// <summary>
/// precisar criar a table para o Quartz
/// https://github.com/quartznet/quartznet/tree/main/database/tables
/// </summary>
public class SqlServerHealthCheck : IHealthCheck
{
    readonly string _connectionString;

    public SqlServerHealthCheck(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("quartz");
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync(cancellationToken);

            await using var command = connection.CreateCommand();

            command.CommandText = "SELECT 1";

            await command.ExecuteScalarAsync(cancellationToken);

            return HealthCheckResult.Healthy("SqlServer");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("SqlServer", ex);
        }
    }
}
