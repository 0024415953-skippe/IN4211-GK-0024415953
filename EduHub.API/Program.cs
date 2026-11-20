using Scalar.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using EduHub.Domain.Repositories;
using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Infrastructure.Repositories;
using EduHub.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Thay AddSwaggerGen bằng AddOpenApi
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Đăng ký Repository
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();

// Đăng ký các Notification Services cụ thể
builder.Services.AddTransient<EmailNotificationService>();
builder.Services.AddTransient<SmsNotificationService>();

// Đăng ký Factory và EventService
builder.Services.AddTransient<INotificationFactory, NotificationFactory>();
builder.Services.AddTransient<EventService>();

var app = builder.Build();

// Thay UseSwagger bằng MapOpenApi và MapScalarApiReference
app.MapOpenApi();
app.MapScalarApiReference();

app.MapControllers();

app.Run();