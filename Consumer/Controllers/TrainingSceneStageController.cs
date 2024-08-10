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
    public class TrainingSceneStageController : Controller
    {
        public async Task<IActionResult> TrainingSceneStageIndex()
        {
            List<TrainingSceneStage> trainingSceneStageList = new List<TrainingSceneStage>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingSceneStages"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    trainingSceneStageList = JsonConvert.DeserializeObject<List<TrainingSceneStage>>(apiResponse);
                }
            }
            return View(trainingSceneStageList);
        }

        public ViewResult GetTrainingSceneStage() => View();

        [HttpPost]
        public async Task<IActionResult> GetTrainingSceneStage(Guid id)
        {
            TrainingSceneStage trainingSceneStage = new TrainingSceneStage();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingSceneStages/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    trainingSceneStage = JsonConvert.DeserializeObject<TrainingSceneStage>(apiResponse);
                }
            }
            return View(trainingSceneStage);
        }

        public ViewResult AddTrainingSceneStage() => View();

        [HttpPost]
        public async Task<IActionResult> AddTrainingSceneStage(TrainingSceneStage TrainingSceneStage)
        {
            TrainingSceneStage receivedTrainingSceneStage = new TrainingSceneStage();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(TrainingSceneStage), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44394/api/TrainingSceneStages", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedTrainingSceneStage = JsonConvert.DeserializeObject<TrainingSceneStage>(apiResponse);
                }
            }
            return View(receivedTrainingSceneStage);
        }

        public async Task<IActionResult> UpdateTrainingSceneStage(Guid id)
        {
            TrainingSceneStage trainingSceneStage = new TrainingSceneStage();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingSceneStages/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    trainingSceneStage = JsonConvert.DeserializeObject<TrainingSceneStage>(apiResponse);
                }
            }
            return View(trainingSceneStage);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTrainingSceneStage(TrainingSceneStage trainingSceneStage)
        {
            TrainingSceneStage receivedTrainingSceneStage = new TrainingSceneStage();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(trainingSceneStage), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44394/api/TrainingSceneStages/" + trainingSceneStage.TSStageID, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedTrainingSceneStage = JsonConvert.DeserializeObject<TrainingSceneStage>(apiResponse);
                }
            }
            return View(receivedTrainingSceneStage);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrainingSceneStage(Guid trainingSceneStageId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/TrainingSceneStages/" + trainingSceneStageId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}