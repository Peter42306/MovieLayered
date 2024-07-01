using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieLayered.BLL.DTO;
using MovieLayered.BLL.Interfaces;
using MovieLayered.DAL.Entities;
using MovieLayered.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace MovieLayered.Controllers
{
    public class MovieController:Controller
    {
        //private readonly IRepository<Movie> _repository;

        private readonly IMovieService _movieService;
        private readonly IWebHostEnvironment _webHostEnvironment;        

        public MovieController(IMovieService movieService,IWebHostEnvironment webHostEnvironment)
        {
            _movieService= movieService;
            _webHostEnvironment = webHostEnvironment;
        }

		///////////////////////////////////////////////////////////////////////////////////////////

		// GET запрос для отображения всех фильмов в списке
		public async Task<IActionResult> Index()
		{
			//var model = await _repository.GetAll();

            var model=await _movieService.GetMovies();
			return View(model);

			//return View(await _movieContext.Movies.ToArrayAsync());
		}

        ///////////////////////////////////////////////////////////////////////////////////////////

        // GET запрос для отображения деталей конкретного выбранного фильма

        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id==null)
                {
                    return NotFound();
                }

                MovieDTO movie = await _movieService.GetMovie((int)id);
                return View(movie);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }

			////////////////////////////////////////////
            // работало в предыдущем задании, на репозиторий
            // 			
            //if (id == null || await _repository.GetAll() == null)
            //{
            //    return NotFound();
            //}

            //var movie = await _repository.GetById((int)id);

            //if (movie == null)
            //{
            //    return NotFound();
            //}

            //return View(movie);

            ////////////////////////////////////////////

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var movie = await _movieContext.Movies.FirstOrDefaultAsync(item => item.Id == id);

            //if (movie == null)
            //{
            //    return NotFound();
            //}

            //return View(movie);
        }

		///////////////////////////////////////////////////////////////////////////////////////////

		// GET запрос для отображения формы создания нового фильма
		[HttpGet]
		public async Task< IActionResult> Create()
		{
            ViewBag.ListMovies = new SelectList(await _movieService.GetMovies(), "Id", "Title");
			return View();
		}

		// POST запрос для создания нового фильма в списке
		// Bind - инициализация полей объекта, происходит напрямую через форму html
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,Director,Genre,ReleaseYear,Description")] MovieDTO movie, IFormFile uploadedFile)
		{
			if (ModelState.IsValid)
			{
				await _movieService.CreateMovie(movie);
				return View("~/Views/Player/Index.cshtml", await _movieService.GetMovies());
			}

			ViewBag.ListTeams = new SelectList(await _movieService.GetMovies(), "Id", "Name");
			return View(movie);


			//if (ModelState.IsValid)
			//{
			//	if (uploadedFile != null && uploadedFile.Length > 0)
			//	{
			//		movie.PosterPath = await UploadPicture(uploadedFile);
			//	}

			//	// Добавляем фильм в репозиторий
			//	await _repository.Create(movie);

			//	// Сохраняем изменения в базе данных
			//	await _repository.Save();

			//	// Перенаправляем на страницу списка фильмов
			//	return RedirectToAction(nameof(Index));
			//}

			//// Если модель не валидна, возвращаем представление с текущими данными фильма
			//return View(movie);
		}

		///////////////////////////////////////////////////////////////////////////////////////////
		
		// GET запрос на отображение формы редактирования фильма
		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			try
			{
				if (id == null)
				{
					return NotFound();
				}
				MovieDTO movie = await _movieService.GetMovie((int)id);
				return View(movie);
			}
			catch (ValidationException ex)
			{
				return NotFound(ex.Message);
			}
		}


		// GET запрос на редактирования фильма
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(MovieDTO movie)
		{
			if (ModelState.IsValid)
			{
				await _movieService.UpdateMovie(movie);
				return View("~/Views/Player/Index.cshtml", await _movieService.GetMovies());
			}
			return View(movie);
		}

		///////////////////////////////////////////////////////////////////////////////////////////

		// GET запрос формы для удаления фильма
		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			try
			{
				if (id == null)
				{
					return NotFound();
				}
				MovieDTO movie = await _movieService.GetMovie((int)id);
				return View(movie);
			}
			catch (ValidationException ex)
			{
				return NotFound(ex.Message);
			}
		}

		// POST запрос для удаления фильма
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _movieService.DeleteMovie(id);
			return View("~/Views/Player/Index.cshtml", await _movieService.GetMovies());
		}


		///////////////////////////////////////////////////////////////////////////////////////////



		///////////////////////////////////////////////////////////////////////////////////////////

		// Вспомогательный метод для проверки существования фильма
		private async Task<bool> MovieExists(int id)
		{
			var movie = await _movieService.GetMovie(id);
			return movie != null;

			//List<Movie> list = await _repository.GetAll();
			//return (list?.Any(m => m.Id == id)).GetValueOrDefault();

			//return _movieContext.Movies.Any(item => item.Id == id);
		}

		///////////////////////////////////////////////////////////////////////////////////////////

		// Вспомогательный метод для загрузки картинок, с уменьшением размера фото
		private async Task<string> UploadPicture(IFormFile uploadedFile)
		{
			// Путь к папке, где будут храниться изображения
			string uploadedFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Image");

			// Генерируем новое уникальное имя файла для изображения
			string newFileNameGenerated = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;

			// Полный путь к файлу на сервере
			string filePath = Path.Combine(uploadedFolder, newFileNameGenerated);

			// Сохраняем файл на сервере
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				// Load the image
				using (var image = await Image.LoadAsync(uploadedFile.OpenReadStream()))
				{
					// Resize the image to a maximum width and height of 800px
					image.Mutate(x => x.Resize(new ResizeOptions
					{
						Mode = ResizeMode.Max,
						Size = new Size(600, 600)
						//Size = new Size(image.Width, image.Height)
					}));

					await image.SaveAsPngAsync(fileStream);
				}
			}
			// Устанавливаем путь к изображению в объекте фильма
			return "/Image/" + newFileNameGenerated;
		}
	}
}
