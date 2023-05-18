using WorldModel;

namespace WorldCitiesApi.Dtos
{
    public class ModelDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Sales { get; set; }
        public virtual ICollection<Car> Cars { get; set; }

        public int CountingNum { get; set; }
    }
}
