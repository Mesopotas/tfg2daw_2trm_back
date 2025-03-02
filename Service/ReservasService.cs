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
    public class ReservasService : IReservasService
    {
        private readonly IReservasRepository _reservasRepository;

        public ReservasService(IReservasRepository reservasRepository)
        {
            _reservasRepository = reservasRepository;
        }

        public async Task<List<Reservas>> GetAllAsync()
        {
            return await _reservasRepository.GetAllAsync();
        }

        public async Task<Reservas?> GetByIdAsync(int id)
        {
            return await _reservasRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Reservas reserva)
        {
            await _reservasRepository.AddAsync(reserva);
        }

        public async Task UpdateAsync(Reservas reserva)
        {
            await _reservasRepository.UpdateAsync(reserva);
        }

        public async Task DeleteAsync(int id)
        {
           var reserva = await _reservasRepository.GetByIdAsync(id);
           if (reserva == null)
           {
               //return NotFound();
           }
           await _reservasRepository.DeleteAsync(id);
           //return NoContent();
        }


                }
}