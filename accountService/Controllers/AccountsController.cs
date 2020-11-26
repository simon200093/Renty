using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using accountService.Data;
using accountService.Models;
using System.Net.Http;
using System.Text.Json;

namespace accountService.Controllers

{
    [Route("[controller]")]
    [ApiController]


    public class AccountsController : ControllerBase
    {
        private readonly accountServiceContext _context;

        public AccountsController(accountServiceContext context)
        {
            _context = context;

        }



        // GET: api/accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(long id)
        {
            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }
        // GET: accounts/find/username
        //login
        [HttpGet("find/{username}")]
        public async Task<IActionResult> GetAccountByUsername(string username)
        {
            var account = await _context.Account.Where(b => b.UserName == username).ToListAsync();
            Console.WriteLine(account.Count());
            if (account.Count() == 0)
            {
                return null;
            }
            return Ok(account[0].Password);
        }

        // PUT: api/accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(long id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }
            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
                return Ok(account);
        }

        // POST: api/accounts/register
        // check if the username is registered
        // allow post if username is valid
        [HttpPost("register")]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            var checker = await GetAccountByUsername(account.UserName);
            if(checker == null)
            {
                _context.Account.Add(account);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAccount", new { id = account.Id }, account);
            }
            else
            {
                return Ok("exist");
            }

        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Account>> DeleteAccount(long id)
        {

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Account.Remove(account);
            await _context.SaveChangesAsync();

            return account;
        }

        private bool AccountExists(long id)
        {
            return _context.Account.Any(e => e.Id == id);
        }
    }
}
