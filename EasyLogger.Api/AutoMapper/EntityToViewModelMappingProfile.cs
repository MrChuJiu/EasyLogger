using AutoMapper;
using EasyLogger.Api.Dtos.EasyLoggerProjectDto;
using EasyLogger.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.AutoMapper
{
    public class EntityToViewModelMappingProfile : Profile
    {
        public EntityToViewModelMappingProfile()
        {
            CreateMap<EasyLoggerProject, EasyLoggerProjectListDto>();
            CreateMap<EasyLoggerProject, EasyLoggerProjectEditDto>();
        }
    }
}
