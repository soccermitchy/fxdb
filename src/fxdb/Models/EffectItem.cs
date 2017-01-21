using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fxdb.Models
{
    public class EffectItem
    {
        public int? id;
        public string name;
        public string path;

        public EffectItem StripPath()
        {
            return new EffectItem()
            {
                id = id,
                name = name
            };
        }
    }
}
