using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyLogger.Api.Dtos;
using EasyLogger.Api.Dtos.EasyLoggerProjectDto;
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
        private readonly IMapper _mapper;

        public ProjectController(ISqlSugarRepository<EasyLoggerProject, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("GetEasyLoggerProjectAsync")]
        public async Task<PagedResultDto<EasyLoggerProjectListDto>> GetEasyLoggerProjectAsync(EasyLoggerProjectInput input)
        {
            // 定义返回参数
            var result = new PagedResultDto<EasyLoggerProjectListDto>();
            var total = 0;
            // 因为项目的数据存储在默认的数据库中所以这里不用做切换
            var sqlSugarClient = _repository.GetCurrentSqlSugar();

            var entityList = sqlSugarClient.Queryable<EasyLoggerProject>()
                  .OrderBy(s => s.Code, OrderByType.Desc)
                  .ToPageList(input.PageIndex, input.PageSize, ref total);


            result.List = _mapper.Map<List<EasyLoggerProjectListDto>>(entityList);
            result.Total = total;
            return result;
        }
        /// <summary>
        /// 添加修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task PostAsync(CreateOrUpdateEasyLoggerProjectInput input)
        {
            if (input.EasyLoggerProject.Id.HasValue)
            {
                await Update(input.EasyLoggerProject);
            }
            else
            {
                await Create(input.EasyLoggerProject);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual async Task Create(EasyLoggerProjectEditDto input)
        {
            var entity = _mapper.Map<EasyLoggerProject>(input);
            _repository.GetCurrentSqlSugar().Insertable(entity).ExecuteCommand();
        }
        /// <summary>
        /// 编辑
        /// </summary>
        protected virtual async Task Update(EasyLoggerProjectEditDto input)
        {
            if (input.Id != null)
            {
                var entity = _mapper.Map<EasyLoggerProject>(input);

                var sqlSugarClient = _repository.GetCurrentSqlSugar();
                sqlSugarClient.Updateable(entity)
                  .ExecuteCommand();

            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            _repository.GetCurrentSqlSugar().Deleteable<EasyLoggerProject>(new { Id = id }).ExecuteCommand();
        }

    }
}