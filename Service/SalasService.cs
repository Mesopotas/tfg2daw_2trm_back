using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Repositories;
using Cinema.Service;

namespace Cinema.Service
{
    public class SalasService : ISalasService
    {
        private readonly ISalasRepository _salasRepository;

        public SalasService(ISalasRepository salasRepository)
        {
            _salasRepository = salasRepository;
        }

        public async Task<List<Salas>> GetAllAsync()
        {
            return await _salasRepository.GetAllAsync();
        }
    }
}