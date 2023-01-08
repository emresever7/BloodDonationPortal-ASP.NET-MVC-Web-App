using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IKU_BloodBank.Models
{
    public class User:ApplicationUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int AppointmentId { get; set; }
        // Navigation properties
        public virtual Appointment Appointment { get; set; }
    }
}