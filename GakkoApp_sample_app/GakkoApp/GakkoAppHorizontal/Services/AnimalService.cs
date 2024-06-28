using GakkoHorizontalSlice.Model;
using GakkoHorizontalSlice.Repositories;

namespace GakkoHorizontalSlice.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public Task<IEnumerable<Animal>> GetAnimalsAsync(string orderBy)
        {
            return _animalRepository.GetAnimalsAsync(orderBy);
        }

        public Task AddAnimalAsync(Animal animal)
        {
            return _animalRepository.AddAnimalAsync(animal);
        }

        public Task UpdateAnimalAsync(int id, Animal animal)
        {
            return _animalRepository.UpdateAnimalAsync(id, animal);
        }

        public Task DeleteAnimalAsync(int id)
        {
            return _animalRepository.DeleteAnimalAsync(id);
        }
    }
}
