using System;
using System.Collections.Generic;

namespace RestAPI.DAL;

public partial class TSemMst
{
    public int SemesterId { get; set; }

    public int SemesterNumber { get; set; }

    public bool IsOdd { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<TPaperSemMapping> TPaperSemMappings { get; set; } = new List<TPaperSemMapping>();
}
