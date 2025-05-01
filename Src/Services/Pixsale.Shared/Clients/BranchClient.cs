using System.Text.Json;
using Pixsale.Shared.Clients.Models;

namespace Pixsale.Shared.Clients;


public class BranchClient
{
    private readonly HttpClient _httpClient;
    public BranchClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Branch>> GetAllBranchAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/branch");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var branches = JsonSerializer.Deserialize<List<Branch>>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return branches!;
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<Branch> GetBranchById(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/branche/{id}");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var branch = JsonSerializer.Deserialize<Branch>(jsonString);
            return branch!;
        }
        catch (System.Exception)
        {

            throw;
        }

    }

}