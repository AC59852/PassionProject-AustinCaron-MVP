using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject_AustinCaron_MVP.Models
{
    public class Character
    {
        [Key]
        public int CharacterId { get; set; }

        public string CharacterName { get; set; }

        public string CharacterBio { get; set; }

        public int CharacterWeight { get; set; }

        public string CharacterHeight { get; set; }

        public string CharacterAffiliation { get; set; }

        public int CharacterAge { get; set; }

        public string CharacterIcon { get; set; }

        public string CharacterImage { get; set; }

        public string CharacterBck { get; set; }


        // A character can only be in one anime
        // An anime can have many characters
        [ForeignKey("Anime")]
        public int AnimeId { get; set; }
        public virtual Anime Anime { get; set; }
    }

    public class CharacterDto
    {
        public int CharacterId { get; set; }

        public string CharacterName { get; set; }

        public string CharacterBio { get; set; }

        public int CharacterWeight { get; set; }

        public string CharacterHeight { get; set; }

        public string CharacterAffiliation { get; set; }

        public int CharacterAge { get; set; }

        public string CharacterIcon { get; set; }

        public string CharacterImage { get; set; }

        public string CharacterBck { get; set; }

        public int AnimeId { get; set; }
        public string AnimeName { get; set; }

    }
}