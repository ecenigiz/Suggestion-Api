using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kaizen_0._1.Data.Entities
{
    public class Suggestion
    {
      
        
        public int Id { get; }
        [Key]
        public string Name { get; set; }
        public string SuggestionText { get; set; }


    }
}