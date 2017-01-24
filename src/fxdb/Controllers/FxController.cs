using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using libaudiomagic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace fxdb.Models
{
    [Route("api/[controller]")]
    public class FxController : Controller
    {
        private IEffectRepository _effectItems { get; set; }
        private readonly ILogger _logger;
        public FxController(IEffectRepository effects, ILogger<FxController> logger)
        {
            _effectItems = effects;
            _logger = logger;
        }
        // GET: api/fx
        [HttpGet]
        public IEnumerable<EffectItem> Get()
        {
            var itemSet = _effectItems.GetAll();
            //return itemSet.Select(e => e.StripPath());
            return itemSet;
        }

        // GET api/fx/<int id>
        [HttpGet("info/{id:regex(\\d+)}")]
        public EffectItem GetInfo(int id)
        {
            var item = _effectItems.Find(id);
            if (item != null) return item.StripPath();
            Response.StatusCode = 404;
            return null;
        }

        [HttpGet("play/{id:regex(\\d+)}")]
        public IActionResult GetFile(int id) {
            var item = _effectItems.Find(id);
            if (item == null) {
                Response.StatusCode = 404;
                return NotFound();
            }
            string mimeType;
            try
            {
                mimeType = Magic.DetermineMimeType(item.path);
            }
            catch
            {
                Response.StatusCode = 404;
                return NotFound();
            }

            return File(new FileStream(item.path, FileMode.Open), mimeType);
        }
        // POST api/fx
        [HttpPost]
        public async Task<EffectItem> Post(string title, IFormFile file)
        {
            if (file == null) throw new ArgumentNullException("File is null");
            var item = new EffectItem() {name = title};
            item = _effectItems.Add(item); // this is done so we can get an item ID
            item.path = "storage/" + item.id;
            _effectItems.Update(item);
            using (var fileStream = new FileStream(item.path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            var m = Magic.DetermineMimeType("storage/" + item.id);
            _logger.LogInformation("magic result: " + m);
            if (m != null) return item.StripPath();

            Response.StatusCode = 415; // Unsupported media type
            _effectItems.Remove(item.id);
            System.IO.File.Delete("storage/" + item.id);
            return null;
        }

        // DELETE api/fx/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _effectItems.Remove(id);
        }
    }
}
