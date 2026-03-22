using System;
using System.Collections.Generic;

namespace RestAPI.DAL;

public partial class TTeacherLeave
{
    public int LeaveId { get; set; }

    public int TeacherId { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public bool IsActive { get; set; }

    public virtual TTeacherMst Teacher { get; set; } = null!;
}
