using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using Newtonsoft.Json;

namespace MovieProjectFrontEnd.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: CategoriesController
        public async Task<ActionResult> IndexAsync()
        {
            List<Categories> categories = new List<Categories>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44306/api/Categories");
                string strValue = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<Categories>>(strValue);

            }
            return View(categories);
           
        }

        // GET: CategoriesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            List<Movies> filmler = new List<Movies>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44306/api/Movies/GetMoviesByCategoryId?categoryId="+id.ToString());
                string strValue = await response.Content.ReadAsStringAsync();
                filmler = JsonConvert.DeserializeObject<List<Movies>>(strValue);

            }
            return View(filmler);
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Categories category)
        {
            try
            {
                category.Id = 0;
                using (var httpClient = new HttpClient())
                {
                    string jsonInString = JsonConvert.SerializeObject(category);
                    var response = await httpClient.PostAsync("https://localhost:44306/api/Categories",
                        new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                }



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriesController/Edit/5
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

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44306/api/Categories/" + id.ToString());
                string strValue = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<Categories>(strValue));

            }
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Categories category)
        {
            try
            {

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync("https://localhost:44306/api/Categories/" + id.ToString());
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
