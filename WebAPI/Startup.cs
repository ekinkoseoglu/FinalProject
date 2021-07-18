using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace WebAPI
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

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder => builder.WithOrigins("http://localhost:44360")); // Normalde domain ne ise yani site ne ise onu yazıyoruz.
            });


            //services.AddSingleton<IProductService, ProductManager>(); // Birisi senden  IProductService isterse ona ProductManager ver
            //services.AddSingleton<IProductDal, EfProductDal>(); // Birisi senden  IProductDal isterse ona EfProductDal ver

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //BİZ  ASP.NET WebAPI'ye "BU SİSTEMDE JWT KULLANILACAK HABERİN OLSUN" DİYORUZ. (Aşağısı dahil)

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // Token verdiğimiz zaman Issuer olarak (www.ekin.com) veriyoruz oradan bana bu bilgi geri gelsin mi?
                        ValidateAudience = true, // Audience'yı da kontrol et
                        ValidateLifetime = true, // Token'ın da lifetimesini kontrol edeyim mi yoksa bi token olsun yeterli mi?
                        ValidIssuer = tokenOptions.Issuer, //normalde  (www.ekin.com) yazmam gerekiyordu ama yukarıda TokenOptions'a bağladık oradan çektik
                        ValidAudience = tokenOptions.Audience, // TokenOptions'da ki Audience
                        ValidateIssuerSigningKey = true, // Anahtarı da kontrol et
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            //ServiceTool.Create(services);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseCors(
                builder => builder.WithOrigins("http://localhost:44360").AllowAnyHeader());/*Bu domainden gelen her türlü "Get,Post,Put, Delete" gibi bütün http requestlerine izin ver */
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // Eve Giriş için anahtar // Önce eve gireceksin, sonra evde birşeyler yapabilirsin. (Authentication önde olacak)

            app.UseAuthorization(); // Evdeki yetki

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
