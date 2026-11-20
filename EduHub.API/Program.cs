using Scalar.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using EduHub.Domain.Repositories;
using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Infrastructure.Repositories;
using EduHub.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// THÊM DÒNG NÀY: Để hệ thống có thể tìm thấy các API Endpoint và đưa lên Scalar
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddOpenApi();

// Đăng ký Repository
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();

// Đăng ký các Notification Services cụ thể
builder.Services.AddTransient<EmailNotificationService>();
builder.Services.AddTransient<SmsNotificationService>();

// Đăng ký Factory và EventService
builder.Services.AddTransient<INotificationFactory, INotificationFactory>();
builder.Services.AddTransient<EventService>();

var app = builder.Build();

// THÊM KHỐI IF NÀY: Chỉ hiển thị tài liệu API khi đang code (Development)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();