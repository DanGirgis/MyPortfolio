using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolio.ViewModels
{
    public class HomeViewModel
    {
        public Owner owner { get; set; }
        public List<ProtflioItem> protflioItems { get; set; }
    }
}
