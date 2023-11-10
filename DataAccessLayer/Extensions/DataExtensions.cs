using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Abstractions;
using DataAccessLayer.Repositories.Concretes;
using DataAccessLayer.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));//generic olarak scoplarmızı böyle tanımlıyoruz tek seferde yaptık extensionsumuzu
            services.AddDbContext<ProjeContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnetion"))); // böyle daha sağlıklı

           services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;

        }
    }
}
