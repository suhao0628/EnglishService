namespace EnglishService.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();
        IQueryable<T> AllAsNoTracking();
    }
}
