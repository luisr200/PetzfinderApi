using System;
using System.Collections.Generic;
using Petzfinder.Data;
using Petzfinder.Model;
using System.Threading.Tasks;
using Petzfinder.Models;

namespace Petzfinder.Service
{
    public class PetService
    {
        public async Task<Pet> GetPetById(string key)
        {
            PetRepository _repo = new PetRepository();
            return await _repo.GetPetById(key);
        }

        public async Task<List<Pet>> GetAllUserPets(string email)
        {
            PetRepository _repo = new PetRepository();
            return await _repo.GetAllUserPets(email);
        }

        public async Task PutPet(Pet pet)
        {
            PetRepository _repo = new PetRepository();
            await _repo.PutPet(pet);
        }
    }
}
