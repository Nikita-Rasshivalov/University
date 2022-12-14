using PersonalAPI.Models;

namespace PersonalAPI.Services
{
    public interface IPersonalService
    {
        Task<Personal> CreateAsync(Personal person);
        Task DeleteAsync(int id);
        Task<List<Personal>> GetAsync();
        Task<Personal?> GetAsync(int id);
        Task UpdateAsync(Personal person);
    }
}