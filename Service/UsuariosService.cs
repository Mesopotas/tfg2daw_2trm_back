using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoWorking.Repositories;
using CoWorking.Service;

namespace CoWorking.Service
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosService(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        public async Task<List<Usuarios>> GetAllAsync()
        {
            return await _usuariosRepository.GetAllAsync();
        }

        public async Task<Usuarios?> GetByIdAsync(int id)
        {
            return await _usuariosRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Usuarios usuario)
        {
            await _usuariosRepository.AddAsync(usuario);
        }

        public async Task UpdateAsync(Usuarios usuario)
        {
            await _usuariosRepository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
           var usuario = await _usuariosRepository.GetByIdAsync(id);
           if (usuario == null)
           {
               //return NotFound();
           }
           await _usuariosRepository.DeleteAsync(id);
           //return NoContent();
        }

        public async Task InicializarDatosAsync()
        {


        }

    }
}