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
using PassionProject_AustinCaron_MVP.Models;

namespace PassionProject_AustinCaron_MVP.Controllers
{
    public class AnimeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AnimeData/ListAnime
        [HttpGet]
        [Route("api/AnimeData/ListAnime")]
        [ResponseType(typeof(AnimeDto))]
        public IHttpActionResult ListAnime()
        {
            List<Anime> Animes = db.Animes.ToList();
            List<AnimeDto> AnimesDtos = new List<AnimeDto>();

            Animes.ForEach(a => AnimesDtos.Add(new AnimeDto()
            {
                AnimeId = a.AnimeId,
                AnimeName = a.AnimeName,
                AnimeAbbr = a.AnimeAbbr,
                AnimeDescription = a.AnimeDescription,
                AnimeIcon = a.AnimeIcon,
            }));

            return Ok(AnimesDtos);
        }

        // GET: api/AnimeData/5
        [HttpGet]
        [ResponseType(typeof(AnimeDto))]
        [Route("api/animedata/findanime/{id}")]
        public IHttpActionResult FindAnime(int id)
        {
            Anime Anime = db.Animes.Find(id);
            AnimeDto AnimeDto = new AnimeDto()
            {
                AnimeId = Anime.AnimeId,
                AnimeName = Anime.AnimeName,
                AnimeDescription = Anime.AnimeDescription,
                AnimeAbbr = Anime.AnimeAbbr,
                AnimeBck = Anime.AnimeBck,
            };

            if(Anime == null)
            {
                return NotFound();
            }

            return Ok(AnimeDto);
        }

        // PUT: api/AnimeData/5
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/animedata/updateanime/{id}")]
        public IHttpActionResult UpdateAnime(int id, Anime anime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != anime.AnimeId)
            {
                return BadRequest();
            }

            db.Entry(anime).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimeExists(id))
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

        // POST: api/AnimeData
        [ResponseType(typeof(Anime))]
        [Route("api/animedata/addanime")]
        public IHttpActionResult PostAnime(Anime anime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Animes.Add(anime);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = anime.AnimeId }, anime);
        }

        // DELETE: api/AnimeData/5
        [ResponseType(typeof(Anime))]
        [HttpPost]
        [Route("api/animedata/deleteanime/{id}")]
        public IHttpActionResult DeleteAnime(int id)
        {
            Anime anime = db.Animes.Find(id);
            if (anime == null)
            {
                return NotFound();
            }

            db.Animes.Remove(anime);
            db.SaveChanges();

            return Ok(anime);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnimeExists(int id)
        {
            return db.Animes.Count(e => e.AnimeId == id) > 0;
        }
    }
}