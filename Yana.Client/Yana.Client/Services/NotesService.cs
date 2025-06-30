using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yana.Client.Shared.Models;

namespace Yana.Client.Services
{
    public class NotesService
    {
        private readonly HttpClient _httpClient;

        public NotesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            try
            {
                // GetFromJsonAsync automatically deserializes the JSON response into a List<Note>
                var notes = await _httpClient.GetFromJsonAsync<List<Note>>("api/Notes");
                return notes ?? new List<Note>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error Fetching notes: {ex.Message}");
                return new List<Note>();
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"The content type is not supported: {ex.Message}");
                return new List<Note>();
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine($"Invalid Json: {ex.Message}");
                return new List<Note>();
            }
        }
    }
}
