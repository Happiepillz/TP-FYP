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
    public class PatientController : Controller
    {
        public async Task<IActionResult> PatientIndex()
        {
            List<Patient> patientList = new List<Patient>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Patients"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    patientList = JsonConvert.DeserializeObject<List<Patient>>(apiResponse);
                }
            }
            return View(patientList);
        }

        public ViewResult GetPatient() => View();

        [HttpPost]
        public async Task<IActionResult> GetPatient(string id)
        {
            List<Patient> patientList = new List<Patient>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Patients/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    patientList = JsonConvert.DeserializeObject<List<Patient>>(apiResponse);
                }
            }
            return View(patientList);
        }

        [HttpPost]
        public async Task<IActionResult> SearchPatient(string name)
        {
            List<Patient> patientList = new List<Patient>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Patients/search/" + name))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    patientList = JsonConvert.DeserializeObject<List<Patient>>(apiResponse);
                }
            }
            return View(patientList);
        }

        public ViewResult SearchPatient() => View();

        public ViewResult AddPatient() => View();

        [HttpPost]
        public async Task<IActionResult> AddPatient(Patient Patient)
        {
            Patient receivedPatient = new Patient();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Patient), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44394/api/Patients", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedPatient = JsonConvert.DeserializeObject<Patient>(apiResponse);
                }
            }
            return View(receivedPatient);
        }

        public async Task<IActionResult> UpdatePatient(Guid id)
        {
            Patient patient = new Patient();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Patients/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    patient = JsonConvert.DeserializeObject<Patient>(apiResponse);
                }
            }
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePatient(Patient patient)
        {
            Patient receivedPatient = new Patient();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44394/api/Patients/" + patient.PatientID, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedPatient = JsonConvert.DeserializeObject<Patient>(apiResponse);
                }
            }
            return View(receivedPatient);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePatient(Guid patientId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/Patients/" + patientId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("PatientIndex");
        }
    }
}