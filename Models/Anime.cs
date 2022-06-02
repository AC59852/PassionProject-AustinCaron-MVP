using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PassionProject_AustinCaron_MVP.Models
{
    public class Anime
    {
        [Key]
        public int AnimeId { get; set; }

        public string AnimeName { get; set; }

        public string AnimeAbbr { get; set; }

        public string AnimeDescription { get; set; }

        public string AnimeIcon { get; set; }

        public string AnimeBck { get; set; }
    }

    public class AnimeDto
    {
        public int AnimeId { get; set; }

        public string AnimeName { get; set; }

        public string AnimeAbbr { get; set; }

        public string AnimeDescription { get; set; }

        public string AnimeIcon { get; set; }

        public string AnimeBck { get; set; }
    }


}