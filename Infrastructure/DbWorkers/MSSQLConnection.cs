using System.Data.SqlClient;
using System.Reflection.Metadata;
using Infrastructure.DbWorkers;
using Infrastructure.DbWorkers.Common;

namespace Infrastructure.DbWorkers
{
    public class MSSQLConnection: IDbConnection
    {
        private readonly string CONNECTION_STRING;
        private SqlConnection connection;
        private SqlTransaction transaction;
        private SqlCommand command;

        public MSSQLConnection(string connectionString)
        {
            CONNECTION_STRING = connectionString;
        }

        public IDbConnection BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public IDbConnection CloseConnection()
        {
            throw new NotImplementedException();
        }

        public IDbConnection Commit()
        {
            throw new NotImplementedException();
        }

        public DbDto Execute(string sqlRequest)
        {
            throw new NotImplementedException();
        }

        public DbDto Execute(string sqlRequest, List<Parameter> parametersList)
        {
            throw new NotImplementedException();
        }

        public IDbConnection OpenConnection()
        {
            return new MSSQLConnection("Host=localhost; Database=ips_labs_3; Username=postgres; Password=12345678; Port= 5432");
        }

        public IDbConnection Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
