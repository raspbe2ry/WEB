﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebProjekat2.Data;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using WebProjekat2.DIExtensions;
using Ninject;
using Ninject.Web.Common;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using WebProjekat2.IBL;
using WebProjekat2.BL;
using WebProjekat2.MvcControllers;
using WebProjekat2.Controllers;
using WebProjekat2.Models;
using WebProjekat2.ApiControllers;

namespace WebProjekat2
{
    public class Startup
    {
        private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();
        private IKernel Kernel { get; set; }

        private object Resolve(Type type) => Kernel.Get(type);
        private Scope RequestScope(IContext context) => scopeProvider.Value;

        private sealed class Scope : DisposableObject { }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<KarateContext>(options=> options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("test",
                builder =>
                {
                    builder.WithOrigins("http://localhost:8081").AllowAnyHeader()
                                .AllowAnyMethod(); 
                });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope());
            services.AddCustomControllerActivation(Resolve);
            services.AddCustomViewComponentActivation(Resolve);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Kernel = RegisterApplicationComponents(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("test");
            app.UseMvc();
            app.UseStaticFiles();

            //StartData.Initialize(app);
        }

        private IKernel RegisterApplicationComponents(IApplicationBuilder app)
        {
            var kernel = new StandardKernel();

            //foreach (var ctrlType in app.GetControllerTypes())
            //{
            //    kernel.Bind(ctrlType).ToSelf().InRequestScope();
            //}

            DbContextOptionsBuilder db = new DbContextOptionsBuilder();
            db.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            kernel.Bind<DbContext>().To<KarateContext>().InRequestScope().WithConstructorArgument("options", db.Options);
            kernel.Bind<UnitOfWork>().To<UnitOfWork>().InRequestScope().WithConstructorArgument("context", kernel.Get<DbContext>());

            kernel.Bind<ITraining>().To<TrainingManager>().InRequestScope().WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>());
            kernel.Bind<ITrainer>().To<TrainerManager>().InRequestScope().WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>());
            kernel.Bind<IStudent>().To<StudentManager>().InRequestScope().WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>());
            kernel.Bind<IBeltEarning>().To<BeltEarningManager>().InRequestScope().WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>());
            kernel.Bind<IPool>().To<PoolManager>().InRequestScope().WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>());
            kernel.Bind<ApiTrainingManager>().To<ApiTrainingManager>().InRequestScope().WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>());
            kernel.Bind<ApiTrainerManager>().To<ApiTrainerManager>().InRequestScope().WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>());
            kernel.Bind<PoolManager>().To<PoolManager>().InRequestScope().WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>());
            kernel.Bind<ApiPoolManager>().To<ApiPoolManager>().InRequestScope().WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>());
            kernel.Bind<AnalyticsManager>().To<AnalyticsManager>().InRequestScope().WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>())
                .WithConstructorArgument("context", kernel.Get<DbContext>());

            kernel.Bind<StudentController>().To<StudentController>().InRequestScope().WithConstructorArgument("studentManager", kernel.Get<IStudent>())
                                                                                     .WithConstructorArgument("beltEarningManager", kernel.Get<IBeltEarning>())
                                                                                     .WithConstructorArgument("trainerManager", kernel.Get<ITrainer>())
                                                                                     .WithConstructorArgument("trainingManager", kernel.Get<ITraining>());
            kernel.Bind<TrainingController>().To<TrainingController>().InRequestScope().WithConstructorArgument("trainingManager", kernel.Get<ITraining>())
                                                                                       .WithConstructorArgument("trainerManager", kernel.Get<ITrainer>());
            kernel.Bind<TrainerController>().To<TrainerController>().InRequestScope().WithConstructorArgument("trainerManager", kernel.Get<ITrainer>());
            kernel.Bind<BeltEarningController>().To<BeltEarningController>().InRequestScope().WithConstructorArgument("beltEarningManager", kernel.Get<IBeltEarning>());
            kernel.Bind<AnalyticController>().To<AnalyticController>().InRequestScope().WithConstructorArgument("analyticManager", kernel.Get<AnalyticsManager>());
            kernel.Bind<PoolController>().To<PoolController>().InRequestScope().WithConstructorArgument("poolManager", kernel.Get<PoolManager>());

            kernel.Bind<TrainersController>().To<TrainersController>().InRequestScope().WithConstructorArgument("trainerManager", kernel.Get<ApiTrainerManager>());
            kernel.Bind<TrainingsController>().To<TrainingsController>().InRequestScope().WithConstructorArgument("trainingManager", kernel.Get<ApiTrainingManager>());
            kernel.Bind<PoolsController>().To<PoolsController>().InRequestScope().WithConstructorArgument("poolManager", kernel.Get<ApiPoolManager>());

            // Cross-wire required framework services
            kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);

            return kernel;
        }
    }
}
