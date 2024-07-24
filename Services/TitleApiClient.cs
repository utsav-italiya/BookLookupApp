using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BookLookupApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace BookLookupApp.Services
{
    public class TitleApiClient : ITitleApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TitleApiClient> _logger;

        public TitleApiClient(HttpClient httpClient, IConfiguration configuration, ILogger<TitleApiClient> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<TitleDto> GetTitleAsync(string isbn)
        {
            var apiUrl = _configuration["BookConnectApiUrl"];
            var apiKey = _configuration["BookConnectApiKey"];

            var request = new HttpRequestMessage(HttpMethod.Get, $"{apiUrl}/partner/title/{isbn}");
            request.Headers.Add("bci-key", apiKey);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to fetch book details for ISBN {isbn}. Status code: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();

            try
            {
                var jsonObj = JObject.Parse(json);

                var titleDto = new TitleDto
                {
                    Title = jsonObj["TitleText"]?.ToString(),
                    Subtitle = jsonObj["Subtitle"]?.ToString(),
                    CoverImage = "https://app.bookconnect.ca" + jsonObj["ImageFiles"]?.FirstOrDefault(image => image["ShortIdentifier"]?.ToString() == "ci")?["SafeUrl"]?.ToString(),
                    Price = jsonObj["RetailUS"]?.ToObject<decimal>() ?? 0,
                    Description = jsonObj["OtherTexts"]?.FirstOrDefault(t => t["TextTypeCode"]?.ToString() == "01")?["TextBody"]?.ToString(),
                    Authors = jsonObj["Contributors"]?
                                  .Select(c => $"{c["ContributorPerson"]["NamesBeforeKey"]} {c["ContributorPerson"]["KeyNames"]}")
                                  .ToList(),
                    Subject = jsonObj["TitleGroup"]?["CodeSubject1"]?["SubjectDescription"]?.ToString()
                };

                return titleDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error parsing JSON response: {ex.Message}");
                _logger.LogError($"Response content: {json}");
                return null;
            }
        }
    }
}
