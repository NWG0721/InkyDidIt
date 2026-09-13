using InkyDidIt.BLL.Services;
using InkyDidIt.Core.Interfaces.Repositories;
using InkyDidIt.Core.Interfaces.Services;
using InkyDidIt.DAL.DbContexts;
using InkyDidIt.DAL.Repository;
using InkyDidIt.DAL.UnitOfWork;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InkyDidIt.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();

			//builder.Services.AddScoped<IUserRoleService, UserRoleService>();
			//builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			var valkeyPassword = builder.Configuration["VALKEY_PASSWORD"];
			builder.Services.AddSingleton<IConnectionMultiplexer>(
				ConnectionMultiplexer.Connect($"localhost:6969,password={valkeyPassword},abortConnect=false"));

			builder.Services.AddDbContext<InkyDidItDbContext>(options =>
				options.UseNpgsql(builder.Configuration.GetConnectionString("InkyDidItConString")));
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowFrontend", policy =>
					policy.WithOrigins("http://localhost:8585", "http://192.168.1.9:8585")
						  .AllowAnyHeader()
						  .AllowAnyMethod().AllowCredentials());
			});
			//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			//{
			//	options.TokenValidationParameters = new TokenValidationParameters
			//	{
			//		ValidateIssuer = true,
			//		ValidateAudience = true,
			//		ValidateLifetime = true,
			//		ValidateIssuerSigningKey = true,
			//		ValidIssuer = builder.Configuration["JWT_ISSUER"],
			//		ValidAudience = builder.Configuration["JWT_AUDIENCE"],
			//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_KEY"]))
			//	};
			//});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
