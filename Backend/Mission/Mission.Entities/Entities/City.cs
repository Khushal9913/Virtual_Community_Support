using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        public int CountryId { get; set; }

        public string CityName { get; set; }

        public virtual ICollection<Missions> Missions { get; set; } = [];
    }
}
