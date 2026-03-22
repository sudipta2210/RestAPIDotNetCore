using System;
using System.Collections.Generic;

namespace RestAPI.DAL;

public partial class TLoginMaster
{
    public int LoginId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual TLoginMaster? CreatedByNavigation { get; set; }

    public virtual ICollection<TLoginMaster> InverseCreatedByNavigation { get; set; } = new List<TLoginMaster>();

    public virtual ICollection<TLoginMaster> InverseModifiedByNavigation { get; set; } = new List<TLoginMaster>();

    public virtual TLoginMaster? ModifiedByNavigation { get; set; }

    public virtual ICollection<TUserRoleMapping> TUserRoleMappings { get; set; } = new List<TUserRoleMapping>();
}
