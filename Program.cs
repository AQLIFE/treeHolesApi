using Microsoft.EntityFrameworkCore;
using treeHolesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();     // 启用MiniMapApi 的 swgger 的支持
builder.Services.AddSwaggerGen();

#region 添加应用服务和配置
// 启用控制器 的swgger 支持,添加全局过滤器
builder.Services.AddControllers(options =>
    options.Filters.Add<ApplicationFilters>());

// 添加DbContext 支持
builder.Services.AddDbContext<TreeDbContext>(options =>options.UseMySQL(
    builder!.Configuration.GetConnectionString("MysqlConnection")));

// 添加跨域支持
builder.Services.AddCors(config => config.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.Map("/", () => "Hello World!Please use the /api path");


app.MapControllerRoute(name: "default", pattern: "{controller=TreeHoles}/{action=Index}/{id?}");
app.UseCors().UseHttpsRedirection().UseHsts().UseRouting();
app.Run();