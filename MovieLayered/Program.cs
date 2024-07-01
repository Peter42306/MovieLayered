using Microsoft.EntityFrameworkCore;
using MovieLayered.BLL.Infrastructure;
using MovieLayered.BLL.Interfaces;
using MovieLayered.BLL.Services;
//using MovieLayered.DAL.EF;



var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
//builder.Services.AddDbContext<StudentContext>(options => options.UseSqlServer(connection));
//builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(connection));

builder.Services.AddMovieContext(connection);

builder.Services.AddUnitOfWorkService();

builder.Services.AddTransient<IMovieService, MovieService>();

// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

//app.UseRouting();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Movie}/{action=Index}/{id?}");

app.Run();
		