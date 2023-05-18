using System.ComponentModel.DataAnnotations;

namespace WorldModel;

public partial class Model
{
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; }

    public int Sales { get; set; }
    public int MakeId { get; set; }

    public virtual ICollection<Car> Cars { get; } = new List<Car>();
}
