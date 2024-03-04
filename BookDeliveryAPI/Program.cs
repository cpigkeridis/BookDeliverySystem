using BookDeliverySystemAPI.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<BookDeliverySystemAPI.Interfaces.IClientRepository, BookDeliverySystemAPI.Repositories.ClientRepository>();
builder.Services.AddSingleton<BookDeliverySystemAPI.Interfaces.IAdministratorRepository, BookDeliverySystemAPI.Repositories.AdministratorRepository>();
builder.Services.AddSingleton<ConfigManagerAppSettings.IConfigManager, ConfigManagerAppSettings.AppsConfiguration>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
        pattern: "controller=Client/{action=Index}/{id?}");

app.MapControllers();

app.Run();
