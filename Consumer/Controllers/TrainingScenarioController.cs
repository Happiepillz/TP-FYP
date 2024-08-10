using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Consumer.Models;

namespace Consumer.Controllers
{
    public class TrainingScenarioController : Controller
    {
        public async Task<IActionResult> TrainingScenarioIndex()
        {
            List<TrainingScenario> trainingScenarioList = new List<TrainingScenario>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingScenarios"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    trainingScenarioList = JsonConvert.DeserializeObject<List<TrainingScenario>>(apiResponse);
                }
            }
            return View(trainingScenarioList);
        }

        public ViewResult GetTrainingScenario() => View();

        [HttpPost]
        public async Task<IActionResult> GetTrainingScenario(Guid id)
        {
            TrainingScenario trainingScenario = new TrainingScenario();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingScenarios/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    trainingScenario = JsonConvert.DeserializeObject<TrainingScenario>(apiResponse);
                }
            }
            return View(trainingScenario);
        }

        public ViewResult AddTrainingScenario() => View();

        [HttpPost]
        public async Task<IActionResult> AddTrainingScenario(TrainingScenario TrainingScenario)
        {
            TrainingScenario receivedTrainingScenario = new TrainingScenario();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(TrainingScenario), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44394/api/TrainingScenarios", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedTrainingScenario = JsonConvert.DeserializeObject<TrainingScenario>(apiResponse);
                }
            }
            return View(receivedTrainingScenario);
        }

        public async Task<IActionResult> UpdateTrainingScenario(Guid id)
        {
            TrainingScenario trainingScenario = new TrainingScenario();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingScenarios/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    trainingScenario = JsonConvert.DeserializeObject<TrainingScenario>(apiResponse);
                }
            }
            return View(trainingScenario);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTrainingScenario(TrainingScenario trainingScenario)
        {
            TrainingScenario receivedTrainingScenario = new TrainingScenario();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(trainingScenario), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44394/api/TrainingScenarios/" + trainingScenario.TrainingScenarioID, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedTrainingScenario = JsonConvert.DeserializeObject<TrainingScenario>(apiResponse);
                }
            }
            return View(receivedTrainingScenario);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrainingScenario(Guid trainingScenarioId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/TrainingScenarios/" + trainingScenarioId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}