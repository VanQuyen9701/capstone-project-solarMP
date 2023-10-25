﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SolarMP.Models
{
    [Index("RoleName", Name = "IX_Role", IsUnique = true)]
    public partial class Role
    {
        public Role()
        {
            Account = new HashSet<Account>();
        }

        [Key]
        [Column("roleId")]
        [StringLength(10)]
        [Unicode(false)]
        public string RoleId { get; set; }
        [Column("roleName")]
        [StringLength(50)]
        public string RoleName { get; set; }
        [Column("status")]
        public bool? Status { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<Account> Account { get; set; }
    }
}