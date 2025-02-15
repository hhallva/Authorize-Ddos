using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataLayer.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
}
