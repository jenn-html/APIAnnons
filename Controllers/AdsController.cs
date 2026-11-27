using APIAnnons.Data;
using APIAnnons.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAnnons.Controllers
{
    [Route("[controller]")]    
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public AdsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ad>>> GetAll()
        {
            return Ok(await dbContext.Ads.ToListAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Ad>> GetOne(int id)
        {
            var ad = await dbContext.Ads.FirstOrDefaultAsync(a => a.Id == id);
            if (ad == null) return NotFound();
            return Ok(ad);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Ad>> PostAd(NewAd newAd)
        {
            var ad = new Ad
            {
                Title = newAd.Title,
                Description = newAd.Description,
                Price = newAd.Price,
                Category = newAd.Category
            };
            dbContext.Ads.Add(ad);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOne), new { id = ad.Id }, ad);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Ad>> UpdateAd(int id, UpdateAd updated)
        {
            var ad = await dbContext.Ads.FirstOrDefaultAsync(a => a.Id == id);
            if (ad == null) return NotFound();

            ad.Title = updated.Title;
            ad.Description = updated.Description;
            ad.Price = updated.Price;
            ad.Category = updated.Category;

            await dbContext.SaveChangesAsync();
            return Ok(ad);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var ad = await dbContext.Ads.FirstOrDefaultAsync(a => a.Id == id);
            if (ad == null) return NotFound();

            dbContext.Ads.Remove(ad);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
