using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using offerService.Data;
using offerService.Models;

namespace offerService.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly offerServiceContext _context;

        public OffersController(offerServiceContext context)
        {
            _context = context;
        }

        // GET: api/Offers
        [HttpGet]
        public async Task<ActionResult> GetOffer()
        {
            var offer = await _context.Offer.ToListAsync();

            if (offer.Count() == 0)
            {
                return NotFound();
            }
            return Ok(offer);
        }

        // GET: api/Offers
        [HttpGet("getByAccount/{accountId}")]
        public async Task<ActionResult> GetOfferByAccount(long accountId)
        {
            var offer = await _context.Offer.Where(b => b.AccountId == accountId).ToListAsync();

            if (offer.Count() == 0)
            {
                return NotFound();
            }
            return Ok(offer);
        }

        // GET: api/Offers/5
        [HttpGet("getByPost/{postId}")]
        public async Task<ActionResult> GetOfferByPost(long postId)
        {
            var offer = await _context.Offer.Where(b => b.PostId == postId).ToListAsync();

            if (offer.Count() == 0)
            {
                return NotFound();
            }
            return Ok(offer);
        }

        // PUT: api/Offers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOffer(long id, Offer offer)
        {
            if (id != offer.Id)
            {
                return BadRequest();
            }

            _context.Entry(offer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(offer);
        }

        // POST: api/Offers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Offer>> PostOffer(Offer offer)
        {
            _context.Offer.Add(offer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffer", new { id = offer.Id }, offer);
        }

        // DELETE: api/Offers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Offer>> DeleteOffer(long id)
        {
            var offer = await _context.Offer.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }

            _context.Offer.Remove(offer);
            await _context.SaveChangesAsync();

            return offer;
        }

        private bool OfferExists(long id)
        {
            return _context.Offer.Any(e => e.Id == id);
        }
    }
}
