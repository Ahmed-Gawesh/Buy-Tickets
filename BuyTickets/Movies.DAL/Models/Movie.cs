using Movies.DAL.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models
{
    public class Movie:BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public string ImageURL { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //RelationShips
        public List<Movies_Actors> Movies_Actors { get; set; }

        //Cinema
        [ForeignKey("CinemaId")]
        public int CinemaId { get; set; } //ForigenKey
        public Cinema Cinema { get; set; }

        //Producer
        [ForeignKey("ProducerId")]
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }



    }
}
