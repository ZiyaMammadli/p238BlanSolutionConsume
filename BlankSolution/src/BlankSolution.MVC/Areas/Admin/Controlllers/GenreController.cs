using BlankSolution.MVC.Areas.Admin.ViewModels.GenreViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BlankSolution.MVC.Areas.Admin.Controlllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7266/api");
        private readonly HttpClient _httpClient;
        public GenreController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<GenreGetViewModel> genreGetViewModels = new List<GenreGetViewModel>();
            var responseMessage = await _httpClient.GetAsync(baseAdress + "/genres/getall");
            if (responseMessage.IsSuccessStatusCode)
            {
                var datas = await responseMessage.Content.ReadAsStringAsync();
                genreGetViewModels = JsonConvert.DeserializeObject<List<GenreGetViewModel>>(datas);
            }
            return View(genreGetViewModels);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(GenrePostViewModel genrePostViewModel)
        {
            var dataStr=JsonConvert.SerializeObject(genrePostViewModel);
            var stringContent = new StringContent(dataStr, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync(baseAdress + "/Genres/Create",stringContent);
            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,GenrePutViewModel genrePutViewModel)
        {
            var dataStr = JsonConvert.SerializeObject(genrePutViewModel);
            var stringContent=new StringContent(dataStr,Encoding.UTF8, "application/json");
            var responseMessage=await _httpClient.PostAsync(baseAdress+"/Genres/Update/"+id, stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View();
        }
        [HttpGet]
        public async Task <IActionResult> Delete(int id)
        {
            var responseMessage=await _httpClient.DeleteAsync(baseAdress + "/Genres/Delete/" + id);
            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View();
        }
    }
}
