using System;
using System.Collections.Generic;

namespace RestAPI.DAL;

public partial class TUserRoleMapping
{
    public int UserRoleId { get; set; }

    public int LoginId { get; set; }

    public int RoleId { get; set; }

    public DateTime AssignedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual TLoginMaster Login { get; set; } = null!;

    public virtual TRoleMst Role { get; set; } = null!;
}
