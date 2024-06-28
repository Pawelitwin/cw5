using GakkoHorizontalSlice.Model;
using GakkoHorizontalSlice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using System.Data.SqlClient;

namespace GakkoHorizontalSlice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

      
        [HttpGet]
        public async Task<IActionResult> GetAnimals([FromQuery] string? orderBy = "name")
        {
            var animals = await _animalService.GetAnimalsAsync(orderBy);
            return Ok(animals);
        }

 
        [HttpPost]
        public async Task<IActionResult> AddAnimal([FromBody] Animal newAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _animalService.AddAnimalAsync(newAnimal);
            return CreatedAtAction(nameof(GetAnimals), new { id = newAnimal.Id }, newAnimal);
        }

   
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] Animal updatedAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _animalService.UpdateAnimalAsync(id, updatedAnimal);
            return NoContent();
        }

   
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            await _animalService.DeleteAnimalAsync(id);
            return NoContent();
        }
    }
}

