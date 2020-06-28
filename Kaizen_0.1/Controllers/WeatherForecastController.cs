using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen_0._1.Data;
using Kaizen_0._1.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kaizen_0._1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private KaizenContext _kaizenContext;

        public WeatherForecastController(KaizenContext context) {

            _kaizenContext = context;
        }

        [HttpGet]
        [Route("get-suggestions")]
        public ActionResult<IEnumerable<Suggestion>> GetSuggetions()
        {
            return _kaizenContext.Suggestions.ToList();
        }



        [HttpGet]
        [Route("get-suggestion-by-id/{id}")]
        public ActionResult<Suggestion> GetSuggetionById(int id)
        {
            Suggestion sg = _kaizenContext.Suggestions.FirstOrDefault(s => s.Id == id);
            if (sg == null)
                return NotFound("Bu konu ile ilgili yeni bir öneri yapabilirsiniz");
                 
            return Ok(sg);
        }
        



        [HttpPost]
        [Route("post-suggestion/{suggestion}")] //??
        public async Task<ActionResult> Post([FromBody]Suggestion suggestion)
        {
            if (suggestion == null)
            {
                return NotFound("Öneri içeriği boş ");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _kaizenContext.Suggestions.AddAsync(suggestion);
            await _kaizenContext.SaveChangesAsync();
            return Ok(suggestion);
        }

        [HttpPut]
        [Route("update-suggestion")] //??
        public async Task<ActionResult> Update([FromBody]Suggestion suggestion)
        {
            if (suggestion == null)
            {
                return NotFound("Stduent data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Suggestion existinSuggestion = _kaizenContext.Suggestions.FirstOrDefault(s => s.Id == suggestion.Id);
            if (existinSuggestion == null)
            {
                return NotFound("Böyle bir öneri bulunmamaktadır.");
            }
            existinSuggestion.Name = suggestion.Name;
            existinSuggestion.SuggestionText = suggestion.SuggestionText;
            _kaizenContext.Attach(existinSuggestion).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _kaizenContext.SaveChangesAsync();
            return Ok(existinSuggestion);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("Id is not supplied");
            }
            Suggestion suggest = _kaizenContext.Suggestions.FirstOrDefault(s => s.Id == id);
            if (suggest == null)
            {
                return NotFound("Bu id ye ait öneri bulunmamaktadır.");
            }
            _kaizenContext.Suggestions.Remove(suggest);
            await _kaizenContext.SaveChangesAsync();
            return Ok("Öneri silindi.");
        }

        ~WeatherForecastController()
        {
            _kaizenContext.Dispose();
        }
    }
}