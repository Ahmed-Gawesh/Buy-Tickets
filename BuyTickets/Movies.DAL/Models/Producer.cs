using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models
{
    public class Producer:BaseEntity
    {
        public string FullName { get; set; }
        public string ProfilePictureURL { get; set; }
        public string Bio { get; set; }
    }
}
