using api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace api {
  public class Startup {
    private readonly IConfiguration _config;

    public Startup(IConfiguration config) {
      _config = config;
    }
    public IConfiguration Configuration {
      get;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      
  
      services.AddDbContext < DataContext > (optionsAction =>{
        optionsAction.UseMySQL(_config.GetConnectionString("DefaultConnection"));
      });
      services.AddCors();
       
      services.AddAuthentication(opt=>{
         opt.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
          opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
          }).AddJwtBearer(option=>
          {
            option.SaveToken=true;
              option.TokenValidationParameters= new TokenValidationParameters
                {
                    ValidateIssuer=true,
                    ValidateAudience=true,
                    ValidateLifetime=true,
                    ValidateIssuerSigningKey=true,
                    ValidIssuer="https://localhost:5001",
                    ValidAudience="https://localhost:5001",
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@365"))
                 };
          });
      services.AddControllers();
      services.AddSwaggerGen(c =>{
        c.SwaggerDoc("v1", new OpenApiInfo {
          Title = "api",
          Version = "v1"
        });
      });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1"));
      }

      app.UseHttpsRedirection();
      app.UseRouting();  
      app.UseAuthentication(); 
      app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

     
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>{
        endpoints.MapControllers();
      });
    }
  }
}