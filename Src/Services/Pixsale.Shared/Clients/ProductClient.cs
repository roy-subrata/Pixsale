using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Pixsale.Shared.Clients.Models;

namespace Pixsale.Shared.Clients;

public class ProductClient
{

    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductClient> _logger;

    public ProductClient(
        ILogger<ProductClient> logger,
        HttpClient httpClient)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        try
        {
            var Products = await _httpClient.GetFromJsonAsync<List<Product>>("api/product");
            return Products!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching Products {Message}", ex.Message);
            throw;
        }
    }

    public async Task<Product?> GetByIdAsync(Guid Id)
    {
        try
        {
            var Product = await _httpClient.GetFromJsonAsync<Product>($"api/product/{Id}");
            return Product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching Product with ID: {Id} {Message}", Id, ex.Message);
            throw;
        }
    }

    public async Task<Product?> PostAsync(Product Product)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/product", Product);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to create Product. Status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
            if (createdProduct == null)
            {
                _logger.LogWarning("Response content was null when creating Product.");
            }

            return createdProduct;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to create Product {message}", ex.Message);
            throw;
        }
    }

    public async Task<Product?> PutAsync(Product Product)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/product/{Product.Id}", Product);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to update Product. Status code: {StatusCode}", response.StatusCode);
                return null;
            }
            return Product;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to update Product {message}", ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/product/{Id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to delete Product. Status code: {StatusCode}", response.StatusCode);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to delete Product {message}", ex.Message);
            throw;
        }
    }
}