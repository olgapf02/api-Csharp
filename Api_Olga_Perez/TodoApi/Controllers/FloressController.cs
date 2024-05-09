using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloressController : ControllerBase
    {
        private readonly TodoContext _context;

        public FloressController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Floress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flores>>> Getfloress()
        {
            return await _context.floress.ToListAsync();
        }

        // GET: api/Floress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flores>> GetFlores(long id)
        {
            var flores = await _context.floress.FindAsync(id);

            if (flores == null)
            {
                return NotFound();
            }

            return flores;
        }

        // PUT: api/Floress/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlores(long id, Flores flores)
        {
            if (id != flores.Id)
            {
                return BadRequest();
            }

            _context.Entry(flores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FloresExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Floress
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flores>> PostFlores(Flores flores)
        {
            _context.floress.Add(flores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlores", new { id = flores.Id }, flores);
        }

        // DELETE: api/Floress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlores(long id)
        {
            var flores = await _context.floress.FindAsync(id);
            if (flores == null)
            {
                return NotFound();
            }

            _context.floress.Remove(flores);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FloresExists(long id)
        {
            return _context.floress.Any(e => e.Id == id);
        }
    }
}
