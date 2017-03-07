using LazyVocabulary.DAL.Interfaces;
using LazyVocabulary.DAL.UnitOfWork;
using Ninject.Modules;

namespace LazyVocabulary.BLL.Ninject
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
