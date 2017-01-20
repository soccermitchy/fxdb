using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fxdb.Models
{
    public interface IEffectRepository
    {
        void Add(EffectItem item);
        IEnumerable<EffectItem> GetAll();
        EffectItem Find(int key);
        EffectItem Remove(int key);
        void Update(EffectItem item);
    }
}
