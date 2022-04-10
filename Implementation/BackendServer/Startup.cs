using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackendServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            // services.AddSingleton(_ => {
            //     var rsa = RSA.Create();
            //     rsa.ImportRSAPublicKey(
            //         source: Convert.FromBase64String(Configuration["Jwt:Asymmetric:PublicKey"]),
            //         bytesRead: out var _
            //     );
            //
            //     return new RsaSecurityKey(rsa);
            // });
            //
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer(options => {
            //         SecurityKey rsa = services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();
            //
            //         options.IncludeErrorDetails = true; // <- great for debugging
            //
            //         options.TokenValidationParameters = new TokenValidationParameters {
            //             IssuerSigningKey = rsa,
            //             ValidAudience = "jwt-test",
            //             ValidIssuer = "jwt-test",
            //             RequireSignedTokens = true,
            //             RequireExpirationTime = true,
            //             ValidateLifetime = true,
            //             ValidateAudience = true,
            //             ValidateIssuer = true,
            //         };
            //     });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
