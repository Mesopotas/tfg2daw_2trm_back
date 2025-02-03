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
    }
}