﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentionTestTask.Domain.DTOs.Categories
{
    public class UpdateCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
