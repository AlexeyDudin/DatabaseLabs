using Infrastructure.DbWorkers.Common;
using System.Reflection.Metadata;

namespace Infrastructure.DbWorkers
{
    public interface IDbConnection
    {
        public IDbConnection OpenConnection();
        public DbDto Execute(string sqlRequest);
        public DbDto Execute(string sqlRequest, List<Parameter> parametersList);
        public IDbConnection BeginTransaction();
        public IDbConnection Commit();
        public IDbConnection Rollback();
        public IDbConnection CloseConnection();

    }
}
