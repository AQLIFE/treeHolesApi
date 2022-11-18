using Microsoft.EntityFrameworkCore;
using treeHolesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();     // ����MiniMapApi �� swgger ��֧��
builder.Services.AddSwaggerGen();

#region ���Ӧ�÷��������
// ���ÿ����� ��swgger ֧��,���ȫ�ֹ�����
builder.Services.AddControllers(options =>
    options.Filters.Add<ApplicationFilters>());

// ���DbContext ֧��
builder.Services.AddDbContext<TreeDbContext>(options =>options.UseMySQL(
    builder!.Configuration.GetConnectionString("MysqlConnection")));

// ��ӿ���֧��
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