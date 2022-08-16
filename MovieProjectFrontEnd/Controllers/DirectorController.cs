using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using Newtonsoft.Json;

namespace MovieProjectFrontEnd.Controllers
{
    public class DirectorController : Controller
    {
        // GET: Director
        public async Task<ActionResult> IndexAsync()
        {
            List<Directors> directors = new List<Directors>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44306/api/Directors");
                string strValue = await response.Content.ReadAsStringAsync();
                directors = JsonConvert.DeserializeObject<List<Directors>>(strValue);

            }
            return View(directors);
        }

        // GET: Director/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44306/api/Directors/" + id.ToString());
                string strValue = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<Directors>(strValue));

            }
            
        }

        // GET: Director/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Director/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Directors director)
        {
            try
            {
                director.Id = 0;
                using (var httpClient = new HttpClient())
                {
                    string jsonInString = JsonConvert.SerializeObject(director);
                    var response = await httpClient.PostAsync("https://localhost:44306/api/Directors",
                        new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                }
                

                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Director/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Director/Edit/5
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

        // GET: Director/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:44306/api/Directors/"+id.ToString());
                string strValue = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<Directors>(strValue)) ;

            }
          
        }

        // POST: Director/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Directors director)
        {
            try
            {
                
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync("https://localhost:44306/api/Directors/" + id.ToString());
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
