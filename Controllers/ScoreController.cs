using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.DynamicData;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.ApplicationInsights.Web;
using ReforgedApi;

namespace ReforgedApi.Controllers
{
    public class ScoreController : ApiController
    {
        private ReforgedContext db = new ReforgedContext();

        // GET: api/Score
        public IQueryable<Score> GetScores()
        {
            return db.Scores;
        }

        // GET: api/Score/5
        [ResponseType(typeof(Score))]
        public IHttpActionResult GetScore(int id)
        {
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return NotFound();
            }

            return Ok(score);
        }

        // PUT: api/Score/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutScore(int id, Score score)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != score.userId)
            {
                return BadRequest();
            }

            db.Entry(score).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(id))
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

        // POST: api/Score
        [ResponseType(typeof(Score))]
        public IHttpActionResult PostScore(Score score)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Scores.Add(score);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ScoreExists(score.userId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = score.userId }, score);
        }

        // DELETE: api/Score/5
        [ResponseType(typeof(Score))]
        public IHttpActionResult DeleteScore(int id)
        {
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return NotFound();
            }

            db.Scores.Remove(score);
            db.SaveChanges();

            return Ok(score);
        }

        public Score GetScoreByUserAndGameMode(User user, GameMode gameMode)
        {
            Score score = (from s in db.Scores
                        where s.userId == user.id
                        where s.gameModeId == gameMode.id
                        select s).First();

            return score;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScoreExists(int id)
        {
            return db.Scores.Count(e => e.userId == id) > 0;
        }
    }
}