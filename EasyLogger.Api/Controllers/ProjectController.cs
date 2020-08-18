using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLogger.Api.Model;
using EasyLogger.DbStorage.Interface;
using EasyLogger.SqlSugarDbStorage;
using EasyLogger.SqlSugarDbStorage.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace EasyLogger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ISqlSugarRepository<EasyLoggerProject,int> _repository;

        public ProjectController(ISqlSugarRepository<EasyLoggerProject, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<List<EasyLoggerProject>> GetAsync() {

            _repository.Insert(new EasyLoggerProject()
            {
                Name = "TestName",
                Code = "TestCode"

            });

           var data =  _repository.GetQuery();

           return data;
        }

    }
}