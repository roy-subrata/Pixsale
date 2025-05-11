
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Pixsale.Shared.Clients.Models;


namespace Pixsale.Shared.Clients;

public class UnitClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UnitClient> _logger;

    public UnitClient(
        ILogger<UnitClient> logger,
        HttpClient httpClient)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<Unit>> GetAllAsync()
    {
        try
        {
            var units = await _httpClient.GetFromJsonAsync<List<Unit>>("api/unit");
            return units!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching units {Message}", ex.Message);
            throw;
        }
    }

    public async Task<Unit?> GetByIdAsync(Guid Id)
    {
        try
        {
            var unit = await _httpClient.GetFromJsonAsync<Unit>($"api/unit/{Id}");
            return unit;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching unit with ID: {Id} {Message}", Id, ex.Message);
            throw;
        }
    }

    public async Task<Unit?> PostAsync(Unit unit)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/units", unit);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to create units. Status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var units = await response.Content.ReadFromJsonAsync<Unit>();
            if (units == null)
            {
                _logger.LogWarning("Response content was null when creating units.");
            }

            return units;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to create customer {message}", ex.Message);
            throw;
        }
    }

    public async Task<Unit?> PutAsync(Unit unit)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/unit/{unit.Id}", unit);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to update unit. Status code: {StatusCode}", response.StatusCode);
                return null;
            }
            return unit;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to update unit {message}", ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/unit/{Id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to delete unit. Status code: {StatusCode}", response.StatusCode);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to delete unit {message}", ex.Message);
            throw;
        }
    }
}