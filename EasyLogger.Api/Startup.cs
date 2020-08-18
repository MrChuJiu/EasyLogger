using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyLogger.Api.AutoMapper;
using EasyLogger.Api.EasyTools;
using EasyLogger.Api.Model;
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
using SqlSugarProvider = EasyLogger.SqlSugarDbStorage.Impl.SqlSugarProvider;

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
            var defaultDbPath = Path.Combine(PathExtenstions.GetApplicationCurrentPath(), $"{Configuration["EasyLogger:DbName"]}.db");

            services.AddSingleton<ISqlSugarProvider>(new SqlSugarProvider(new SqlSugarSetting()
            {

                Name = SqlSugarDbStorageConsts.DefaultProviderName,
                ConnectionString = @$"Data Source={defaultDbPath}",
                DatabaseType = DbType.Sqlite,
                LogExecuting = (sql, pars) =>
                {
                    Console.WriteLine($"sql:{sql}");
                }

            }));
   
            services.AddTransient(typeof(ISqlSugarRepository<,>), typeof(SqlSugarRepository<,>));
            services.AddTransient(typeof(IDbRepository<,>), typeof(SqlSugarRepository<,>));
            services.AddSingleton<ISqlSugarProviderStorage, DefaultSqlSugarProviderStorage>();
            #endregion

            #region 默认创建基础数据库 和  时间数据库

            if (!File.Exists(defaultDbPath))
            {
                var db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = @$"Data Source={defaultDbPath}",
                    DbType = DbType.Sqlite,
                    IsAutoCloseConnection = true, // 自动释放数据务，如果存在事务，在事务结束后释放
                    InitKeyType = InitKeyType.Attribute// 从实体特性中读取主键自增列信息
                });

                db.CodeFirst.BackupTable().InitTables<EasyLoggerProject>();

                db.Dispose();
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
