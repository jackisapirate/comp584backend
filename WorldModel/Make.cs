using System.ComponentModel.DataAnnotations;

namespace WorldModel;

public partial class Make
{
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Country { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; } = null!;

    public virtual ICollection<Model> Models { get; } = new List<Model>();
}
