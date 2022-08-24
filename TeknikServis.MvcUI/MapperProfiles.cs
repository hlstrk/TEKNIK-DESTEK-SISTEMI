using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknikServis.Entittes.Models;

namespace TeknikServis.MvcUI
{
   
public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TeknikView, Teknik>();
        }
    }
}
