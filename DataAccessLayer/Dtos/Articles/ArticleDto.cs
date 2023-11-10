﻿using DataAccessLayer.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos.Articles
{
    public class ArticleDto
    {

        public Guid Id { get; set; }
        public string Title { get; set; }

        public CategoryDto Category { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}