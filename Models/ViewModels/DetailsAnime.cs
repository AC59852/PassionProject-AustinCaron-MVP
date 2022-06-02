using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_AustinCaron_MVP.Models.ViewModels
{
    public class DetailsAnime
    {
        public AnimeDto SelectedAnime { get; set; }

        public IEnumerable<CharacterDto> RelatedCharacters { get; set; }
    }
}