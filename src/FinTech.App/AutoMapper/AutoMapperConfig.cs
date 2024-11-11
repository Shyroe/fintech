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
            CreateMap<NotaFiscalViewModel, NotaFiscal>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}