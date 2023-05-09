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
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;

namespace MinesweepBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinesController : ControllerBase
    {
        private readonly MineContext _context;

        public MinesController(MineContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<int>> GetStartingPosition(int id)
        {
            if(_context.Mines == null)
            {
                return NotFound();
            }

            Mines? getMines = await _context.Mines.FindAsync(id);

            if(getMines == null)
            {
                return NotFound();
            }

            int answer = getMines.StartHeight * getMines.Width + getMines.StartWidth;
            Response.Headers.Append("Access-Control-Allow-Origin", "https://gregdiakonov.github.io/");
            Response.Headers.Append("Access-Control-Allow-Methods", "GET");
            Response.Headers.Append("Access-Control-Allow-Credentials", "true");

            return Ok(answer);
        }


        // GET: api/Boards/5
        [HttpGet("{id}/{pressed}/{pressMode}/{layout}")]
        public async Task<ActionResult<string>> GetBoard(int id, int pressed, int pressMode, string layout)
        {
            if (_context.Mines == null)
            {
                return NotFound();
            }
            Mines? getMines = await _context.Mines.FindAsync(id);

            if (getMines == null)
            {
                return NotFound();
            }

            int[][]? mines = JsonSerializer.Deserialize<int[][]>(getMines.Layout);
            int height = mines.Length;
            int width = mines[0].Length;

            int[]? unparsedMatrix = JsonSerializer.Deserialize<int[]>(layout);
            int[][] matrix = ParseMatrix.Parse(unparsedMatrix, height, width);

            int pressedH = pressed / width;
            int pressedW = pressed % width;

            if(pressMode == 0)
            {
                matrix = ClickEmulator.LeftClick(matrix, mines, pressedH, pressedW);
            }

            if(pressMode == 1)
            {
                matrix = ClickEmulator.RightClick(matrix, pressedH, pressedW);
            }

            unparsedMatrix = ParseMatrix.Unparse(matrix, height, width);
            layout = JsonSerializer.Serialize<int[]>(unparsedMatrix);

            Response.Headers.Append("Access-Control-Allow-Origin", "https://gregdiakonov.github.io/");
            Response.Headers.Append("Access-Control-Allow-Methods", "GET");
            Response.Headers.Append("Access-Control-Allow-Credentials", "true");

            return Ok(layout);
        }
    }
}
