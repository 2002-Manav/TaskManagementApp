using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Services;
using TaskManagement.Infrastructure.DatabaseContext;
using TaskManagement.Infrastructure.Repositories;
using System.Text;
using TaskManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TaskManagement.Core.Domain.Identities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Register Services
builder.Services.AddScoped<IUserRegisterService, UserRegisterService>();
builder.Services.AddScoped<IUserLoginService, UserLoginService>();
builder.Services.AddScoped<ICreateTaskService, CreateTaskService>();
builder.Services.AddScoped<IUpdateTaskService, UpdateTaskService>();
builder.Services.AddScoped<IDeleteTaskService, DeleteTaskService>();
builder.Services.AddScoped<IGetTaskService, GetTaskService>();
builder.Services.AddScoped<ISearchTaskService, SearchTaskService>();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//Enable Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 3;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders(); 

builder.Services.AddAuthorization();


builder.Services.AddControllers();

//swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
});

var app = builder.Build();

// Configuration of HTTP 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

//swagger implementation
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
