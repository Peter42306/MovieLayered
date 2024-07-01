using Microsoft.EntityFrameworkCore;
using MovieLayered.BLL.Infrastructure;
using MovieLayered.BLL.Interfaces;
using MovieLayered.BLL.Services;
//using MovieLayered.DAL.EF;



var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� �������� ApplicationContext � �������� ������� � ����������
//builder.Services.AddDbContext<StudentContext>(options => options.UseSqlServer(connection));
//builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(connection));

builder.Services.AddMovieContext(connection);

builder.Services.AddUnitOfWorkService();

builder.Services.AddTransient<IMovieService, MovieService>();

// ��������� ������� MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

//app.UseRouting();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Movie}/{action=Index}/{id?}");

app.Run();
		