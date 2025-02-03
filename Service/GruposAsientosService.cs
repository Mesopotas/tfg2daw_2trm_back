using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Controllers;
using Cinema.Repositories;
using Cinema.Service;
using Models;

namespace Cinema.Service
{
    public class GruposAsientosService : IGruposAsientosService
    {
        private readonly IGruposAsientosRepository _gruposAsientosRepository;

        public GruposAsientosService(IGruposAsientosRepository gruposAsientosRepository)
        {
            _gruposAsientosRepository = gruposAsientosRepository;
        }

        public async Task<List<GruposAsientos>> GetAllAsync()
        {
            return await _gruposAsientosRepository.GetAllAsync();
        }

        
        public async Task<GruposAsientos?> GetByIdAsync(int id)
        {
            return await _gruposAsientosRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(GruposAsientos gruposAsientos)
        {
            await _gruposAsientosRepository.AddAsync(gruposAsientos);
        }

        public async Task UpdateAsync(GruposAsientos gruposAsientos)
        {
            await _gruposAsientosRepository.UpdateAsync(gruposAsientos);
        }

        public async Task DeleteAsync(int id)
        {
            await _gruposAsientosRepository.GetByIdAsync(id);
            await _gruposAsientosRepository.DeleteAsync(id);
        }

    }
}