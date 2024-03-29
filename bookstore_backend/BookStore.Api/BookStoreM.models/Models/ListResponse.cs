﻿using BookStoreM.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreM.models.Models
{
    public class ListResponse<T> where T : class
    {
        public IEnumerable<T> Results { get; set; }
        public int TotalRecords { get; set; }
    }
}
