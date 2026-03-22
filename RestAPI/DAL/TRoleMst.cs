using System;
using System.Collections.Generic;

namespace RestAPI.DAL;

public partial class TRoleMst
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<TUserRoleMapping> TUserRoleMappings { get; set; } = new List<TUserRoleMapping>();
}
