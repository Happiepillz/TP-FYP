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
    public class TrainingInstanceController : Controller
    {
        public async Task<IActionResult> TrainingInstanceIndex()
        {
            List<TrainingInstance> trainingInstanceList = new List<TrainingInstance>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingInstances"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    trainingInstanceList = JsonConvert.DeserializeObject<List<TrainingInstance>>(apiResponse);
                }
            }
            return View(trainingInstanceList);
        }

        public ViewResult GetTrainingInstance() => View();

        [HttpPost]
        public async Task<IActionResult> GetTrainingInstance(Guid id)
        {
            TrainingInstance trainingInstance = new TrainingInstance();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingInstances/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    trainingInstance = JsonConvert.DeserializeObject<TrainingInstance>(apiResponse);
                }
            }
            return View(trainingInstance);
        }

        public ViewResult AddTrainingInstance() => View();

        [HttpPost]
        public async Task<IActionResult> AddTrainingInstance(TrainingInstance TrainingInstance)
        {
            TrainingInstance receivedTrainingInstance = new TrainingInstance();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(TrainingInstance), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44394/api/TrainingInstances", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedTrainingInstance = JsonConvert.DeserializeObject<TrainingInstance>(apiResponse);
                }
            }
            return View(receivedTrainingInstance);
        }

        public async Task<IActionResult> UpdateTrainingInstance(Guid id)
        {
            TrainingInstance trainingInstance = new TrainingInstance();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingInstances/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    trainingInstance = JsonConvert.DeserializeObject<TrainingInstance>(apiResponse);
                }
            }
            return View(trainingInstance);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTrainingInstance(TrainingInstance trainingInstance)
        {
            TrainingInstance receivedTrainingInstance = new TrainingInstance();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(trainingInstance), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44394/api/TrainingInstances/" + trainingInstance.TrainingInstanceID, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedTrainingInstance = JsonConvert.DeserializeObject<TrainingInstance>(apiResponse);
                }
            }
            return View(receivedTrainingInstance);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrainingInstance(Guid trainingInstanceId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/TrainingInstances/" + trainingInstanceId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}