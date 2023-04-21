using FlightSearchApp.Domain;

namespace FlightSearchApp.Application
{
    public interface IGenericService<T> where T : Entity
    {
        string Create(T entity);
        T ReadByID(int ID);
        IEnumerable<T> ReadAll();
        void Update(T entity);
        void Delete(int ID);
    }
    public interface IGenericSoftDeleteService<T> : IGenericService<T> where T : EntitySoftDelete
    {
        new void Delete(int ID);
    }
    public abstract class GenericService<T> : IGenericService<T> where T : Entity
    {
        #region Fields
        IGenericRepository<T> _repository;
        #endregion

        #region CTOR
        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        protected Tc DohvatiObjekt<Tc>(Tc obj) where Tc : class
        {
            return obj == null ? (Tc)Activator.CreateInstance(typeof(Tc)) : obj;
        }

        protected ICollection<Tc> DohvatiKolekcijuObjekata<Tc>(ICollection<Tc> obj) where Tc : class
        {
            return obj == null ? (ICollection<Tc>)Activator.CreateInstance(typeof(List<Tc>)) : obj;
        }

        public virtual string Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Create(entity);
            _repository.Save();
            return entity.ID.ToString();
        }

        public virtual T ReadByID(int ID)
        {
            return _repository.Read(ID);
        }

        public virtual IEnumerable<T> ReadAll()
        {
            return _repository.ReadAll();
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Update(entity);
            _repository.Save();
        }

        public virtual void Delete(int ID)
        {
            _repository.Delete(ID);
            _repository.Save();
        }
        #endregion
    }
    public abstract class GenericSoftDeleteService<T> : GenericService<T>, IGenericSoftDeleteService<T> where T : EntitySoftDelete
    {
        #region Fields
        readonly IGenericSoftDeleteRepository<T> _repository;
        #endregion

        #region CTOR
        public GenericSoftDeleteService(IGenericSoftDeleteRepository<T> repository) : base(repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public new void Delete(int ID)
        {
            var entity = _repository.Read(ID);
            entity.IsDeleted = true;

            base.Update(entity);
        }

        public virtual IEnumerable<T> ReadAllActive()
        {
            return _repository.ReadAllActive();
        }
        #endregion
    }
}
