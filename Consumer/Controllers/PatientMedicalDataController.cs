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
    public class PatientMedicalDataController : Controller
    {
        public async Task<IActionResult> MedicalDataIndex()
        {
            List<PatientMedicalData> patientMedicalDataList = new List<PatientMedicalData>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/PatientMedicalDatas"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    patientMedicalDataList = JsonConvert.DeserializeObject<List<PatientMedicalData>>(apiResponse);
                    /*foreach(var p in patientMedicalDataList)
                    {
                        using (var response2 = await httpClient.GetAsync("https://localhost:44394/api/Patients/" + p.PatientID))
                        {
                            string apiResponse2 = await response2.Content.ReadAsStringAsync();                           
                            Patient patient = JsonConvert.DeserializeObject<Patient>(apiResponse2);
                            p.PatientName = patient.PatientName;
                        }
                    }*/
                }
            }
            return View(patientMedicalDataList);
        }

        public ViewResult GetPatientMedicalData() => View();

        [HttpPost]
        public async Task<IActionResult> GetPatientMedicalData(Guid id)
        {
            PatientMedicalData patientMedicalData = new PatientMedicalData();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/PatientMedicalDatas/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    patientMedicalData = JsonConvert.DeserializeObject<PatientMedicalData>(apiResponse);
                }
            }
            return View(patientMedicalData);
        }

        public ViewResult AddPatientMedicalData() => View();

        [HttpPost]
        public async Task<IActionResult> AddPatientMedicalData(PatientMedicalData patientMedicalData)
        {
            PatientMedicalData receivedPatientMedicalData = new PatientMedicalData();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(patientMedicalData), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44394/api/PatientMedicalDatas", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedPatientMedicalData = JsonConvert.DeserializeObject<PatientMedicalData>(apiResponse);
                }
            }
            return View(receivedPatientMedicalData);
        }

        public async Task<IActionResult> UpdatePatientMedicalData(Guid id)
        {
            PatientMedicalData patientMedicalData = new PatientMedicalData();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/PatientMedicalDatas/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    patientMedicalData = JsonConvert.DeserializeObject<PatientMedicalData>(apiResponse);
                }
            }
            return View(patientMedicalData);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePatientMedicalData(PatientMedicalData patientMedicalData)
        {
            PatientMedicalData receivedPatientMedicalData = new PatientMedicalData();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(patientMedicalData), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44394/api/PatientMedicalDatas/" + patientMedicalData.PatientMedicalDataID, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedPatientMedicalData = JsonConvert.DeserializeObject<PatientMedicalData>(apiResponse);
                }
            }
            return View(receivedPatientMedicalData);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePatientMedicalData(Guid PatientMedicalDataId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/PatientMedicalDatas/" + PatientMedicalDataId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("MedicalDataIndex");
        }
    }
}