using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace IKU_BloodBank.Models
{
    public class BloodTeamDbContext : DbContext
    {
        public BloodTeamDbContext() : base("DefaultConnection")
        {
            
        }
        public DbSet<BloodTeam> BloodTeams { get; set; }

    }


}

