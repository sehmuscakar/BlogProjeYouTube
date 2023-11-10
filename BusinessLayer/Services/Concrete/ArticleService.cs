using AutoMapper;
using BusinessLayer.Services.Abstractions;
using DataAccessLayer.Dtos.Articles;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWorks;

namespace BusinessLayer.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ArticleDto>> GetAllArticleWithCategoryNonDeletedAsync()
        {

             var articles= await _unitOfWork.GetRepository<Article>().GetAllAsyc(x=>!x.IsDeleted,x=>x.Category);
            var map = _mapper.Map<List<ArticleDto>>(articles);

            return map; 
        }
    }
}
