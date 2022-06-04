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
    public class CharacterDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CharacterData/ListCharacters
        [HttpGet]
        public IEnumerable<CharacterDto> ListCharacters()
        {
            List<Character> Characters = db.Characters.ToList();
            List<CharacterDto> CharacterDtos = new List<CharacterDto>();

            Characters.ForEach(c => CharacterDtos.Add(new CharacterDto()
            {
                CharacterId = c.CharacterId,
                CharacterName = c.CharacterName,
                CharacterIcon = c.CharacterIcon,
                AnimeName = c.Anime.AnimeName
            }));

            return CharacterDtos;
        }

        [HttpGet]
        [ResponseType(typeof(CharacterDto))]
        [Route("api/characterdata/listcharactersforanime/{id}")]
        public IHttpActionResult ListCharactersForAnime(int id)
        {
            List<Character> Characters = db.Characters.Where(c => c.AnimeId == id).ToList();
            List<CharacterDto> CharacterDtos = new List<CharacterDto>();

            Characters.ForEach(c => CharacterDtos.Add(new CharacterDto()
            {
                CharacterId = c.CharacterId,
                CharacterName = c.CharacterName,
                CharacterIcon=c.CharacterIcon,
                AnimeId = c.Anime.AnimeId,
                AnimeName = c.Anime.AnimeName
            }));
            return Ok(CharacterDtos);
        }

        // GET: api/CharacterData/FindCharacter/5
        [ResponseType(typeof(Character))]
        [HttpGet]
        [Route("api/characterdata/findcharacter/{id}")]
        public IHttpActionResult FindCharacter(int id)
        {
            Character Character = db.Characters.Find(id);
            CharacterDto CharacterDto = new CharacterDto()
            {
                CharacterId = Character.CharacterId,
                CharacterName = Character.CharacterName,
                CharacterBio = Character.CharacterBio,
                CharacterAffiliation = Character.CharacterAffiliation,
                CharacterHeight = Character.CharacterHeight,
                CharacterWeight = Character.CharacterWeight,
                CharacterAge = Character.CharacterAge,
                CharacterBck = Character.CharacterBck,
                CharacterFontRatio = Character.CharacterFontRatio,
                CharacterImage = Character.CharacterImage,
                CharacterIcon = Character.CharacterIcon,
                AnimeId = Character.Anime.AnimeId,
                AnimeName = Character.Anime.AnimeName
            };

            if (Character == null)
            {
                return NotFound();
            }

            return Ok(CharacterDto);
        }

        // POST: api/CharacterData/UpdateCharacter/5
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/characterdata/updatecharacter/{id}")]
        public IHttpActionResult UpdateCharacter(int id, Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != character.CharacterId)
            {
                return BadRequest();
            }

            db.Entry(character).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
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

        // POST: api/CharacterData/AddCharacter
        [ResponseType(typeof(Character))]
        [HttpPost]
        [Route("api/characterdata/addcharacter")]
        public IHttpActionResult AddCharacter(Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Characters.Add(character);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = character.CharacterId }, character);
        }

        // POST: api/CharacterData/DeleteCharacter/5
        [ResponseType(typeof(Character))]
        [HttpPost]
        [Route("api/characterdata/deletecharacter/{id}")]
        public IHttpActionResult DeleteCharacter(int id)
        {
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return NotFound();
            }

            db.Characters.Remove(character);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CharacterExists(int id)
        {
            return db.Characters.Count(e => e.CharacterId == id) > 0;
        }
    }
}