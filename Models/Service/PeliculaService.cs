using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Controllers;
using Cinema.Repositories;
using Cinema.Service;
using Models;

namespace Cinema.Service

{ public class PeliculaService : IPeliculaService
    {
        private readonly IPeliculaRepository _peliculaRepository;

        public PeliculaService(IPeliculaRepository peliculaRepository)
        {
            _peliculaRepository = peliculaRepository;
        }
     

        public async Task<List<Peliculas>> GetAllAsync()
        {
            return await _peliculaRepository.GetAllAsync();
        }

        public async Task<Peliculas?> GetByIdAsync(int id)
        {
            return await _peliculaRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(Peliculas pelicula)
        {
            await _peliculaRepository.AddAsync(pelicula);
        }

        public async Task UpdateAsync( Peliculas pelicula)
        {
            await _peliculaRepository.UpdateAsync(pelicula);
        }

        public async Task DeleteAsync(int id)
        {
           await _peliculaRepository.GetByIdAsync(id);
           
           await _peliculaRepository.DeleteAsync(id);
           //return NoContent();
        }
        
        public async Task InicializarDatosAsync()
        {
        }

      
    }
}
