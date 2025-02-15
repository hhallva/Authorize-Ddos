using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Vacancy
{
    public int VcancyId { get; set; }

    public int PositionId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ClosedDate { get; set; }

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    public virtual Position Position { get; set; } = null!;

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
