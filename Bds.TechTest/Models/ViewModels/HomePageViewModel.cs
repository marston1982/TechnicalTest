﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bds.TechTest.Models.ViewModels
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            SearchResults = new List<SearchResultItem>();
            Errors = new List<string>();
        }

        [Display(Name = "Name")]
        public string SearchTerm { get; set; }

        public IEnumerable<SearchResultItem> SearchResults { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}