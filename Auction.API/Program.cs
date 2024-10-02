using Auction.API.Extensions;
using Auction.Application.Services;
using Auction.DataAccess;
using Auction.DataAccess.Repositories;
using Auction.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Auction.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
            var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
            builder.Services.AddApiAuthentication(Options.Create(jwtOptions!));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AuctionDbContext>(
                options =>
                {
                    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(AuctionDbContext)));
                });

            builder.Services.AddScoped<ITangerinesService, TangerinesService>();
            builder.Services.AddScoped<ITangerinesRepository, TangerinesRepository>();

            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUsersService, UsersService>();

            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();

            builder.Services.AddSingleton<IBetSingletonService, BetSingletonService>();

            builder.Services.AddHostedService<TangerineHostedService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            var a = 0;
            var timer = new System.Threading.Timer( (e) =>
            { Console.WriteLine(a); a++; }, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }
    }
}
