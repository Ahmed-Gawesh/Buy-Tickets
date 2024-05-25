using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models
{
    public class Actor:BaseEntity
    {
        public string FullName { get; set; }
        public string ProfilePictureURL { get; set; }
        public string Bio { get; set; }

        public List<Movies_Actors> Movies_Actors { get; set; }

    }
}
