using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fxdb.Models
{
    public class EntityFrameworkEffectRepository : IEffectRepository
    {
        private readonly FxContext _context;
        public EntityFrameworkEffectRepository(FxContext context)
        {
            _context = context;
        }
        public EffectItem Add(EffectItem item)
        {
            var entityEntry = _context.EffectItems.Add(item);
            _context.SaveChanges();
            return entityEntry.Entity;
        }

        public IEnumerable<EffectItem> GetAll()
        {
            return _context.EffectItems;
        }

        public EffectItem Find(int key)
        {
            return _context.EffectItems.First(e => e.Id == key);
        }

        public EffectItem Remove(int key)
        {
            var ent = _context.EffectItems.Remove(Find(key)).Entity;
            _context.SaveChanges();
            return ent;
        }

        public EffectItem Remove(int? key)
        {
            return !key.HasValue ? null : Remove(key.Value);
        }

        public void Update(EffectItem item)
        {
            _context.EffectItems.Update(item);
            _context.SaveChanges();
        }
    }
}
