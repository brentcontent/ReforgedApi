using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ReforgedApi;

namespace ReforgedApi.Controllers
{
    public class FactionController : ApiController
    {
        private ReforgedContext db = new ReforgedContext();

        // GET: api/Faction
        public IQueryable<Faction> GetFactions()
        {
            return db.Factions;
        }

        // GET: api/Faction/5
        [ResponseType(typeof(Faction))]
        public IHttpActionResult GetFaction(int id)
        {
            Faction faction = db.Factions.Find(id);
            if (faction == null)
            {
                return NotFound();
            }

            return Ok(faction);
        }

        // PUT: api/Faction/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFaction(int id, Faction faction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faction.id)
            {
                return BadRequest();
            }

            db.Entry(faction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Faction
        [ResponseType(typeof(Faction))]
        public IHttpActionResult PostFaction(Faction faction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Factions.Add(faction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = faction.id }, faction);
        }

        // DELETE: api/Faction/5
        [ResponseType(typeof(Faction))]
        public IHttpActionResult DeleteFaction(int id)
        {
            Faction faction = db.Factions.Find(id);
            if (faction == null)
            {
                return NotFound();
            }

            db.Factions.Remove(faction);
            db.SaveChanges();

            return Ok(faction);
        }

        public Faction GetFavoriteFactionOfUser(User user)
        {
            
            int id = (from u in db.Users
                    where u.id == user.id
                    join f in db.Factions on u.favoriteFactionId equals f.id
                    select f.id).First();

            Faction faction = db.Factions.Find(id);

            return faction;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FactionExists(int id)
        {
            return db.Factions.Count(e => e.id == id) > 0;
        }
    }
}