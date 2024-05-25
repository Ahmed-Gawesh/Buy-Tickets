using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models
{
    public class Movies_Actors
    {
        [Key]
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        [Key]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}
