using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace fxdb.Models
{
    [Route("api/[controller]")]
    public class FxController : Controller
    {
        private IEffectRepository EffectItems { get; set; }
    
        public FxController(IEffectRepository effects)
        {
            EffectItems = effects;
        }
        // GET: api/fx
        [HttpGet]
        public IEnumerable<EffectItem> Get()
        {
            var itemSet = EffectItems.GetAll();
            return itemSet.Select(e => e.StripPath());
        }

        // GET api/fx/<int id>
        [HttpGet("{id}")]
        public EffectItem Get(int id)
        {
            var item = EffectItems.Find(id);
            if (item != null) return item.StripPath();
            Response.StatusCode = 404;
            return null;
        }

        // POST api/fx
        [HttpPost]
        public void Post([FromBody]EffectItem value)
        {
            value.id = null;
            EffectItems.Add(value);
        }

        // DELETE api/fx/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            EffectItems.Remove(id);
        }
    }
}
