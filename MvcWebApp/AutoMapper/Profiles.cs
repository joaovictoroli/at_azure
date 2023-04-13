using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MvcWebApp.Entities;
using MvcWebApp.Models.ViewModels;

namespace MvcWebApp.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Friend, UserViewModel>().ReverseMap(); ;
        }
    }
}
