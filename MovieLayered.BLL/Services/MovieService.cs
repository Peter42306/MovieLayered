using MovieLayered.BLL.DTO;
using MovieLayered.BLL.Interfaces;
using MovieLayered.DAL.Entities;
using MovieLayered.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using AutoMapper;
using System.Diagnostics.Eventing.Reader;

namespace MovieLayered.BLL.Services
{
    public class MovieService : IMovieService
    {
        IUnitOfWork DataBase {  get; set; }

        public MovieService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.
        public async Task<IEnumerable<MovieDTO>> GetAllMovies()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Movie, MovieDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Movie>, IEnumerable<MovieDTO>>(await DataBase.Movies.GetAll());
        }

        public async Task<MovieDTO> GetMovie(int id)
        {
            var movie = await DataBase.Movies.Get(id);

            if (movie == null)
            {
                throw new ValidationException("Неверное значение");
                //throw new ValidationException("Wrong movie!", "");
            }

            return new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Genre = movie.Genre,
                ReleaseYear = movie.ReleaseYear,
                PosterPath= movie.PosterPath,
                Description = movie.Description
            };

            //var movie = await DataBase.Players.Get(id);
            //if (player == null)
            //    throw new ValidationException("Wrong player!", "");
            //return new PlayerDTO
            //{
            //    Id = player.Id,
            //    Name = player.Name,
            //    Age = player.Age,
            //    Position = player.Position,
            //    TeamId = player.TeamId,
            //    Team = player.Team?.Name
            //};
        }

		public async Task<MovieDTO> GetMovie(string title)
		{
            var movie = await DataBase.Movies.Get(title);

			if (movie == null)
			{
				throw new ValidationException("Неверное значение");
				//throw new ValidationException("Wrong movie!", "");
			}

			return new MovieDTO
			{
				Id = movie.Id,
				Title = movie.Title,
				Director = movie.Director,
				Genre = movie.Genre,
				ReleaseYear = movie.ReleaseYear,
				PosterPath = movie.PosterPath,
				Description = movie.Description
			};

			//var movie = await DataBase.Players.Get(id);
			//if (player == null)
			//    throw new ValidationException("Wrong player!", "");
			//return new PlayerDTO
			//{
			//    Id = player.Id,
			//    Name = player.Name,
			//    Age = player.Age,
			//    Position = player.Position,
			//    TeamId = player.TeamId,
			//    Team = player.Team?.Name
			//};
		}

		public async Task CreateMovie(MovieDTO movieDto)
        {
            var movie = new Movie
            {
                Id=movieDto.Id,
                Title = movieDto.Title,
                Director = movieDto.Director,
                Genre = movieDto.Genre,
                ReleaseYear = movieDto.ReleaseYear,
                PosterPath = movieDto.PosterPath,
                Description = movieDto.Description
            };
            
            await DataBase.Movies.Create(movie);
            await DataBase.Save();            
        }

        public async Task UpdateMovie(MovieDTO movieDto)
        {
            var movie=await DataBase.Movies.Get(movieDto.Id);

            if (movie != null)
            {
                movie.Id = movieDto.Id;
                movie.Title = movieDto.Title;
                movie.Director = movieDto.Director;
                movie.Genre = movieDto.Genre;
                movie.ReleaseYear = movieDto.ReleaseYear;
                movie.PosterPath = movieDto.PosterPath;
                movie.Description = movieDto.Description;

				//DataBase.Movies.Update(movie);
				await DataBase.Save();
			}
            else
            {
				throw new ValidationException("Фильм не найден");
			}
            
            
        }

        public async Task DeleteMovie(int id)
        {
            var movie = await DataBase.Movies.Get(id);
            
            if (movie!=null)
            {
                DataBase.Movies.Delete(id);
            }

            //await DataBase.Movies.Delete(id);
            await DataBase.Save();

            //await DataBase.Movies.Delete(id);
            //await DataBase.Save();            
        }

        

        
    }
}
