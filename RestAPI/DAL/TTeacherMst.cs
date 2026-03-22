using System;
using System.Collections.Generic;

namespace RestAPI.DAL;

public partial class TTeacherMst
{
    public int TeacherId { get; set; }

    public string? TeacherName { get; set; }

    public bool IsCommon { get; set; }

    public int? DeptId { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual TDeptMst? Dept { get; set; }

    public virtual ICollection<TTeacherLeave> TTeacherLeaves { get; set; } = new List<TTeacherLeave>();

    public virtual ICollection<TTeacherPaperMapping> TTeacherPaperMappings { get; set; } = new List<TTeacherPaperMapping>();

    public virtual ICollection<TTimetable> TTimetables { get; set; } = new List<TTimetable>();
}
