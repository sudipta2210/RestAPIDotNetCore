using System;
using System.Collections.Generic;

namespace RestAPI.DAL;

public partial class TTimetable
{
    public int TimetableId { get; set; }

    public int WeekNumber { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public int Slot { get; set; }

    public int TeacherId { get; set; }

    public int TpsmId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }

    public bool IsNotified { get; set; }

    public bool Isactive { get; set; }

    public virtual TTeacherMst Teacher { get; set; } = null!;

    public virtual TPaperSemMapping Tpsm { get; set; } = null!;
}
