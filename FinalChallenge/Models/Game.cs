using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalChallenge.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        public string  Venue { get; set; }

        
        public string AspNetUserId { get; set; }

        [ForeignKey("AspNetUserId")]
        public AspNetUser FeePayee { get; set; }

        public double FeeAmount { get; set; }
    }
}