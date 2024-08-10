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
    public class TrainingSceneController : Controller
    {
        public async Task<IActionResult> TrainingSceneIndex()
        {
            List<TrainingScene> trainingSceneList = new List<TrainingScene>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingScenes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    trainingSceneList = JsonConvert.DeserializeObject<List<TrainingScene>>(apiResponse);
                }
            }
            return View(trainingSceneList);
        }

        public ViewResult GetTrainingScene() => View();

        [HttpPost]
        public async Task<IActionResult> GetTrainingScene(Guid id)
        {
            TrainingScene trainingScene = new TrainingScene();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingScenes/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    trainingScene = JsonConvert.DeserializeObject<TrainingScene>(apiResponse);
                }
            }
            return View(trainingScene);
        }

        public ViewResult AddTrainingScene() => View();

        [HttpPost]
        public async Task<IActionResult> AddTrainingScene(TrainingScene TrainingScene)
        {
            TrainingScene receivedTrainingScene = new TrainingScene();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(TrainingScene), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44394/api/TrainingScenes", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedTrainingScene = JsonConvert.DeserializeObject<TrainingScene>(apiResponse);
                }
            }
            return View(receivedTrainingScene);
        }

        public async Task<IActionResult> UpdateTrainingScene(Guid id)
        {
            TrainingScene trainingScene = new TrainingScene();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingScenes/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    trainingScene = JsonConvert.DeserializeObject<TrainingScene>(apiResponse);
                }
            }
            return View(trainingScene);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTrainingScene(TrainingScene trainingScene)
        {
            TrainingScene receivedTrainingScene = new TrainingScene();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(trainingScene), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44394/api/TrainingScenes/" + trainingScene.TrainingSceneID, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedTrainingScene = JsonConvert.DeserializeObject<TrainingScene>(apiResponse);
                }
            }
            return View(receivedTrainingScene);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrainingScene(Guid trainingSceneId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/TrainingScenes/" + trainingSceneId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}