using API.W.Movies.DAL.Models;
using API.W.Movies.DAL.Models.Dtos;
using API.W.Movies.Repository.IRepository;
using API.W.Movies.Services.IServices;
using AutoMapper;

namespace API.W.Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<bool> MovieExistsByIdAsync(int id)
        {
            return await _movieRepository.MovieExistsByIdAsync(id);
        }

        public async Task<bool> MovieExistsByNameAsync(string name)
        {
            return await _movieRepository.MovieExistsByNameAsync(name);
        }

        public async Task<MovieDto> CreateMovieAsync(MovieCreateUpdateDto movieCreateDto)
        {
            //Validar si la película ya existe
            var movieExists = await _movieRepository.MovieExistsByNameAsync(movieCreateDto.Name);

            if (movieExists)
            {
                throw new InvalidOperationException($"Ya existe una película con el nombre de '{movieCreateDto.Name}'");
            }

            //Mapear el DTO a la entidad
            var movie = _mapper.Map<Movie>(movieCreateDto);

            //Crear la película en el repositorio
            var movieCreated = await _movieRepository.CreateMovieAsync(movie);

            if (!movieCreated)
            {
                throw new Exception("Ocurrió un error al crear la película.");
            }

            //Mapear la entidad creada a DTO
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            //Verificar si la película existe
            var movieExists = await _movieRepository.GetMovieAsync(id);

            if (movieExists == null)
            {
                throw new InvalidOperationException($"No se encontró la película con ID: '{id}'");
            }

            //Eliminar la película del repositorio
            var movieDeleted = await _movieRepository.DeleteMovieAsync(id);

            if (!movieDeleted)
            {
                throw new Exception("Ocurrió un error al eliminar la película.");
            }

            return movieDeleted;
        }

        public async Task<ICollection<MovieDto>> GetMoviesAsync()
        {
            // Obtener las películas del repositorio
            var movies = await _movieRepository.GetMoviesAsync();

            // Mapear toda la colección de una vez
            return _mapper.Map<ICollection<MovieDto>>(movies);
        }

        public async Task<MovieDto> GetMovieAsync(int id)
        {
            // Obtener la película del repositorio
            var movie = await _movieRepository.GetMovieAsync(id);

            if (movie == null)
            {
                throw new InvalidOperationException($"No se encontró la película con ID: '{id}'");
            }

            // Mapear la entidad a DTO
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> UpdateMovieAsync(MovieCreateUpdateDto dto, int id)
        {
            //Validar si la película existe
            var movieExists = await _movieRepository.GetMovieAsync(id);

            if (movieExists == null)
            {
                throw new InvalidOperationException($"No se encontró la película con ID: '{id}'");
            }

            var nameExists = await _movieRepository.MovieExistsByNameAsync(dto.Name);

            if (nameExists && movieExists.Name != dto.Name)
            {
                throw new InvalidOperationException($"Ya existe una película con el nombre de '{dto.Name}'");
            }

            //Mapear el DTO a la entidad
            _mapper.Map(dto, movieExists);

            //Actualizamos la película en el repositorio
            var movieUpdated = await _movieRepository.UpdateMovieAsync(movieExists);

            if (!movieUpdated)
            {
                throw new Exception("Ocurrió un error al actualizar la película.");
            }

            //Retornar el DTO actualizado
            return _mapper.Map<MovieDto>(movieExists);
        }
    }
}
