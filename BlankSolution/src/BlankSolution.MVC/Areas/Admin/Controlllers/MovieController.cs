using BlankSolution.MVC.Areas.Admin.ViewModels.GenreViewModel;
using BlankSolution.MVC.Areas.Admin.ViewModels.MovieViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BlankSolution.MVC.Areas.Admin.Controlllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7266/api");
        private readonly HttpClient _httpClient;
        public MovieController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public async Task< IActionResult> Index()
        {
            List<MovieGetViewModel> movieGetViewModels = new List<MovieGetViewModel>();
            var responseMessage =await _httpClient.GetAsync(baseAdress + "/movies/getall");
            if(responseMessage.IsSuccessStatusCode)
            {
                var datas=await responseMessage.Content.ReadAsStringAsync();    
                movieGetViewModels=JsonConvert.DeserializeObject<List<MovieGetViewModel>>(datas);
                TempData["baseAdress"] = baseAdress;
                return View(movieGetViewModels);
            }
            return View();
        }
        [HttpGet]
        public async Task <IActionResult> Create()
        {
            List<GenreGetViewModel> genreGetViewModels = new List<GenreGetViewModel>();
            var responseMessage=await _httpClient.GetAsync(baseAdress+"/genres/getall");
            if(responseMessage.IsSuccessStatusCode)
            {
                var datas=await responseMessage.Content.ReadAsStringAsync();
                genreGetViewModels=JsonConvert.DeserializeObject<List<GenreGetViewModel>>(datas);
                ViewData["ListGenreViewModel"] = genreGetViewModels;
                return View();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MoviePostViewModel moviePostViewModel)
        {
            List<GenreGetViewModel> genreGetViewModels = new List<GenreGetViewModel>();
            var responseMessage = await _httpClient.GetAsync(baseAdress + "/genres/getall");
            if (responseMessage.IsSuccessStatusCode)
            {
                var datas = await responseMessage.Content.ReadAsStringAsync();
                genreGetViewModels = JsonConvert.DeserializeObject<List<GenreGetViewModel>>(datas);
                ViewData["ListGenreViewModel"] = genreGetViewModels;
            }
            using (var content=new MultipartFormDataContent())
            {
                content.Add(new StringContent(moviePostViewModel.Name), "Name");
                content.Add(new StringContent(moviePostViewModel.SalePrice.ToString()), "SalePrice");
                content.Add(new StringContent(moviePostViewModel.CostPrice.ToString()), "CostPrice");

                if(moviePostViewModel.ImageFile != null)
                {
                    var fileContent=new StreamContent(moviePostViewModel.ImageFile.OpenReadStream());
                    fileContent.Headers.ContentType=System.Net.Http.Headers.MediaTypeHeaderValue.Parse(moviePostViewModel.ImageFile.ContentType);
                    content.Add(fileContent,"ImageFile",moviePostViewModel.ImageFile.FileName);
                }
                var response = await _httpClient.PostAsync(baseAdress + "/movies/create", content);
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            MovieDeleteViewModel movieDeleteViewModel = new MovieDeleteViewModel() { Id=id};
            var datas=JsonConvert.SerializeObject(movieDeleteViewModel);
            var stirngContent = new StringContent(datas, Encoding.UTF8, "application/json");
            var response=await _httpClient.PostAsync(baseAdress+"/movies/delete/"+id, stirngContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }
    }

}
