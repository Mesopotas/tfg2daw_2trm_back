using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoWorking.Repositories;
using CoWorking.DTO;
using CoWorking.Service;

namespace CoWorking.Service
{
    public class OpinionesAsientosService : IOpinionesAsientosService
    {
        private readonly IOpinionesAsientosRepository _opinionesAsientosRepository;

        public OpinionesAsientosService(IOpinionesAsientosRepository opinionesAsientosRepository)
        {
            _opinionesAsientosRepository = opinionesAsientosRepository;
        }

        public async Task<List<OpinionesAsientos>> GetAllAsync()
        {
            return await _opinionesAsientosRepository.GetAllAsync();
        }

        public async Task<OpinionesAsientos?> GetByIdAsync(int id)
        {
            return await _opinionesAsientosRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(OpinionesAsientos opinionesAsiento)
        {
            await _opinionesAsientosRepository.AddAsync(opinionesAsiento);
        }

        public async Task UpdateAsync(OpinionesAsientos opinionesAsiento)
        {
            await _opinionesAsientosRepository.UpdateAsync(opinionesAsiento);
        }

        public async Task DeleteAsync(int id)
        {
           var opinionesAsiento = await _opinionesAsientosRepository.GetByIdAsync(id);
           if (opinionesAsiento == null)
           {
               //return NotFound();
           }
           await _opinionesAsientosRepository.DeleteAsync(id);
           //return NoContent();
        }


                }
}