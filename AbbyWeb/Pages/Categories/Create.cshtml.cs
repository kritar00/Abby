using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AbbyWeb.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "The display order cannot match the name!");
            }
            if(ModelState.IsValid) { 
            await _db.Category.AddAsync(Category);
            await _db.SaveChangesAsync();
                TempData["Success"] = "Category created";
            return RedirectToPage("Index");
            }
            TempData["Error"] = "Error creating";
            return Page();
        }
    }
}