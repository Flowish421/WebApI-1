using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using WebApI_1.Data;
using WebApI_1.Models;

// MODEL
public class Workplace
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}

// DTOs
public class WorkplaceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
}
public class CreateWorkplaceDto
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
}

// Weather DTO
public class WeatherResult
{
    public CurrentWeather current { get; set; } = null!;
    public class CurrentWeather
    {
        public double temp_c { get; set; }
        public string condition { get; set; } = null!;
    }
}

// SERVICE INTERFACE
public interface IWorkplaceService
{
    Task<List<WorkplaceDto>> GetAllAsync();
    Task<WorkplaceDto?> GetByIdAsync(int id);
    Task<WorkplaceDto> CreateAsync(CreateWorkplaceDto dto);
    Task<bool> UpdateAsync(int id, CreateWorkplaceDto dto);
    Task<bool> DeleteAsync(int id);
    Task<string> GetWeatherAsync(string city);
}

// SERVICE IMPLEMENTATION
public class WorkplaceService : IWorkplaceService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly HttpClient _http;

    public WorkplaceService(AppDbContext context, IMapper mapper, HttpClient http)
    {
        _context = context;
        _mapper = mapper;
        _http = http;
    }

    public async Task<List<WorkplaceDto>> GetAllAsync()
    {
        var workplaces = await _context.Workplaces.ToListAsync();
        return _mapper.Map<List<WorkplaceDto>>(workplaces);
    }

    public async Task<WorkplaceDto?> GetByIdAsync(int id)
    {
        var workplace = await _context.Workplaces.FindAsync(id);
        return workplace == null ? null : _mapper.Map<WorkplaceDto>(workplace);
    }

    public async Task<WorkplaceDto> CreateAsync(CreateWorkplaceDto dto)
    {
        var workplace = _mapper.Map<Workplace>(dto);
        _context.Workplaces.Add(workplace);
        await _context.SaveChangesAsync();
        return _mapper.Map<WorkplaceDto>(workplace);
    }

    public async Task<bool> UpdateAsync(int id, CreateWorkplaceDto dto)
    {
        var workplace = await _context.Workplaces.FindAsync(id);
        if (workplace == null) return false;

        _mapper.Map(dto, workplace);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var workplace = await _context.Workplaces.FindAsync(id);
        if (workplace == null) return false;

        _context.Workplaces.Remove(workplace);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string> GetWeatherAsync(string city)
    {
        var result = await _http.GetFromJsonAsync<WeatherResult>($"https://api.weatherapi.com/v1/current.json?key=demo&q={city}&aqi=no");
        return result == null ? "Ingen data" : $"{result.current.temp_c}°C";
    }
}
