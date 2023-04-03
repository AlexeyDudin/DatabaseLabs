using DomainLab3;

namespace InfrastructureLab4.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<Cource> CourceRepository { get; }
        public IRepository<CourceStatus> CourceStatusRepository { get; }
        public IRepository<CourceModule> CourceModuleRepository { get; }

        public void Commit();
    }
}
