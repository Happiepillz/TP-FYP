using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Consumer.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Consumer.ModelObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using System.Web;
using System.Web.Razor;
//using MPProject.Models;

namespace APIConsume.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(IConfiguration config)
        {
            this.config = config;
        }

        public IConfiguration config { get; }

        private string baseUrl => config["API:url"];
        //=========================================== Login ==========================================================

        public IActionResult Index()
        {
            return View("Index");
            //login button function ++++++
        }

        public async Task<IActionResult> Login(User user)
        {
            //string url = baseUrl + "/users/getuser/";
            string url = "https://localhost:44394/api/users/getuser/" + user.UserName;
            User userModel = new User();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userModel = JsonConvert.DeserializeObject<User>(apiResponse);

                }
                if (userModel != null && userModel.UserPassword.Equals(user.UserPassword))
                {
                    //HttpContext.Session.SetString("username", userModel.Surname + " " + userModel.GivenName);
                    return View("~/Views/Home/About_partial.cshtml");

                }
                else
                {
                    ViewBag.error = "Invalid! Please enter the correct authentication.";
                    return View("~/Views/Home/Index.cshtml");
                }
                //    return Content(content.ToString());
            }
        }
        //==============================================================================================================

        //=========================================== Logout ===========================================================
        //[HttpGet]
        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Remove("UserName");
        //    return RedirectToAction("Index");
        //}


        // ----used modelobjects paginated list.
        //[HttpGet]
        //public async Task<IActionResult> UserIndex(string sortOrder, int? pageNumber)
        //{
        //    ViewData["CurrentSort"] = sortOrder;
        //    ViewData["UserNameSortParm"] = sortOrder == "UserName" ? "UserName_desc" : "UserName";
        //    ViewData["UserEmailSortParm"] = sortOrder == "UserEmail" ? "UserEmail_desc" : "UserEmail";
        //    ViewData["UserContactSortParm"] = sortOrder == "UserContact" ? "UserContact_desc" : "UserContact";
        //    ViewData["UserPasswordSortParm"] = sortOrder == "UserPassword" ? "UserPassword_desc" : "UserPassword";

        //    //string url = baseUrl + "/users";
        //    List<User> userList = new List<User>();
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync("https://localhost:44394/api/users/get/" + sortOrder))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            userList = JsonConvert.DeserializeObject<List<User>>(apiResponse);
        //        }
        //    }
        //    //return View("~/Views/Home/UserIndex.cshtml", userList);
        //    int pageSize = 10;
        //    return View("~/Views/Home/UserIndex.cshtml", PaginatedList<User>.CreateAsync(userList, pageNumber ?? 1, pageSize));

        //}
        [HttpGet]
        public async Task<IActionResult> UserIndex(string sortOrder, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["UserNameSortParm"] = sortOrder == "UserName" ? "UserName_desc" : "UserName";
            ViewData["UserEmailSortParm"] = sortOrder == "UserEmail" ? "UserEmail_desc" : "UserEmail";
            ViewData["UserContactSortParm"] = sortOrder == "UserContact" ? "UserContact_desc" : "UserContact";
            ViewData["UserPasswordSortParm"] = sortOrder == "UserPassword" ? "UserPassword_desc" : "UserPassword";

            //string url = baseUrl + "/users";
            List<User> userList = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/users/get/" + sortOrder))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userList = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
            //return View("~/Views/Home/UserIndex.cshtml", userList);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            // return View("~/Views/Home/UserIndex.cshtml", PaginatedList<User>.CreateAsync(userList, pageNumber ?? 1, pageSize));
            return View(userList.ToPagedList(pageNumber, pageSize));

        }


        //===============================================ADD USER===========================================
        public ViewResult AddUser() => View();
        //==========================================================Add User =====================================================
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            string addurl = baseUrl + "/users";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(addurl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedUser = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            List<User> userList = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/users"))
                {
                    string apiResponse2 = await response.Content.ReadAsStringAsync();
                    userList = JsonConvert.DeserializeObject<List<User>>(apiResponse2);
                }
            }
            return View("~/Views/Home/UserIndex.cshtml", userList);
        }


        //====================================Update User==================================================
        public Guid Id_test { get; set; }
        public async Task<IActionResult> UpdateUser(Guid id)
        {
            Guid UserId = id;
            Id_test = id;
            User readingUser = new User();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/users/" + UserId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    readingUser = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            return View(readingUser);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUser(User user)
        {
            User receivedUser = new User();
            string apiResponse;
            using (var httpClient = new HttpClient())
            {
                User user_abc = new User();
                user_abc.UserId = new Guid(user.UserId.ToString());
                user_abc.UserName = user.UserName;
                user_abc.UserEmail = user.UserEmail;
                user_abc.UserContact = user.UserContact;
                user_abc.UserPassword = user.UserPassword;

                string json = JsonConvert.SerializeObject(user_abc);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44394/api/users/" + user.UserId, content))
                {

                    apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success " + apiResponse;
                    receivedUser = JsonConvert.DeserializeObject<User>(apiResponse);
                }

            }

            List<User> userList = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/users"))
                {
                    string apiResponse2 = await response.Content.ReadAsStringAsync();
                    userList = JsonConvert.DeserializeObject<List<User>>(apiResponse2);
                }
            }
            return View("~/Views/Home/UserIndex.cshtml", userList);


        }

        //============================================== Delete ==================================================================
        //====================================Delete function is only able to delete user without foregin key ======================
        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid UserId)
        {
            //string hiass = "maybe is deleted";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/users/" + UserId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            List<User> userList = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/users"))
                {
                    string apiResponse2 = await response.Content.ReadAsStringAsync();
                    userList = JsonConvert.DeserializeObject<List<User>>(apiResponse2);
                }
            }
            return View("~/Views/Home/UserIndex.cshtml", userList);

        }


        // Display all but replacing activity type ID with activity with sorting ===========================================================
        [HttpGet]
        public async Task<IActionResult> ActivityLog(string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["UserNameSortParm"] = sortOrder == "UserName" ? "UserName_desc" : "UserName";
            ViewData["ActivityDateSortParm"] = sortOrder == "ActivityDateTime" ? "ActivityDateTime_desc" : "ActivityDateTime";
            ViewData["ActivityDataNameSortParm"] = sortOrder == "ActivityDataName" ? "ActivityDataName_desc" : "ActivityDataName";
            ViewData["ActivityDataValueSortParm"] = sortOrder == "ActivityDataValue" ? "ActivityDataValue_desc" : "ActivityDataValue";
            ViewData["ActivityStatusSortParm"] = sortOrder == "ActivityStatus" ? "ActivityStatus_desc" : "ActivityStatus";
            ViewData["DrugNameSortParm"] = sortOrder == "DrugName" ? "DrugName_desc" : "DrugName";
            ViewData["TSStageNameSortParm"] = sortOrder == "TSStageName" ? "TSStageName_desc" : "TSStageName";
            ViewData["ActivityTypeNameSortParm"] = sortOrder == "ActivityTypeName" ? "ActivityTypeName_desc" : "ActivityTypeName";
            List<ActivityLogUserDTO> ActLogList = new List<ActivityLogUserDTO>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/ActivityLogs/dto/ActivityLog/" + sortOrder))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ActLogList = JsonConvert.DeserializeObject<List<ActivityLogUserDTO>>(apiResponse);
                }
            }

            int pageSize = 10;
            return View("~/Views/Home/ActivityLog.cshtml", PaginatedList<ActivityLogUserDTO>.CreateAsync(ActLogList, pageNumber ?? 1, pageSize));
        }

        public IActionResult About_partial()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public class ViewModel
        {
            public IEnumerable<ActivityLogUserDTO> ActivityLogUserDTO { get; set; }
            public IEnumerable<TrgInstUserDto> TrgInstUserDto { get; set; }
        }
        //======================================Display Category Log ======================================
        public async Task<IActionResult> ActCatPost(string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ActivityCategoryNameParm"] = sortOrder == "ActivityCategoryName" ? "ActivityCategoryName_desc" : "ActivityCategoryName";
            ViewData["ActivityCategoryDescriptionParm"] = sortOrder == "ActivityCategoryDescription" ? "ActivityCategoryDescription_desc" : "ActivityCategoryDescription";

            List<ActivityCategory> CatList = new List<ActivityCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/ActivityCategories/get/" + sortOrder))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    CatList = JsonConvert.DeserializeObject<List<ActivityCategory>>(apiResponse);
                }
                
            }
            int pageSize = 10;
            return View("~/Views/Home/ActCategory.cshtml", PaginatedList<ActivityCategory>.CreateAsync(CatList, pageNumber ?? 1, pageSize));
            
        }
        //================================== Update ========================================================
        public Guid id_test2 { get; set; }
        public async Task<IActionResult> updateCat(Guid id)
        {
            Guid ActivityCategoryID = id;
            id_test2 = id;
            ActivityCategory updateCat = new ActivityCategory();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/ActivityCategories/" + ActivityCategoryID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    updateCat = JsonConvert.DeserializeObject<ActivityCategory>(apiResponse);
                }
            }
            return View(updateCat);
        }

        //Updating Category--------------------------------------------------------------------------------------------

        public async Task<IActionResult> updateCat2(ActivityCategory activityCat)
        {
            ActivityCategory activityCats = new ActivityCategory();
            string apiResponse;
            using (var httpClient = new HttpClient())
            {
                ActivityCategory activityCater = new ActivityCategory();
                activityCater.ActivityCategoryID = new Guid(activityCat.ActivityCategoryID.ToString());
                activityCater.ActivityCategoryName = activityCat.ActivityCategoryName;
                activityCater.ActivityCategoryDescription = activityCat.ActivityCategoryDescription;



                string json = JsonConvert.SerializeObject(activityCater);
                //var stringContent = new StringContent(json);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44394/api/ActivityCategories/" + activityCat.ActivityCategoryID, content))
                {

                    apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success " + apiResponse;
                    activityCats = JsonConvert.DeserializeObject<ActivityCategory>(apiResponse);
                }
                //return Content(content.ToString());
            }
            List<ActivityCategory> ActCatList = new List<ActivityCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/ActivityCategories"))
                {
                    string apiResponse2 = await response.Content.ReadAsStringAsync();
                    ActCatList = JsonConvert.DeserializeObject<List<ActivityCategory>>(apiResponse2);
                }
            }
            return View("~/Views/Home/ActCategory.cshtml", ActCatList);
        }


        //Adding Category--------------------------------------------------------------------------------------------------------

        public ViewResult AddCat() => View("~/Views/Home/AddCategory.cshtml");

        [HttpPost]
        public async Task<IActionResult> AddCat(ActivityCategory actcat)
        {
            ActivityCategory newActCat = new ActivityCategory();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(actcat), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44394/api/ActivityCategories/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    newActCat = JsonConvert.DeserializeObject<ActivityCategory>(apiResponse);


                }

            }
            List<ActivityCategory> ActCatList = new List<ActivityCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/ActivityCategories"))
                {
                    string apiResponse2 = await response.Content.ReadAsStringAsync();
                    ActCatList = JsonConvert.DeserializeObject<List<ActivityCategory>>(apiResponse2);
                }
            }
            return View("~/Views/Home/ActCategory.cshtml", ActCatList);
        }
        //========Delete Category===============
        [HttpGet("[Controller]/DeleteCat/{ActivityCategoryID}")]
        public async Task<IActionResult> DeleteCategory(Guid ActivityCategoryID)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/ActivityCategories/" + ActivityCategoryID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            List<ActivityCategory> ActCatList = new List<ActivityCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/ActivityCategories"))
                {
                    string apiResponse2 = await response.Content.ReadAsStringAsync();
                    ActCatList = JsonConvert.DeserializeObject<List<ActivityCategory>>(apiResponse2);
                }
            }
            return View("~/Views/Home/ActCategory.cshtml", ActCatList);
        }

        //=========== Activity Type Display ===================
        public async Task<IActionResult> ActTypeDisplay(string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ActivityCategoryNameParm"] = sortOrder == "ActivityCategoryName" ? "ActivityCategoryName_desc" : "ActivityCategoryName";
            ViewData["ActivityTypeDescriptionParm"] = sortOrder == "ActivityTypeDescription" ? "ActivityTypeDescription_desc" : "ActivityTypeDescription";
            ViewData["ActivityTypeIDParm"] = sortOrder == "ActivityTypeID" ? "ActivityTypeID_desc" : "ActivityTypeID";
            ViewData["ActivityTypeNameParm"] = sortOrder == "ActivityTypeName" ? "ActivityTypeName_desc" : "ActivityTypeName";

            List<TypeCategoryDTO> ActStatList = new List<TypeCategoryDTO>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/ActivityTypes/dto/ActivityType2/" + sortOrder))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ActStatList = JsonConvert.DeserializeObject<List<TypeCategoryDTO>>(apiResponse);
                }
            }
            //return View("~/Views/Home/TypeIndex.cshtml", ActStatList);
            int pageSize = 10;
            return View("~/Views/Home/TypeIndex.cshtml", PaginatedList<TypeCategoryDTO>.CreateAsync(ActStatList, pageNumber ?? 1, pageSize));
        }
        //================= Add New Activity Type =========================
        public async Task<IActionResult> PopActCatDDL()
        {
            List<SelectListItem> listOfCat = new List<SelectListItem>();
            List<ActivityCategory> categoryList = new List<ActivityCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/ActivityCategories"))
                {
                    string apiResponse3 = await response.Content.ReadAsStringAsync();


                    categoryList = JsonConvert.DeserializeObject<List<ActivityCategory>>(apiResponse3);
                    foreach (var item in categoryList)
                    {
                        var listCat = new SelectListItem(item.ActivityCategoryName.ToString(), item.ActivityCategoryID.ToString());
                        listOfCat.Add(listCat);

                    }

                }
                var drop = new CategoryDropDownList();
                drop.ActivityCategoryName = listOfCat;
                ViewData["Categories"] = drop;
            }
            return View("~/Views/Home/AddActType.cshtml");
        }
        public async Task<IActionResult> AddType(ActivityType actType)
        {
            string addurl = baseUrl + "/ActivityTypes";
            string strDDLValue = Request.Form["ActCat"].ToString();
            var item3 = new SelectedActCat();
            item3.selectedActCategory = strDDLValue;
            actType.ActivityCategoryID = Guid.Parse(strDDLValue);
            ViewData["select"] = item3;
            ActivityType receivedType = new ActivityType();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(actType), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(addurl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                }
            }
            
            return Content(strDDLValue) ;

        }

        //================= Update Activity Type ===========================

        //================Get Drug List========================
        public async Task<IActionResult> DrugIndex(string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DrugNameSortParm"] = sortOrder == "DrugName" ? "DrugName_desc" : "DrugName";
            ViewData["DrugDosageSortParm"] = sortOrder == "DrugDosage" ? "DrugDosage_desc" : "DrugDosage";
            ViewData["DrugDescriptionSortParm"] = sortOrder == "DrugDescription" ? "DrugDescription_desc" : "DrugDescription";
            ViewData["DrugTypeSortParm"] = sortOrder == "DrugType" ? "DrugType_desc" : "DrugType";
            ViewData["DrugSideEffectsSortParm"] = sortOrder == "DrugSideEffects" ? "DrugSideEffects_desc" : "DrugSideEffects";
            ViewData["DrugPrecautionsSortParm"] = sortOrder == "DrugPrecautions" ? "DrugPrecautions_desc" : "DrugPrecautions";
            ViewData["DrugInteractionsSortParm"] = sortOrder == "DrugInteractions" ? "DrugInteractions_desc" : "DrugInteractions";
            ViewData["DrugRemarksSortParm"] = sortOrder == "DrugRemarks" ? "DrugRemarks_desc" : "DrugRemarks";

            List<Drug> DrugList = new List<Drug>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Drugs/Sort/" + sortOrder))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DrugList = JsonConvert.DeserializeObject<List<Drug>>(apiResponse);
                }
            }
            //return View(DrugList);
            int pageSize = 10;
            return View("~/Views/Home/DrugIndex.cshtml", PaginatedList<Drug>.CreateAsync(DrugList, pageNumber ?? 1, pageSize));
        }
       
        //===============Add Drug==========================
        public ViewResult AddDrug() => View("~/Views/Home/Druggie.cshtml");

        [HttpPost]
        public async Task<IActionResult> AddDrug(Drug Druggie)
        {
            Drug newDruggie = new Drug();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Druggie), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44394/api/Drugs/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    newDruggie = JsonConvert.DeserializeObject<Drug>(apiResponse);


                }

            }
            List<Drug> DrugIndex = new List<Drug>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Drugs"))
                {
                    string apiResponse2 = await response.Content.ReadAsStringAsync();
                    DrugIndex = JsonConvert.DeserializeObject<List<Drug>>(apiResponse2);
                }
            }
            return View("~/Views/Home/DrugIndex.cshtml", DrugIndex);
        }
        //===================Update Drug==============================
        public Guid drug_test { get; set; }
        public async Task<IActionResult> updateDrug1(Guid id)
        {
            Guid Drug = id;
            drug_test = id;
            Drug updateDrug = new Drug();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Drugs/" + Drug))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    updateDrug = JsonConvert.DeserializeObject<Drug>(apiResponse);
                }
            }

            return View("~/Views/Home/UpdateDrug.cshtml", updateDrug);
        }

        public async Task<IActionResult> updateDrug(Drug activityDrugs)
        {
            Drug drugUpdate = new Drug();
            string apiResponse;
            using (var httpClient = new HttpClient())
            {
                Drug DrugUpdater = new Drug();
                DrugUpdater.DrugId = new Guid(activityDrugs.DrugId.ToString());
                DrugUpdater.DrugName = activityDrugs.DrugName;
                DrugUpdater.DrugDosage = activityDrugs.DrugDosage;
                DrugUpdater.DrugDescription = activityDrugs.DrugDescription;
                DrugUpdater.DrugType = activityDrugs.DrugType;
                DrugUpdater.DrugSideEffects = activityDrugs.DrugSideEffects;
                DrugUpdater.DrugPrecautions = activityDrugs.DrugPrecautions;
                DrugUpdater.DrugInteractions = activityDrugs.DrugInteractions;
                DrugUpdater.DrugRemarks = activityDrugs.DrugRemarks;


                string json = JsonConvert.SerializeObject(DrugUpdater);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44394/api/Drugs/" + activityDrugs.DrugId, content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success" + apiResponse;
                    drugUpdate = JsonConvert.DeserializeObject<Drug>(apiResponse);
                }
            }

            List<Drug> drugUpdateList = new List<Drug>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Drugs"))
                {
                    string apiResponse2 = await response.Content.ReadAsStringAsync();
                    drugUpdateList = JsonConvert.DeserializeObject<List<Drug>>(apiResponse2);
                }
            }

            return View("~/Views/Home/DrugIndex.cshtml", drugUpdateList);
        }

        //============Delete Drug===============

        [HttpGet("[Controller]/DeleteDrug/{DrugID}")]
        public async Task<IActionResult> DeleteDrug(Guid DrugID)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/Drugs/" + DrugID))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            List<Drug> DrugList = new List<Drug>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Drugs"))
                {
                    string apiResponse2 = await response.Content.ReadAsStringAsync();
                    DrugList = JsonConvert.DeserializeObject<List<Drug>>(apiResponse2);
                }
            }
            return View("~/Views/Home/DrugIndex.cshtml", DrugList);
        }
        //=================================== Search User ============================================================
        public async Task<IActionResult> SearchUser()
        {
            List<User> userList = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/users"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userList = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
            return View(userList);
        }
        [HttpPost]
        public async Task<IActionResult> SearchUser(string name)
        {

            List<User> UserList = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Users/search/" + name))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UserList = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }

            return View(UserList);
        }
        [HttpPost]
        public async Task<IActionResult> SelectDate(Guid UserId)
        {

            List<TrgInstUserDto> DateList = new List<TrgInstUserDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/TrainingInstances/dto/TngInstuser/" + UserId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DateList = JsonConvert.DeserializeObject<List<TrgInstUserDto>>(apiResponse);
                }
            }
            TempData["userid"] = UserId;
            TempData.Keep("userid");
            return View(DateList);
        }
        [HttpPost]
        public async Task<IActionResult> DisplayLog(Guid TrainingInstanceID)
        {
            Guid iduser = (Guid)TempData["userid"];
            List<ActivityLogUserDTO> LogList = new List<ActivityLogUserDTO>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/ActivityLogs/dto/loguser/" + TrainingInstanceID + "/" + iduser))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    LogList = JsonConvert.DeserializeObject<List<ActivityLogUserDTO>>(apiResponse);
                }
                return View(LogList);
            }
        }

    }
}
