using DomainLab3;
using InfrastructureLab4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLab4.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private BaseDbContext _dbContext;
        private IRepository<Cource> _courceRepository;
        private IRepository<CourceStatus> _courceStatusRepository;
        private IRepository<CourceModule> _courceModuleRepository;

        public IRepository<Cource> CourceRepository { get => _courceRepository; }
        public IRepository<CourceStatus> CourceStatusRepository { get => _courceStatusRepository; }
        public IRepository<CourceModule> CourceModuleRepository { get => _courceModuleRepository; }

        public UnitOfWork(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
            _courceRepository = new Repository<Cource>(dbContext);
            _courceStatusRepository = new Repository<CourceStatus>(dbContext);
            _courceModuleRepository = new Repository<CourceModule>(dbContext);

            _dbContext.Database.Migrate();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
