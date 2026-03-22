using System;
using System.Collections.Generic;

namespace RestAPI.DAL;

public partial class TPaperSemMapping
{
    public int Tpsmid { get; set; }

    public int PaperId { get; set; }

    public int SemId { get; set; }

    public int DeptId { get; set; }

    public bool IsActive { get; set; }

    public virtual TDeptMst Dept { get; set; } = null!;

    public virtual TPaperMst Paper { get; set; } = null!;

    public virtual TSemMst Sem { get; set; } = null!;

    public virtual ICollection<TTeacherPaperMapping> TTeacherPaperMappings { get; set; } = new List<TTeacherPaperMapping>();

    public virtual ICollection<TTimetable> TTimetables { get; set; } = new List<TTimetable>();
}
