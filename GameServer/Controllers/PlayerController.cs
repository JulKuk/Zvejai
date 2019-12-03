using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameServer.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerContext _context;
        public int Qty { get; set; } = 0;

        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View("this in index");
        //}

        public PlayerController(PlayerContext context)
        {
            _context = context;

            //if (_context.Players.Count() == 0)
            //{
            //    // Create a new Player if collection is empty,
            //    // which means you can't delete all Players.
            //    for (int i = 0; i < 4; i++)
            //    {
            //        Qty++;
            //        Player p = new Player { Name = "Player-" + Qty, health_points = 100, PosX = 0, PosY = 0 };
            //        _context.Players.Add(p);
            //    }

            //    _context.SaveChanges();
            //}
        }


        // GET api/player
        [HttpGet]
        public ActionResult<ICollection<Player>> GetAll()
        {
            return _context.Players.Include(player => player.Weapon)
                                   .Include(player => player.Weapons).ToList();
        }

        // GET api/player/5
        [HttpGet("{id}", Name = "GetPlayer")]
        public ActionResult<Player> GetById(long id)
        {
            List<Player> temp = _context.Players.Include(player => player.Weapon).Include(player => player.Weapons).ToList();
            Player p = temp.Find(plaeyr => plaeyr.id == id);
            if (p == null) {
                return NotFound("player not found");
            }
            return p;
        }

        // POST api/player
        [HttpPost]
        //public string Create(Player player)
        public ActionResult<Player> Create(Player player)
        {
            _context.Players.Add(player);
            _context.Weapons.Add(player.Weapon);
            _context.SaveChanges();

            //return Ok(); //"created - ok"; 
            return player;
        }


        [HttpPut("{id}")]
        public IActionResult Update(long id, Player p)
        {
            List<Player> temp = _context.Players.Include(player => player.Weapon).Include(player => player.Weapons).ToList();
            var pp = temp.Find(plaeyr => plaeyr.id == id);
            if (pp == null)
            {
                return NotFound();
            }

            pp.health_points = p.health_points;
            pp.Name = p.Name;
            pp.points = p.points;
            pp.PosX = p.PosX;
            pp.PosY = p.PosY;
            pp.speed = p.speed;

            _context.Players.Update(pp);
            _context.SaveChanges();

            return Ok(); //NoContent();
        }

        [HttpPatch]
        public IActionResult PartialUpdate(Player player)
        {
            var play1 = _context.Players.Find(player.id);
            if (play1 == null)
            {
                return NotFound();
            }
            else
            {
                play1.PosX = player.PosX;
                play1.PosY = player.PosY;

                _context.Players.Update(play1);
                _context.SaveChanges();
            }
            return Ok();
            //return CreatedAtRoute("GetPlayer", new { id = player.Id }, player);
        }

        // DELETE api/values/5
        /*[HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Players.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Players.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }

    }
}




