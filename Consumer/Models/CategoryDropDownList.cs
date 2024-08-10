using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Models
{
    public class CategoryDropDownList
    {
        public string selectedItem { get; set; }
        public List<SelectListItem> ActivityCategoryName { get; set; }
        public List<Guid> ActivityCategoryID { get; set; }
    }
}
