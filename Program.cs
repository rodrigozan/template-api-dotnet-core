using api;
using api.Data;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using ITfoxtec.Identity.Saml2;
using ITfoxtec.Identity.Saml2.Schemas.Metadata;
using ITfoxtec.Identity.Saml2.MvcCore.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var title = "API Minha Encomenda Chegou";

        //Inicia a API
        StartAPI(builder);

        void StartAPI(WebApplicationBuilder builder)
        {

            string prod = "/mec.api";
            if (builder.Environment.IsDevelopment())
            {
                prod = "";
            }

            ConfigureAuthentication(builder);
            ConfigureMvc(builder);
            ConfigureServices(builder);
            ConfigureOkta(builder);


            var app = builder.Build();


            app.UseAuthentication(); 
            app.UseAuthorization(); 
                                     
            app.MapControllers();            

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "swagger";
                options.SwaggerEndpoint($"{prod}/swagger/api/swagger.json", "Documento - " + title);

            });

            app.UseCors("CorsPolicy");

            app.Run();
        }

        void ConfigureAuthentication(WebApplicationBuilder builder)
        {

            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

            builder.Services.
                AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).
                AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
        }

        void ConfigureMvc(WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy", builder => builder
                     .AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader());
                })
                .AddMemoryCache() 
                .AddControllers()
                 .AddJsonOptions(x =>
                 {
                     x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                     x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                 })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });
        }

        void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataBaseContext>();
            builder.Services.AddTransient<TokenService>();

            builder.Services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("api", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = title,
                    Description = "Documento da " + title,
                    Version = "v1"
                });

            });

        }

        void ConfigureOkta(WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            builder.Services.Configure<Saml2Configuration>(builder.Configuration.GetSection("Saml2"));

            builder.Services.Configure<Saml2Configuration>(saml2Configuration =>
            {
                saml2Configuration.AllowedAudienceUris.Add(saml2Configuration.Issuer);

                var entityDescriptor = new EntityDescriptor();
                entityDescriptor.ReadIdPSsoDescriptorFromUrl(new Uri(builder.Configuration["Saml2:IdPMetadata"]));
                if (entityDescriptor.IdPSsoDescriptor != null)
                {
                    saml2Configuration.SingleSignOnDestination = entityDescriptor.IdPSsoDescriptor.SingleSignOnServices.First().Location;
                    saml2Configuration.SignatureValidationCertificates.AddRange(entityDescriptor.IdPSsoDescriptor.SigningCertificates);
                }
                else
                {
                    throw new Exception("IdPSsoDescriptor not loaded from metadata.");
                }
            });

            builder.Services.AddSaml2();
        }
    }
}