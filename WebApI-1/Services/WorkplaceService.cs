using WebApI_1.DTOs;
using WebApI_1.Models;
using WebApI_1.Data;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace WebApI_1.Services
{
    public interface IWorkplaceService
    {
        Task<List<GetWorkplaceDto>> GetAllAsync();
        Task<GetWorkplaceDto?> GetByIdAsync(int id);
        Task<GetWorkplaceDto> CreateAsync(CreateWorkplaceDto dto);
        Task<bool> UpdateAsync(int id, CreateWorkplaceDto dto);
        Task<bool> DeleteAsync(int id);
        Task<string> GetWeatherAsync(string city);
    }

    public class WorkplaceService : IWorkplaceService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public WorkplaceService(AppDbContext context, IMapper mapper, HttpClient http, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _http = http;
            _config = config;
        }

        public async Task<List<GetWorkplaceDto>> GetAllAsync()
        {
            var entities = await _context.Workplaces.ToListAsync();
            return _mapper.Map<List<GetWorkplaceDto>>(entities);
        }

        public async Task<GetWorkplaceDto?> GetByIdAsync(int id)
        {
            var entity = await _context.Workplaces.FindAsync(id);
            return entity == null ? null : _mapper.Map<GetWorkplaceDto>(entity);
        }

        public async Task<GetWorkplaceDto> CreateAsync(CreateWorkplaceDto dto)
        {
            var entity = _mapper.Map<Workplace>(dto);
            _context.Workplaces.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetWorkplaceDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, CreateWorkplaceDto dto)
        {
            var entity = await _context.Workplaces.FindAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.Address = dto.Address;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Workplaces.FindAsync(id);
            if (entity == null) return false;

            _context.Workplaces.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> GetWeatherAsync(string city)
        {
            try
            {
                var url = $"{_config["WeatherApi:BaseUrl"]}/current.json?key={_config["WeatherApi:ApiKey"]}&q={city}";
                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return $"Väderfel: {response.StatusCode}";

                var json = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(json)!;
                return $"{data.current.temp_c}°C";
            }
            catch (Exception ex)
            {
                return $"Fel vid väderhämtning: {ex.Message}";
            }
        }
    }
}
