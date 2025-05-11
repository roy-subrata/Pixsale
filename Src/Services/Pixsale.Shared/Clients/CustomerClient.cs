using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Pixsale.Shared.Clients.Models;

namespace Pixsale.Shared.Clients;

public class CustomerClient
{

    private readonly HttpClient _httpClient;
    private readonly ILogger<CustomerClient> _logger;

    public CustomerClient(
        ILogger<CustomerClient> logger,
        HttpClient httpClient)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        try
        {
            var customers = await _httpClient.GetFromJsonAsync<List<Customer>>("api/customer");
            return customers!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching customers {Message}", ex.Message);
            throw;
        }
    }

    public async Task<Customer?> GetByIdAsync(Guid Id)
    {
        try
        {
            var customer = await _httpClient.GetFromJsonAsync<Customer>($"api/customer/{Id}");
            return customer;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching customer with ID: {Id} {Message}", Id, ex.Message);
            throw;
        }
    }

    public async Task<Customer?> PostAsync(Customer customer)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/customer", customer);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to create customer. Status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var createdCustomer = await response.Content.ReadFromJsonAsync<Customer>();
            if (createdCustomer == null)
            {
                _logger.LogWarning("Response content was null when creating customer.");
            }

            return createdCustomer;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to create customer {message}", ex.Message);
            throw;
        }
    }

    public async Task<Customer?> PutAsync(Customer customer)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customer/{customer.Id}", customer);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to update customer. Status code: {StatusCode}", response.StatusCode);
                return null;
            }
            return customer;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to update customer {message}", ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/customer/{Id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to delete customer. Status code: {StatusCode}", response.StatusCode);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to delete customer {message}", ex.Message);
            throw;
        }
    }
}