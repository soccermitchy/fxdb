using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace fxdb.Models
{
    public class EffectItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string name { get; set; }
        public string path { get; set; }

        public EffectItem StripPath()
        {
            return new EffectItem()
            {
                Id = Id,
                name = name
            };
        }
    }
}
