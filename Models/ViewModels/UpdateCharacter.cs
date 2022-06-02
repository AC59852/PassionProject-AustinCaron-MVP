using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_AustinCaron_MVP.Models.ViewModels
{
    public class UpdateCharacter
    {
        public CharacterDto SelectedCharacter { get; set; }

        public IEnumerable<AnimeDto> AnimeOptions { get; set; }
    }
}