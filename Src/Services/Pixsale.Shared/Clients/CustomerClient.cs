using System.Text.Json;
using Pixsale.Shared.Clients.Models;

namespace Pixsale.Shared.Clients;

public class CustomerClient
{

    private readonly HttpClient _httpClient;

    public CustomerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<Customer>> GetAllCustomerAsync()
    {
        var response = await _httpClient.GetAsync("api/customer");
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var customers = JsonSerializer.Deserialize<List<Customer>>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return customers!;
    }

    public async Task<Customer> GetByIdAsync(Guid Id)
    {
        var response = await _httpClient.GetAsync("api/customer");
        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var customer = JsonSerializer.Deserialize<Customer>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return customer!;
    }

    public async Task<Customer> Create(Customer customer)
    {
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(customer),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync("api/customer", jsonContent);
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        var createdCustomer = JsonSerializer.Deserialize<Customer>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return createdCustomer!;
    }
}