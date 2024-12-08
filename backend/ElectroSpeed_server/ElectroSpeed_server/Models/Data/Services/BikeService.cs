using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Recursos;

namespace ElectroSpeed_server.Models.Data.Services
{
    public class BikeService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly FiltroRecurso _filtroRecurso;

        public BikeService(UnitOfWork unitOfWork, FiltroRecurso filtroRecurso)
        {
            _unitOfWork = unitOfWork;
            _filtroRecurso = filtroRecurso;
        }

        public async Task<IEnumerable<Bicicletas>> GetAllBikesAsync()
        {
            return await _unitOfWork.BikeRepository.GetAllBikesAsync();
        }

        public async Task<Bicicletas> GetBikeByIdAsync(int id)
        {
            return await _unitOfWork.BikeRepository.GetBikeByIdAsync(id);
        }

        public async Task AddBikeAsync(Bicicletas bike)
        {
            await _unitOfWork.BikeRepository.AddBikeAsync(bike);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateBikeAsync(Bicicletas bike)
        {
            await _unitOfWork.BikeRepository.UpdateBikeAsync(bike);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteBikeAsync(int id)
        {
            await _unitOfWork.BikeRepository.DeleteBikeAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<BicisPaginas> FiltroBicisAsync(FiltroBicis model)
        {
            // Obtenemos todas las bicicletas desde el repositorio
            var bicicletas = await _unitOfWork.BikeRepository.GetAllBikesAsync();

            // Aplicamos el filtro, ordenación y paginación utilizando FiltroRecurso
            var bicicletasFiltradas = _filtroRecurso.Search(model.Consulta, bicicletas.ToList());
            var bicicletasOrdenadas = _filtroRecurso.Order(model, bicicletasFiltradas);
            return _filtroRecurso.Pages(model, bicicletasOrdenadas);
        }
    }
}
