using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyLogger.Api.AutoMapper;
using EasyLogger.Api.EasyTools;
using EasyLogger.DbStorage.Interface;
using EasyLogger.SqlSugarDbStorage;
using EasyLogger.SqlSugarDbStorage.Impl;
using EasyLogger.SqlSugarDbStorage.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SqlSugar;
using EasyLogger.SqlSugarDbStorage;
using SqlSugarProvider = EasyLogger.SqlSugarDbStorage.Impl.SqlSugarProvider;
using EasyLogger.Model;

namespace EasyLogger.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string SwaggerName = "v1";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region AutoMapper
            services.AddAutoMapper(typeof(EntityToViewModelMappingProfile), typeof(ViewModelToEntityMappingProfile));
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SwaggerName, new OpenApiInfo { Title = "EasyLogger", Description = "日志记录教程", Version = "v1" });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "EasyLogger.Api.xml");
                c.IncludeXmlComments(xmlPath, true);
            });
            #endregion

            #region SqlSugar
            // 改造一下把 自己的注入部分封装起来
            var defaultDbPath = Path.Combine(PathExtenstions.GetApplicationCurrentPath(), $"{Configuration["EasyLogger:DbName"]}.db");
            services.AddSqlSugarDbStorage(new SqlSugarSetting()
            {
                Name = SqlSugarDbStorageConsts.DefaultProviderName,
                ConnectionString = @$"Data Source={defaultDbPath}",
                DatabaseType = DbType.Sqlite,
                LogExecuting = (sql, pars) =>
                {
                    Console.WriteLine($"sql:{sql}");
                }
            });
            #endregion

            #region 默认创建基础数据库 和  时间数据库

            if (!File.Exists(defaultDbPath))
            {
                var partition = services.BuildServiceProvider().GetService<IPartitionDbTableFactory>();
                partition.DbTableCreate(defaultDbPath, true);
            }

            var startUpDbPath = Path.Combine(PathExtenstions.GetApplicationCurrentPath(), $"{Configuration["EasyLogger:DbName"]}-{DateTime.Now.ToString("yyyy-MM")}.db");
            if (!File.Exists(startUpDbPath))
            {
                var partition = services.BuildServiceProvider().GetService<IPartitionDbTableFactory>();
                partition.DbTableCreate(startUpDbPath, false);
            }

            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            #region Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{SwaggerName}/swagger.json", SwaggerName);
                c.RoutePrefix = string.Empty;
            });
            #endregion


            app.UseRouting();

            app.UseAuthorization();

            //app.Use(async (context, next) =>
            //{
            //    var sqlStorage = app.ApplicationServices.GetService<ISqlSugarProviderStorage>();
            //    var sugarClient = sqlStorage.GetByName(null, SqlSugarDbStorageConsts.DefaultProviderName).Sugar;
            //    Console.WriteLine("查看sugarClient");
            //});


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
