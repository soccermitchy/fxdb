using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fxdb.Models
{
    public class MockEffectRepository : IEffectRepository
    {
        private static readonly ConcurrentDictionary<int, EffectItem> _effects = new ConcurrentDictionary<int, EffectItem>();

        public MockEffectRepository()
        {
            Add(new EffectItem
                {
                    id = 0,
                    name = "Test Effect",
                    path = "storage/1"
                });
            Add(new EffectItem
            {
                id = 1,
                name = "Test Effect 2",
                path = "/test2.mp3"
            });
        }

        public IEnumerable<EffectItem> GetAll()
        {
            return _effects.Values;
        }

        public EffectItem Add(EffectItem item)
        {
            var key = _effects.Count;
            while (true) // keep trying different key IDs until we get one we can use... in an ideal situation (*cough ef cough*) this will be done for us.
            {
                if (!_effects.ContainsKey(key))
                {
                    item = new EffectItem
                    {
                        id = key,
                        name = item.name,
                        path = item.path
                    };
                    _effects.TryAdd(key, item);
                    return item;
                }
                key++;
            }
        }

        public EffectItem Find(int key)
        {
            EffectItem item;
            _effects.TryGetValue(key, out item);
            return item;
        }

        public EffectItem Remove(int key)
        {
            EffectItem item;
            _effects.TryRemove(key, out item);
            return item;
        }

        public void Update(EffectItem item)
        {
            if (item.id != null) _effects[item.id.Value] = item;
        }
    }
}
