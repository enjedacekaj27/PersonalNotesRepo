using Entities.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class Note
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }
      //  public User User { get; set; }

      
    }
}
