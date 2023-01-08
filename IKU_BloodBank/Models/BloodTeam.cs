using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IKU_BloodBank.Models
{
    public class BloodTeam
    {
        [Key]
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
        public string CityIdArea { get; set; }
        public string Town { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Neighbourhood { get; set; }
        public bool Platelets { get; set; }
        public string PhoneNumber { get; set; }
    }
}