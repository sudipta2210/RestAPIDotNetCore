using System;
using System.Collections.Generic;

namespace RestAPI.DAL;

public partial class TTeacherPaperMapping
{
    public int Tpmid { get; set; }

    public int TeacherId { get; set; }

    public int Tpsmid { get; set; }

    public bool IsActive { get; set; }

    public virtual TTeacherMst Teacher { get; set; } = null!;

    public virtual TPaperSemMapping Tpsm { get; set; } = null!;
}
