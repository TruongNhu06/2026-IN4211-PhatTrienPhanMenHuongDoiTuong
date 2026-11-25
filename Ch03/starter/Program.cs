using EduHub.Application.Clubs;
using EduHub.Domain.Interfaces;
using EduHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ── Đăng ký services (Dependency Injection) ──────────────────────────────
// Scoped = mỗi request một instance. Kho in-memory dùng danh sách static nên dữ liệu vẫn giữ giữa các request.
// TODO 6a: Đăng ký IClubRepository → InMemoryClubRepository (AddScoped).
builder.Services.AddScoped<IClubRepository, InMemoryClubRepository>();
// TODO 6b: Đăng ký IMemberRepository → InMemoryMemberRepository (AddScoped).
builder.Services.AddScoped<IMemberRepository, InMemoryMemberRepository>();

// TODO 6c: Đăng ký 4 handler: GetClubsQueryHandler, GetClubByIdQueryHandler,
//          CreateClubCommandHandler, DeleteClubCommandHandler (AddScoped).
builder.Services.AddScoped<GetClubsQueryHandler>();
builder.Services.AddScoped<GetClubByIdQueryHandler>();
builder.Services.AddScoped<CreateClubCommandHandler>();
builder.Services.AddScoped<DeleteClubCommandHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// TODO 7: Cấu hình SwaggerDoc "v1" với Title = "EduHub API".
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "EduHub API", Version = "v1" });
});

var app = builder.Build();

// ── Middleware pipeline ───────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
