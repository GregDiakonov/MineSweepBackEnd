using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinesweepBackEnd.Models;
using MinesweepBackEnd.Utility;
using System.Text.Json;

namespace MinesweepBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly MinesweeperContext _context;

        public BoardsController(MinesweeperContext context)
        {
            _context = context;
        }

        // GET: api/Boards/5
        [HttpGet("{id}/{pressed}/{pressMode}/{layout}")]
        public async Task<ActionResult<Board>> GetBoard(long id, int pressed, int pressMode, string layout)
        {
            int[]? data = JsonSerializer.Deserialize<int[]>(layout);

            if (_context.Boards == null)
            {
                return NotFound();
            }
            var board = await _context.Boards.FindAsync(id);
            

            return board;
        }

        // PUT: api/Boards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoard(long id, Board board)
        {
            if (id != board.Id)
            {
                return BadRequest();
            }

            _context.Entry(board).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardExists(id))
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

        // POST: api/Boards
        [HttpPost("{id}/{pressed}/{height}/{width}/{pressMode}/{layout}")]
        public async Task<ActionResult<Board>> PostBoard(long id, int pressed, int width, int height, int pressMode, string layout)
        {
            int pressedH = pressed / width;
            int pressedW = pressed % width;

            if(_context.Boards == null)
            {
                return NotFound();
            }

            var board = await _context.Boards.FindAsync(id);
            int[][] visible_layout = JsonSerializer.Deserialize<int[][]>(layout); 
            int[][] mines;

            int outcome = 0;

            if(board == null)
            {
                mines = MakeMines.makeMines(pressedH, pressedW, width, height);
            } 
            else
            {
                mines = JsonSerializer.Deserialize<int[][]>(board.Mines);
            }

            if(pressed == 0)
            {
                visible_layout = ClickEmulator.leftClick(visible_layout, mines, pressedH, pressedW);
                if(CheckOutcome.checkLoss(visible_layout))
                {
                    visible_layout = CheckOutcome.highlightMines(visible_layout, mines);
                    outcome = -1;
                } 
                else
                {
                    if(CheckOutcome.checkWin(visible_layout, mines))
                    {
                        outcome = 1;
                    }
                }
            }

            if(pressed == 1)
            {
                visible_layout = ClickEmulator.rightClick(visible_layout, pressedH, pressedW);
            }

            string response = JsonSerializer.Serialize(visible_layout);

            return Content(response);
        }

        // DELETE: api/Boards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(long id)
        {
            if (_context.Boards == null)
            {
                return NotFound();
            }
            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoardExists(long id)
        {
            return (_context.Boards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
