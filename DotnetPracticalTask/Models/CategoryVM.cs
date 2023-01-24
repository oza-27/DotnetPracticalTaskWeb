using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotnetPracticalTask.Models
{
    public class CategoryVM
    {
        public Category Category { get; set; }
        public IEnumerable<SelectListItem> categoryList { get; set; }
    }
}
