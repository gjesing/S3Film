using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Entities
{
    public class Film
    {
        public int Id { get; set; }
        [Display (Name = "Titel")]
        public string Titel { get; set; }
        [Display (Name = "Land")]
        public string Land { get; set; }
        [Display (Name = "Udgivelsesår")]
        public int Year { get; set; }
        [Display (Name = "Genre")]
        public string Genre { get; set; }
        [Display (Name = "Antal Oscars")]
        public int Oscars { get; set; }
    }
}
