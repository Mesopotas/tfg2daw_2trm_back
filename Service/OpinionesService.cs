using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Controllers;
using Cinema.Repositories;
using Cinema.Service;
using Models;

namespace Cinema.Service

{ public class OpinionesService : IOpinionesService
    {
        private readonly IOpinionesRepository _opinionesRepository;

        public OpinionesService(IOpinionesRepository opinionesRepository)
        {
            _opinionesRepository = opinionesRepository;
        }
     

        public async Task<List<Opiniones>> GetAllAsync()
        {
            return await _opinionesRepository.GetAllAsync();
        }

        public async Task<Opiniones?> GetByIdAsync(int id)
        {
            return await _opinionesRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(Opiniones opiniones)
        {
            await _opinionesRepository.AddAsync(opiniones);
        }

        public async Task UpdateAsync( Opiniones opiniones)
        {
            await _opinionesRepository.UpdateAsync(opiniones);
        }

        public async Task DeleteAsync(int id)
        {
           await _opinionesRepository.GetByIdAsync(id);
           
           await _opinionesRepository.DeleteAsync(id);
           //return NoContent();
        }
        
        public async Task InicializarDatosAsync()
        {
        }

      
    }
}
