﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SolarMP.Models
{
    public partial class Request
    {
        [Key]
        [Column("requestId")]
        [StringLength(16)]
        [Unicode(false)]
        public string RequestId { get; set; }
        [Required]
        [Column("packageId")]
        [StringLength(16)]
        [Unicode(false)]
        public string PackageId { get; set; }
        [Required]
        [Column("accountId")]
        [StringLength(16)]
        [Unicode(false)]
        public string AccountId { get; set; }
        [Column("createAt", TypeName = "datetime")]
        public DateTime CreateAt { get; set; }
        [Column("status")]
        public bool Status { get; set; }
        [Required]
        [Column("description")]
        public string Description { get; set; }
        [Column("staffId")]
        [StringLength(16)]
        [Unicode(false)]
        public string StaffId { get; set; }

        [ForeignKey("AccountId")]
        [InverseProperty("Request")]
        public virtual Account Account { get; set; }
        [ForeignKey("PackageId")]
        [InverseProperty("Request")]
        public virtual Package Package { get; set; }
    }
}