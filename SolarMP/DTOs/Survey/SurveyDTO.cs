using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SolarMP.DTOs.Survey
{
    public class SurveyDTO
    {
        public string SurveyId { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string StaffId { get; set; }
        public bool Status { get; set; }
    }
}
