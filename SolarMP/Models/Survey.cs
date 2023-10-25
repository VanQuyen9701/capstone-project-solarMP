﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SolarMP.Models
{
    public partial class Survey
    {
        [Key]
        [Column("surveyId")]
        [StringLength(16)]
        [Unicode(false)]
        public string SurveyId { get; set; }
        [Column("description")]
        [StringLength(255)]
        [Unicode(false)]
        public string Description { get; set; }
        [Column("note", TypeName = "text")]
        public string Note { get; set; }
        [Column("staffId")]
        [StringLength(16)]
        [Unicode(false)]
        public string StaffId { get; set; }
        [Column("status")]
        public bool Status { get; set; }

        [ForeignKey("StaffId")]
        [InverseProperty("Survey")]
        public virtual Account Staff { get; set; }
    }
}