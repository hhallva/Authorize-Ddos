using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataLayer.Models;

public partial class Skill
{
    public int SkillId { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Vacancy> Vcancies { get; set; } = new List<Vacancy>();
}
