using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yana.Client.Shared.Models;
using Microsoft.Extensions.Logging;

namespace Yana.Client.Shared.Services
{
    public class NotesService : INotesService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NotesService>? _logger;

        public NotesService(HttpClient httpClient, ILogger<NotesService>? logger = null)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            try
            {
                var requestUri = $"{_httpClient.BaseAddress}api/Notes";
                _logger?.LogInformation($"Attempting to fetch notes from: {requestUri}");
                _logger?.LogInformation($"HttpClient.BaseAddress: {_httpClient.BaseAddress}");
                _logger?.LogInformation($"Request timeout: {_httpClient.Timeout}");
                
                // GetFromJsonAsync automatically deserializes the JSON response into a List<Note>
                var notes = await _httpClient.GetFromJsonAsync<List<Note>>("api/Notes");
                
                _logger?.LogInformation($"Successfully fetched {notes?.Count ?? 0} notes");
                return notes ?? new List<Note>();
            }
            catch (HttpRequestException ex)
            {
                var errorMsg = $"HTTP Error fetching notes from {_httpClient.BaseAddress}: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                
                // Log additional details for debugging
                if (ex.InnerException != null)
                {
                    var innerMsg = $"Inner exception: {ex.InnerException.Message}";
                    _logger?.LogError(innerMsg);
                    Console.WriteLine(innerMsg);
                }
                
                return new List<Note>();
            }
            catch (TaskCanceledException ex)
            {
                var errorMsg = $"Request timeout fetching notes from {_httpClient.BaseAddress}: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return new List<Note>();
            }
            catch (NotSupportedException ex)
            {
                var errorMsg = $"Content type not supported: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return new List<Note>();
            }
            catch (System.Text.Json.JsonException ex)
            {
                var errorMsg = $"Invalid JSON response: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return new List<Note>();
            }
            catch (Exception ex)
            {
                var errorMsg = $"Unexpected error fetching notes: {ex.GetType().Name} - {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                
                // Log stack trace for debugging
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                
                return new List<Note>();
            }
        }

        public async Task<Note?> GetNoteByIdAsync(Guid id)
        {
            try
            {
                var requestUri = $"{_httpClient.BaseAddress}api/Notes/{id}";
                _logger?.LogInformation($"Attempting to fetch note by ID from: {requestUri}");
                
                var note = await _httpClient.GetFromJsonAsync<Note>($"api/Notes/{id}");
                
                _logger?.LogInformation($"Successfully fetched note with ID: {note?.Id}");
                return note;
            }
            catch (HttpRequestException ex)
            {
                var errorMsg = $"HTTP Error fetching note by ID {id}: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                
                if (ex.InnerException != null)
                {
                    var innerMsg = $"Inner exception: {ex.InnerException.Message}";
                    _logger?.LogError(innerMsg);
                    Console.WriteLine(innerMsg);
                }
                
                return null;
            }
            catch (TaskCanceledException ex)
            {
                var errorMsg = $"Request timeout fetching note by ID {id}: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return null;
            }
            catch (NotSupportedException ex)
            {
                var errorMsg = $"Content type not supported: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return null;
            }
            catch (System.Text.Json.JsonException ex)
            {
                var errorMsg = $"Invalid JSON response: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return null;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Unexpected error fetching note by ID {id}: {ex.GetType().Name} - {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return null;
            }
        }

        public async Task<Note?> CreateNoteAsync(Note note)
        {
            try
            {
                var requestUri = $"{_httpClient.BaseAddress}api/Notes";
                _logger?.LogInformation($"Attempting to create note at: {requestUri}");
                
                var response = await _httpClient.PostAsJsonAsync("api/Notes", note);
                
                if (response.IsSuccessStatusCode)
                {
                    var createdNote = await response.Content.ReadFromJsonAsync<Note>();
                    _logger?.LogInformation($"Successfully created note with ID: {createdNote?.Id}");
                    return createdNote;
                }
                else
                {
                    var errorMsg = $"Error creating note: {response.StatusCode} - {response.ReasonPhrase}";
                    _logger?.LogError(errorMsg);
                    Console.WriteLine(errorMsg);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                var errorMsg = $"HTTP Error creating note: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                
                if (ex.InnerException != null)
                {
                    var innerMsg = $"Inner exception: {ex.InnerException.Message}";
                    _logger?.LogError(innerMsg);
                    Console.WriteLine(innerMsg);
                }
                
                return null;
            }
            catch (TaskCanceledException ex)
            {
                var errorMsg = $"Request timeout creating note: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return null;
            }
            catch (NotSupportedException ex)
            {
                var errorMsg = $"Content type not supported: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return null;
            }
            catch (System.Text.Json.JsonException ex)
            {
                var errorMsg = $"Invalid JSON in response: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return null;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Unexpected error creating note: {ex.GetType().Name} - {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return null;
            }
        }

        public async Task<Note?> UpdateNoteAsync(Guid id, Note note)
        {
            try
            {
                var requestUri = $"{_httpClient.BaseAddress}api/Notes/{id}";
                _logger?.LogInformation($"Attempting to update note at: {requestUri}");
                
                // Ensure the note ID matches the URL parameter
                note.Id = id;
                
                var response = await _httpClient.PutAsJsonAsync($"api/Notes/{id}", note);
                
                if (response.IsSuccessStatusCode)
                {
                    // The API returns NoContent (204) for PUT operations, so we need to fetch the updated note
                    var updatedNote = await GetNoteByIdAsync(id);
                    _logger?.LogInformation($"Successfully updated note with ID: {id}");
                    return updatedNote;
                }
                else
                {
                    var errorMsg = $"Error updating note: {response.StatusCode} - {response.ReasonPhrase}";
                    _logger?.LogError(errorMsg);
                    Console.WriteLine(errorMsg);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                var errorMsg = $"HTTP Error updating note: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                
                if (ex.InnerException != null)
                {
                    var innerMsg = $"Inner exception: {ex.InnerException.Message}";
                    _logger?.LogError(innerMsg);
                    Console.WriteLine(innerMsg);
                }
                
                return null;
            }
            catch (TaskCanceledException ex)
            {
                var errorMsg = $"Request timeout updating note: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return null;
            }
            catch (NotSupportedException ex)
            {
                var errorMsg = $"Content type not supported: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return null;
            }
            catch (System.Text.Json.JsonException ex)
            {
                var errorMsg = $"Invalid JSON in response: {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return null;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Unexpected error updating note: {ex.GetType().Name} - {ex.Message}";
                _logger?.LogError(ex, errorMsg);
                Console.WriteLine(errorMsg);
                return null;
            }
        }
    }
}
