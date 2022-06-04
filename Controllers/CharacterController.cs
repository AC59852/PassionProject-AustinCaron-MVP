using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PassionProject_AustinCaron_MVP.Models;
using PassionProject_AustinCaron_MVP.Models.ViewModels;

namespace PassionProject_AustinCaron_MVP.Controllers
{
    public class CharacterController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static CharacterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/api/");
        }


        // GET: Character
        public ActionResult List()
        {
            //Obj: Communicate with the CharacterData API to retrieve the list of characters
            // curl https://localhost:44381/api/characterdata/listcharacters

            string url = "characterdata/listcharacters";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<CharacterDto> Characters = response.Content.ReadAsAsync<IEnumerable<CharacterDto>>().Result;

            return View(Characters);
        }

        // GET: Character/Details/5
        public ActionResult Details(int id)
        {
            DetailsCharacter ViewModel = new DetailsCharacter();

            //curl https://localhost:44381/api/characterdata/findcharacter/{id}

            string url = "characterdata/findcharacter/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            CharacterDto SelectedCharacter = response.Content.ReadAsAsync<CharacterDto>().Result;

            ViewModel.SelectedCharacter = SelectedCharacter;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Character/New
        public ActionResult New()
        {
            string url = "animedata/listanime";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<AnimeDto> AnimeOptions = response.Content.ReadAsAsync<IEnumerable<AnimeDto>>().Result;

            return View(AnimeOptions);
        }

        // POST: Character/Create
        [HttpPost]
        public ActionResult Create(Character character)
        {
            //Obj: Add a new character into the database using the api

            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(animal.AnimalName);
            //objective: add a new animal into our system using the API
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44324/api/animaldata/addanimal 
            string url = "characterdata/addcharacter";


            string jsonpayload = jss.Serialize(character);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("List");

        }

        // GET: Character/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateCharacter ViewModel = new UpdateCharacter();

            string url = "characterdata/findcharacter/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            CharacterDto SelectedCharacter = response.Content.ReadAsAsync<CharacterDto>().Result;
            ViewModel.SelectedCharacter = SelectedCharacter;

            url = "animedata/listanime";
            response = client.GetAsync(url).Result;
            IEnumerable<AnimeDto> AnimeOptions = response.Content.ReadAsAsync<IEnumerable<AnimeDto>>().Result;

            ViewModel.AnimeOptions = AnimeOptions;

            return View(ViewModel);
        }

        // POST: Character/Update/5
        [HttpPost]
        public ActionResult Update(int id, Character character)
        {
            string url = "characterdata/updatecharacter/" + id;
            string jsonpayload = jss.Serialize(character);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(jsonpayload);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("/Details/" + character.CharacterId);
            } else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Character/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "characterdata/findcharacter/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            CharacterDto selectedcharacter = response.Content.ReadAsAsync<CharacterDto>().Result;

            return View(selectedcharacter);
        }

        // POST: Character/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "characterdata/deletecharacter/" + id;
            HttpContent content = new StringContent("");

            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List", "Anime");
            } else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
