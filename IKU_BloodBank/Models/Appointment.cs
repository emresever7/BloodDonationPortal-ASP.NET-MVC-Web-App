using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IKU_BloodBank.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BloodTeamId { get; set; }
        public DateTime AppointmentDate { get; set; }
        // Navigation properties
        public virtual User User { get; set; }
        public virtual BloodTeam BloodTeam { get; set; }
    }
}