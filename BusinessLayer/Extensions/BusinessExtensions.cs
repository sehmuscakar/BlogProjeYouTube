using BusinessLayer.Services.Abstractions;
using BusinessLayer.Services.Concrete;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Abstractions;
using DataAccessLayer.Repositories.Concretes;
using DataAccessLayer.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Extensions
{
   public static class BusinessExtensions
    {
        public static IServiceCollection LoadBusinessLayerExtension(this IServiceCollection services) // burda confige ihtiyacımız yok 
        {
            var assembly = Assembly.GetExecutingAssembly();


            services.AddScoped<IArticleService, ArticleService>();
            //   services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddAutoMapper(assembly);// assembly ;automapper ın sisteme add lendi yani eklendiği ,bu ksıım auto mapperın dependis ejenşın olan kütüphanesi ile oluyor ona dikket et.
            return services; 

        }

    }
}
