using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
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
            //return itemSet.Select(e => e.StripPath());
            return itemSet;
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
        public async Task<EffectItem> Post(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException("File is null");
            var title = "test";
            var item = new EffectItem() {name = title};
            item = EffectItems.Add(item); // this is done so we can get an item ID
            item.path = "storage/" + item.id;
            EffectItems.Update(item);
            using (var fileStream = new FileStream(item.path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return item.StripPath();
        }

        // DELETE api/fx/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            EffectItems.Remove(id);
        }
    }
}
