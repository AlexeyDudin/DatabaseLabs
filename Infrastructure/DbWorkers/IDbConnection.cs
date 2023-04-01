using DomainLab3.Models.Dtos;
using Infrastructure.DbWorkers.Common;

namespace Infrastructure.DbWorkers
{
    public interface IDbConnection
    {
        public void OpenConnection();
        public DbDto Execute(string sqlRequest);
        public DbDto Execute(string sqlRequest, List<Parameter> parametersList);
        public void BeginTransaction();
        public void Commit();
        public void Rollback();
        public void CloseConnection();

    }
}
