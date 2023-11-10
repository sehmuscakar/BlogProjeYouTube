﻿using DataAccessLayer.Dtos.Articles;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
   public interface IArticleService
    {
        Task<List<ArticleDto>> GetAllArticleWithCategoryNonDeletedAsync();
    }
}
