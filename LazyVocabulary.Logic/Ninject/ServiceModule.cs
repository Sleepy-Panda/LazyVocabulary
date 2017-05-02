using LazyVocabulary.DataAccess.Interfaces;
using LazyVocabulary.DataAccess.UnitOfWork;
using Ninject.Modules;

namespace LazyVocabulary.Logic.Ninject
{
    public class ServiceModule : NinjectModule
    {
        private string _connectionString;

        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(_connectionString);
        }
    }
}
