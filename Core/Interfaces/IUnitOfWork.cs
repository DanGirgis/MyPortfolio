namespace Core.Interface
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepository <T> Entity { get; }
        void save();
    }
}
