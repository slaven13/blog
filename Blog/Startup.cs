using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseAccessLayer.Data.Repository.GenericRepository;
using DataBaseAccessLayer.Data.DatabaseContext;
using DataBaseAccessLayer.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BusinessLogic.Services;
using DataBaseAccessLayer.Data.Repository.Contracts;
using DataBaseAccessLayer.Data.Repository;
using AutoMapper;


namespace Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<BlogContext>(options => options.UseSqlServer(Configuration.GetConnectionString("default")));

            services.AddScoped(typeof(IRepository<Comment>), typeof(Repository<Comment>));
            services.AddScoped(typeof(IRepository<Post>), typeof(Repository<Post>));
            services.AddScoped(typeof(IRepository<User>), typeof(Repository<User>));
            services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));
            services.AddScoped(typeof(IPostRepository), typeof(PostRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

            services.AddScoped(typeof(IPostsService), typeof(PostsService));
            services.AddScoped(typeof(ICommentsService), typeof(CommentsService));

            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
