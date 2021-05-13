using api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using api.Business_Logic;

namespace api
{
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
      },ServiceLifetime.Transient);
      services.AddCors();
      var key ="secureSuperKey@365";
      services.AddAuthentication(x=>{

x.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
x.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(x=>{
          x.RequireHttpsMetadata = false;
        x.SaveToken=true;
        x.TokenValidationParameters=new TokenValidationParameters{
ValidateIssuerSigningKey=true,
IssuerSigningKey=  new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
ValidateIssuer=false,
ValidateAudience=false,

            
        };
      });
      services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));
      
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
      
      app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

     app.UseAuthentication(); 
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>{
        endpoints.MapControllers();
      });
    }
  }
}