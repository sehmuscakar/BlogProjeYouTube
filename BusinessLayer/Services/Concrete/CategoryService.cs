using AutoMapper;
using BusinessLayer.Services.Abstractions;
using DataAccessLayer.Dtos.Categories;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesNonDeleted()
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsyc(x => x.IsDeleted);
            var map = _mapper.Map<List<CategoryDto>>(categories);

            return map; 
        }
    }
}
