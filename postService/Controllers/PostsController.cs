using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using postService.Data;
using postService.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace postService.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly postServiceContext _context;
        static readonly HttpClient Client = new HttpClient();

        public PostsController(postServiceContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet("getByCate/{category}/{page}")]
        public async Task<ActionResult> GetPostByCate(string category,int page)
        {
            var dataStartRange = (page - 1) * 20;
            var dataEndRange = page * 20;
            var post = await _context.Post.OrderByDescending(o => o.PostTime).Where(b => b.PostType == category).Skip(dataStartRange).Take(dataEndRange).ToListAsync();

            if (post.Count() == 0)
            {
                return NotFound();
            }
            return Ok(post);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(long id)
        {
            var post = await _context.Post.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }


        // PUT: api/Posts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(long id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(post);
        }

        // POST: api/Posts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostPost(Post post)
        {

            var checker = await CheckWord(post.PostContent);
            var jo = JObject.Parse(checker);
            var id = jo["bad-words-total"].ToString();
            if (id == "0")
            {
                _context.Post.Add(post);
                await _context.SaveChangesAsync();
                //return CreatedAtAction("GetPost", new { id = post.Id }, post);
                return Ok(post);
            }
            else
            {
                return Ok("Bad word detected");
            }

        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(long id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        private bool PostExists(long id)
        {
            return _context.Post.Any(e => e.Id == id);
        }

        private async Task<String> CheckWord(string postContent)
        {;
            var req = new List<KeyValuePair<string, string>>();
            req.Add(new KeyValuePair<string, string>("user-id", "simon200093"));
            req.Add(new KeyValuePair<string, string>("api-key", "LgHlayWeTzQL6J3w1beS70NC0R0sbs8KUxTi03vmom9Df1z3"));
            req.Add(new KeyValuePair<string, string>("ip", "162.209.104.195"));
            req.Add(new KeyValuePair<string, string>("content", postContent));
            var content = new FormUrlEncodedContent(req);
            var response = await Client.PostAsync("https://neutrinoapi.net/bad-word-filter", content);
            var responseStr = await response.Content.ReadAsStringAsync();
            return responseStr;
        }
    }
}
