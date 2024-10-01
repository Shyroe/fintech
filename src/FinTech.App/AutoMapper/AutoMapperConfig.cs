using AutoMapper;
using FinTech.App.ViewModels;
using FinTech.Business.Models;

namespace FinTech.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<StatusNotaFiscal, StatusNotaFiscalViewModel>().ReverseMap();
        }
    }
}