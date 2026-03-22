using System;
using System.Collections.Generic;

namespace RestAPI.DAL;

public partial class TDeptMst
{
    public int DeptId { get; set; }

    public string DeptName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<TPaperSemMapping> TPaperSemMappings { get; set; } = new List<TPaperSemMapping>();

    public virtual ICollection<TTeacherMst> TTeacherMsts { get; set; } = new List<TTeacherMst>();
}
