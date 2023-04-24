using FlightSearchApp.Domain;

namespace FlightSearchApp.Application
{
    #region Interfaces
    public interface ICodeListService : IGenericSoftDeleteService<CodeList>
    {
        object ReadEntitiesForCombo(bool filter);
        object ReadAllForEntityForCombo(CodesEnum entity);
        List<string[]> ReadForDT(string entity);
        List<CodeList> ReadAllForEntity(CodesEnum entity);
    }
    #endregion

    #region Services
    public class CodeListService : GenericSoftDeleteService<CodeList>, ICodeListService
    {
        private readonly ICodeListRepository _Repository;
        public CodeListService(ICodeListRepository repository) : base(repository)
        {
            _Repository = repository;
        }

        public object ReadEntitiesForCombo(bool filter)
        {
            List<object> list = new();

            if (filter)
                list.Add(Globals.ComboEmptyRow());

            foreach (var i in Enum.GetValues(typeof(CodesEnum)))
                if ((int)i < 100)
                    list.Add(new { id = i.ToString(), text = i.ToString() });

            return list.ToArray();
        }
        public object ReadAllForEntityForCombo(CodesEnum entity)
        {
            return Globals.ReadForCombo(false, _Repository.ReadAllForEntityForCombo(entity).
             Select(o => new { id = o.ID.ToString(), text = o.Code }));
        }
        public List<string[]> ReadForDT(string entity)
        {
            var rows = new List<string[]> { };

            var list = _Repository.ReadAllActive().ToList();

            if (entity != "-1")
                list = list.Where(s => s.Entity == entity).ToList();

            list.ForEach(s =>
            {
                rows.Add(
                    new string[]
                    {
                        s.Code,
                        s.UpdateTD,
                        s.DeleteTD
                    });
            });

            return rows;
        }
        public List<CodeList> ReadAllForEntity(CodesEnum entity) 
        {
            return _Repository.ReadAllForEntityForCombo(entity).ToList();
        }
    }
    #endregion
}