using WorldModel;

namespace WorldCitiesApi.Dtos
{
    public class MakeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Model> Models { get; set; }

        public int CountingNum { get; set; }

        public int Sumsales { get; set; }
    }
}
