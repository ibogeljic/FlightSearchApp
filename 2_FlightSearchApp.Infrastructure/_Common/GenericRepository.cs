using FlightSearchApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSearchApp.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        #region Fields
        private bool disposed = false;
        //protected readonly DbContext _context;
        protected readonly FSADbContext _context = new FSADbContext();
        protected readonly DbSet<T> _dbset;
        #endregion

        #region CTOR
        public GenericRepository(/*DbContext context*/)
        {
            //_context = context;
            _context = new FSADbContext();
            _dbset = _context.Set<T>();
        }
        #endregion

        #region Methods
        public T Find(int id)
        {
            return _dbset.Find(id);
        }
        public void Create(T entity)
        {
            _dbset.Add(entity);
        }

        public void CreateRange(T[] entities)
        {
            _dbset.AddRange(entities);
        }

        public void CreateSave(T entity)
        {
            _dbset.Add(entity);
            _context.SaveChanges();
        }

        public T Read(int id)
        {
            return _dbset
                .AsNoTracking()
                .SingleOrDefault(x => x.ID == id);
        }

        public T ReadTracking(int id)
        {
            return _dbset
                .SingleOrDefault(x => x.ID == id);
        }

        public IEnumerable<T> ReadAll()
        {
            return QueryDbset()
                .AsEnumerable();
        }

        public IQueryable<T> QueryDbset()
        {
            return _dbset
                .AsNoTracking();
        }

        public IQueryable<T> QueryDbsetTracking()
        {
            return _dbset;
        }

        public void UpdatePartially(T entity, List<string> properties)
        {
            _context.Attach(entity);
            foreach (string s in properties)
                _context.Entry(entity).Property(s).IsModified = true;
            Save();
            Detach(entity);
        }

        public void Update(T entity)
        {
            if (!Exists(entity))
                _context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateSave(T entity)
        {
            if (!Exists(entity))
                _context.Entry(entity).State = EntityState.Modified;

            Save();
            Detach(entity);
        }

        public void Delete(int id)
        {
            var obj = _dbset.Find(id);
            _dbset.Remove(obj);
        }

        public void DeleteSave(int id)
        {
            Delete(id);
            Save();
        }

        public void CreateUpdate(T entity)
        {
            if (entity.ID == 0)
                Create(entity);
            else
                Update(entity);
        }

        public void CreateUpdateSave(T entity)
        {
            CreateUpdate(entity);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        private bool Exists(T entity)
        {
            return _dbset.Local.Any(e => e.ID == entity.ID);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class GenericSoftDeleteRepository<T> : GenericRepository<T>, IGenericSoftDeleteRepository<T> where T : EntitySoftDelete
    {
        #region CTOR
        public GenericSoftDeleteRepository() : base()
        {
        }
        #endregion

        #region Methods
        public IEnumerable<T> ReadAllActive()
        {
            return QueryDbset()
                .Where(e => !e.IsDeleted)
                .AsEnumerable();
        }

        public IQueryable<T> QueryDbsetActive()
        {
            return _dbset
                .AsNoTracking()
                .Where(e => !e.IsDeleted);
        }

        public IQueryable<T> QueryDbsetTrackingActive()
        {
            return _dbset
                .Where(e => !e.IsDeleted);
        }

        public new void Delete(int id)
        {
            _dbset.Find(id).IsDeleted = true;
        }
        #endregion
    }
}
