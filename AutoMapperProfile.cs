using AutoMapper;
using Data.Dtos;
using Data.Models;

namespace MyCycleAPI
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<FamilyMemberDto,FamilyMember>().ReverseMap();

        }
    }
}
