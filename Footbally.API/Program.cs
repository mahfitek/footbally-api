using System.Text;
using Footbally.Application.AI;
using Footbally.Application.AI.Agents;
using Footbally.Application.Interfaces;
using Footbally.Infrastructure.Data;
using Footbally.Infrastructure.Jobs;
using Footbally.Infrastructure.Repositories;
using Footbally.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT token girin: Bearer {token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// DbContext - PostgreSQL
builder.Services.AddDbContext<FootballyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<IUserService, UserRepository>();
builder.Services.AddScoped<IMatchService, MatchRepository>();
builder.Services.AddScoped<ITeamService, TeamRepository>();
builder.Services.AddScoped<IPlayerProfileService, PlayerProfileRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpClient<ISupportService, AnthropicSupportService>();

// AI Provider
builder.Services.AddHttpClient<IAIProvider, OpenAiProvider>();

// AI Repository
builder.Services.AddScoped<IAiJobRepository, AiJobRepository>();

// AI Agents
// AI Agents
builder.Services.AddScoped<ProfileCoachAgent>();
builder.Services.AddScoped<RatingCardAgent>();
builder.Services.AddScoped<ModerationAgent>();
builder.Services.AddScoped<TrustScoreAgent>();
builder.Services.AddScoped<ScoutReportAgent>();
builder.Services.AddScoped<PlayerAnalysisAgent>();
builder.Services.AddScoped<CareerCoachAgent>();
builder.Services.AddScoped<FootballCvAgent>();
builder.Services.AddScoped<MatchRecommendationAgent>();
builder.Services.AddScoped<PlayerDiscoveryAgent>();
builder.Services.AddScoped<VideoSummaryAgent>();
builder.Services.AddScoped<PlayerComparisonAgent>();
builder.Services.AddScoped<ScoutMessageAgent>();
builder.Services.AddScoped<FakeProfileDetectionAgent>();
builder.Services.AddScoped<ContentQualityAgent>();
builder.Services.AddScoped<SupportTicketAgent>();

// AI Orchestrator
builder.Services.AddScoped<AiOrchestrator>();

// AI Background Job
builder.Services.AddHostedService<AiJobProcessor>();

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not found.");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FootballyCors", policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200",
            "https://getfootbally.com",
            "https://www.getfootbally.com",
            "https://footbally-lime.vercel.app",
            builder.Configuration["Cors:ProductionOrigin"] ?? "https://getfootbally.com"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("FootballyCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();