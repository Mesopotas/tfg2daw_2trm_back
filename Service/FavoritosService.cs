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
    public class FavoritosService : IFavoritosService
    {
        private readonly IFavoritosRepository _favoritosRepository;

        public FavoritosService(IFavoritosRepository favoritosRepository)
        {
            _favoritosRepository = favoritosRepository;
        }

        public async Task<List<FavoritosDTO>> GetAllAsync()
        {
            return await _favoritosRepository.GetAllAsync();
        }

        public async Task<List<FavoritosDTO>> GetByIdAsync(int id)
        {
            return await _favoritosRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Favoritos favorito)
        {
            await _favoritosRepository.AddAsync(favorito);
        }
    

public async Task DeleteAsync(int idUsuario, int idSala)
{
    var favorito = await _favoritosRepository.GetByIdAsync(idUsuario);
    
    if (favorito == null)
    {
        // return NotFound();
    }

    await _favoritosRepository.DeleteAsync(idUsuario, idSala);

    // return NoContent();
}



                }
}