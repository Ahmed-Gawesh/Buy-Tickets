using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models
{
    public class Cinema:BaseEntity
    {
  

        public string Logo { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }

        //many Movies
    }
}
