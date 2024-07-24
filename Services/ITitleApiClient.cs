using System.Threading.Tasks;
using BookLookupApp.Models;

namespace BookLookupApp.Services
{
    public interface ITitleApiClient
    {
        Task<TitleDto> GetTitleAsync(string isbn);
    }
}