using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using Newtonsoft.Json;

namespace MovieProjectFrontEnd.Controllers
{
    public class MovieController : Controller
    {
        // GET: MovieController
        public async Task<ActionResult> IndexAsync()
        {
            List<Movies> movies = new List<Movies>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44306/api/Movies/GetMovies");
                string strValue = await response.Content.ReadAsStringAsync();
                movies = JsonConvert.DeserializeObject<List<Movies>>(strValue);

            }
            return View(movies);
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieController/Create
        public async Task<ActionResult> CreateAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44306/api/Directors");
                string strValue = await response.Content.ReadAsStringAsync();
                ViewBag.directors = JsonConvert.DeserializeObject<List<Directors>>(strValue);

                var response2 = await httpClient.GetAsync("https://localhost:44306/api/Categories");
                string strValue2 = await response2.Content.ReadAsStringAsync();
                ViewBag.categories = JsonConvert.DeserializeObject<List<Categories>>(strValue2);


            }
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Movies movie)
        {
   
            try
            {
                movie.Id = 0;
                using (var httpClient = new HttpClient())
                {
                    string jsonInString = JsonConvert.SerializeObject(movie);
                    var response = await httpClient.PostAsync("https://localhost:44306/api/Movies/PostMovies",
                        new StringContent(jsonInString, Encoding.UTF8, "application/json"));



                }



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44306/api/Movies/DeleteMovies/" + id.ToString());
                string strValue = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<Categories>(strValue));



            }
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Movies movie)
        {
            try
            {

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync("https://localhost:44306/api/Movies/DeleteMovies/" + id.ToString());
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
