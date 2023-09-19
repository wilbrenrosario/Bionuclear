using Bionuclear.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);

//Enable Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "origin",
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5500", "https://master--incandescent-sunburst-c9c837.netlify.app/#!/",
                              "https://master--incandescent-sunburst-c9c837.netlify.app").AllowAnyHeader()
                                                  .AllowAnyMethod(); ;
                      });
});

//JWT
#region " JWT "


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();
#endregion " JWT "

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//
    app.UseSwagger();
    app.UseSwaggerUI();
//

app.UseHttpsRedirection();

app.UseCors("origin");

app.UseAuthorization();

app.MapControllers();

app.Run();
