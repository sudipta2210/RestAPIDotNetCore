using System;
using System.Collections.Generic;

namespace RestAPI.DAL;

public partial class TPaperMst
{
    public int PaperId { get; set; }

    public string PaperName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<TPaperSemMapping> TPaperSemMappings { get; set; } = new List<TPaperSemMapping>();
}
