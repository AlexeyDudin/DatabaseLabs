namespace Infrastructure.DbWorkers;
public static class ConnectionProvider
{
    public static IDbConnection GetConnection()
    {
        return new MSSQLConnection("Host=localhost; Database=ips_labs_3; Username=postgres; Password=12345678; Port= 5432");
    }
}