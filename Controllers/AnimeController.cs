using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassionProject_AustinCaron_MVP.Models;
using PassionProject_AustinCaron_MVP.Models.ViewModels;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Diagnostics;

namespace PassionProject_AustinCaron_MVP.Controllers
{
    public class AnimeController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AnimeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/api/");
        }
        // GET: Anime/List
        public ActionResult List()
        {
            string url = "animedata/listanime";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<AnimeDto> Animes = response.Content.ReadAsAsync<IEnumerable<AnimeDto>>().Result;

            return View(Animes);
        }

        // GET: Anime/Details/5
        public ActionResult Details(int id)
        {
            //Obj: Communicate with the CharacterData API to retrieve the list of characters
            // curl https://localhost:44381/api/characterdata/findcharacter/{id}

            DetailsAnime ViewModel = new DetailsAnime();

            string url = "animedata/findanime/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AnimeDto SelectedAnime = response.Content.ReadAsAsync<AnimeDto>().Result;

            ViewModel.SelectedAnime = SelectedAnime;

            url = "characterdata/listcharactersforanime/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<CharacterDto> RelatedCharacters = response.Content.ReadAsAsync<IEnumerable<CharacterDto>>().Result;

            ViewModel.RelatedCharacters = RelatedCharacters;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Anime/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Anime/Create
        [HttpPost]
        public ActionResult Create(Anime Anime)
        {
            string url = "animedata/addanime";

            string jsonpayload = jss.Serialize(Anime);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            } else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Anime/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "animedata/findanime/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AnimeDto selectedanime = response.Content.ReadAsAsync<AnimeDto>().Result;

            return View(selectedanime);
        }

        // POST: Anime/Update/5
        [HttpPost]
        public ActionResult Update(int id, Anime anime)
        {
            string url = "animedata/updateanime/" + id;
            string jsonpayload = jss.Serialize(anime);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(jsonpayload);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("/Details/" + anime.AnimeId);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Anime/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "animedata/findanime" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AnimeDto selectedanime = response.Content.ReadAsAsync<AnimeDto>().Result;

            return View(selectedanime);
        }

        // POST: Anime/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "animedata/deleteanime/" + id;
            HttpContent content = new StringContent("");

            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsJsonAsync(url, content).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            } else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
