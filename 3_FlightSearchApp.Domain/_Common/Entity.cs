using System.ComponentModel.DataAnnotations;

namespace FlightSearchApp.Domain
{
    public abstract class Entity
    {
        [Key]
        public int ID { get; set; }
    }
    public abstract class EntitySoftDelete : Entity
    {
        public bool IsDeleted { get; set; } = false;
    }
}