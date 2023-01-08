using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IKU_BloodBank.Models
{
    public class AppointmentsDbContext:DbContext
    {
        public AppointmentsDbContext() : base("DefaultConnection")
        {
        }
        public DbSet<Appointment> Appoinments { get; set; }
    }
}