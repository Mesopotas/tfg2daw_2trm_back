using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Controllers;
using Cinema.Repositories;
using Cinema.Service;
using Models;

namespace Cinema.Service
{
    public class AsientosService : IAsientosService
    {
        private readonly IAsientosRepository _asientosRepository;

        public AsientosService(IAsientosRepository asientosRepository)
        {
            _asientosRepository = asientosRepository;
        }

        public async Task<List<Asientos>> GetAllAsync()
        {
            return await _asientosRepository.GetAllAsync();
        }

        public async Task<Asientos?> GetByIdAsync(int idSala, int id)
        {
            return await _asientosRepository.GetByIdAsync(idSala, id);
        }

        public async Task<Asientos?> GetByIdSalasAsync(int id)
        {
            return await _asientosRepository.GetByIdSalasAsync(id);
        }

        public async Task UpdateAsync(Asientos asientos)
        {
            await _asientosRepository.UpdateAsync(asientos);
        }

    }
}