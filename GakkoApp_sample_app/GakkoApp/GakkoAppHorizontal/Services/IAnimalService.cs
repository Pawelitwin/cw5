using GakkoHorizontalSlice.Model;

namespace GakkoHorizontalSlice.Services
{
    public interface IAnimalService
    {
        Task<IEnumerable<Animal>> GetAnimalsAsync(string orderBy);
        Task AddAnimalAsync(Animal animal);
        Task UpdateAnimalAsync(int id, Animal animal);
        Task DeleteAnimalAsync(int id);
    }
}
