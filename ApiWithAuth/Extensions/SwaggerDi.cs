using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ApiWithAuth.Extensions
{
    public static class SwaggerDi
    {
        public static void SwaggerHandler(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Event Management System",
                    Description = "ASP.Net Web API for events management",
                    TermsOfService = new Uri("http://adediranmuhydeen.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Muhydeen Adediran",
                        Url = new Uri("https://www.linkedin.com/in/muhydeen-adebayo-adediran-984471117/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License",
                        Url = new Uri("http://adediranmuhydeen.com")
                    }

                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer [Space] and your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"'"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                    }
                });
            });
        }
    }
}
