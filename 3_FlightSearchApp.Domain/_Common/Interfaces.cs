
namespace FlightSearchApp.Domain
{
    #region Generic repository
    public interface IGenericRepository<T> where T : Entity
    {
        void Create(T entity);
        void CreateRange(T[] entities);
        void CreateSave(T entity);
        T Find(int id);
        T Read(int id);
        T ReadTracking(int id);
        IEnumerable<T> ReadAll();
        IQueryable<T> QueryDbset();
        IQueryable<T> QueryDbsetTracking();
        void UpdatePartially(T entity, List<string> properties);
        void Update(T entity);
        void UpdateSave(T entity);
        void Delete(int id);
        void DeleteSave(int id);
        void CreateUpdate(T entity);
        void CreateUpdateSave(T entity);
        void Save();
    }
    public interface IGenericSoftDeleteRepository<T> : IGenericRepository<T> where T : EntitySoftDelete
    {
        IEnumerable<T> ReadAllActive();
        IQueryable<T> QueryDbsetActive();
        IQueryable<T> QueryDbsetTrackingActive();
    }
    #endregion

    #region CodeListRepostitory
    public interface ICodeListRepository : IGenericSoftDeleteRepository<CodeList>
    {
        IEnumerable<CodeList> ReadAllActiveOrdered();
        IEnumerable<CodeList> ReadAllForEntityForCombo(CodesEnum entity);
    }
    #endregion

    #region FlighOfferResporitory
    public interface IFlightOfferRepository : IGenericRepository<FlightOffer>
    {
        void CheckAndSaveNonDuplicate(FlightOffer flightOffer);
    }
    #endregion
}
