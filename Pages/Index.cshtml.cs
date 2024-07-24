using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLookupApp.Models;
using BookLookupApp.Services;

namespace BookLookupApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITitleApiClient _titleApiClient;

        public IndexModel(ITitleApiClient titleApiClient)
        {
            _titleApiClient = titleApiClient;
        }

        [BindProperty]
        public string ISBN { get; set; }
        
        public TitleDto Book { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(ISBN))
            {
                ModelState.AddModelError(string.Empty, "Please enter a valid ISBN (International Standard Book Number).");
                return Page();
            }

            Book = await _titleApiClient.GetTitleAsync(ISBN);

            if (Book == null)
            {
                ModelState.AddModelError(string.Empty, "Book not found.");
            }

            return Page();
        }
    }
}
