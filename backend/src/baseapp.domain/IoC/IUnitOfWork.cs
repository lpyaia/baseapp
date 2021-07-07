namespace BaseApp.Domain.IoC
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}