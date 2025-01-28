using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Controllers;
using Cinema.Repositories;
using Cinema.Service;
using Models;

namespace Cinema.Service
{
    public class FechasHorasService : IFechasHorasService
    {
        private readonly IFechasHorasRepository _fechasRepository;

        public FechasHorasService(IFechasHorasRepository fechasHorasRepository)
        {
            _fechasRepository = fechasHorasRepository;
        }

        public async Task<List<FechasHoras>> GetAllAsync()
        {
            return await _fechasRepository.GetAllAsync();
        }

        public async Task<FechasHoras?> GetByIdAsync(int id)
        {
            return await _fechasRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(FechasHoras fechas)
        {
            await _fechasRepository.AddAsync(fechas);
        }

        public async Task UpdateAsync(FechasHoras fechas)
        {
            await _fechasRepository.UpdateAsync(fechas);
        }

        public async Task DeleteAsync(int id)
        {
            await _fechasRepository.GetByIdAsync(id);
            await _fechasRepository.DeleteAsync(id);
        }

        public async Task InicializarDatosAsync()
        {
        }
    }
}