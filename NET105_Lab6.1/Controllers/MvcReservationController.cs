using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NET105_Lab6._1.Models;
using System.Text;
namespace NET105_Lab6._1.Controllers
{
    public class MvcReservationController: Controller
    {
        private readonly HttpClient _httpClient;
        public MvcReservationController (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            List<Reservation> reservationList = new List<Reservation>();
            var response = await _httpClient.GetAsync("https://localhost:44336/api/Reservations");
            if(response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                reservationList=JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
            }
            return View(reservationList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create (Reservation reservation)
        {
            var content= new StringContent(JsonConvert.SerializeObject(reservation),Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("https://localhost:44336/api/Reservations", content);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); 
            }
            return View(reservation);
        }
        public async Task<IActionResult> Details ( int id)
        {
            Reservation reservation = new Reservation();
            var response = await _httpClient.GetAsync($"https://localhost:44336/api/Reservations/{id}");
            if(response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
            }
            return View(reservation);
        }
        public async Task<IActionResult> Edit (int id)
        {
            Reservation reservation = new Reservation();
            var response = await _httpClient.GetAsync($"https://localhost:44336/api/Reservations/{id}");
            if( response.IsSuccessStatusCode )
            {
                var apiResponse= await response.Content.ReadAsStringAsync();
                reservation= JsonConvert.DeserializeObject<Reservation>(apiResponse);
            }
            return View(reservation);
        }
        [HttpPost]
        public async Task<IActionResult> Edit( int id, Reservation reservation)
        {
            var conten = new StringContent(JsonConvert.SerializeObject(reservation),Encoding.UTF8,"application/json");
            var response = await _httpClient.PutAsync($"https://localhost:44336/api/Reservations/{id}", conten);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        public async Task<IActionResult> Delete (int id)
        {
            Reservation reservation= new Reservation();
            var response = await _httpClient.GetAsync($"https://localhost:44336/api/Reservations/{id}");
            if( response.IsSuccessStatusCode ) 
            {
                var apiResponse= await response.Content.ReadAsStringAsync();
                reservation= JsonConvert.DeserializeObject<Reservation>(apiResponse);
            }
            return View(reservation) ;
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {

            var response = await _httpClient.DeleteAsync($"https://localhost:44336/api/Reservations/{id}");
            if( response.IsSuccessStatusCode )
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
