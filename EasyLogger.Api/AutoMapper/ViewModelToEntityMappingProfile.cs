using AutoMapper;
using EasyLogger.Api.Dtos.EasyLoggerProjectDto;
using EasyLogger.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.AutoMapper
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<EasyLoggerProjectListDto, EasyLoggerProject>();
            CreateMap<EasyLoggerProjectEditDto, EasyLoggerProject>();

        }
    }
}
