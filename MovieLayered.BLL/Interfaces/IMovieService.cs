using MovieLayered.BLL.DTO;

namespace MovieLayered.BLL.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDTO>> GetAllMovies();
        Task<MovieDTO> GetMovie(int id);
		Task<MovieDTO> GetMovie(string title);
		Task CreateMovie(MovieDTO movieDto);
        Task UpdateMovie(MovieDTO movieDto);
        Task DeleteMovie(int id);        
    }
}
