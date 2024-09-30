using Firebase.Database;
using MessengerEntity.Entity;
using MessengerPersistency.GenericRepository;
using MessengerPersistency.Interface;
using MessengerService.Service;
using Microsoft.AspNetCore.Builder.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de Firebase
var firebaseConfig = builder.Configuration.GetSection("Firebase");
var databaseUrl = firebaseConfig["DatabaseUrl"];

// Agrega el servicio FirebaseClient usando la URL de la base de datos
builder.Services.AddSingleton<FirebaseClient>(provider =>
{
    return new FirebaseClient(databaseUrl);
});

builder.Services.AddScoped<IGenericRepositry<User>>(provider =>
{
    var firebaseClient = provider.GetRequiredService<FirebaseClient>();
    return new GenericRepository<User>(firebaseClient, "user");
});

builder.Services.AddScoped<IGenericRepositry<Chat>>(provider =>
{
    var firebaseClient = provider.GetRequiredService<FirebaseClient>();
    return new GenericRepository<Chat>(firebaseClient, "chat");
});

builder.Services.AddScoped<IGenericRepositry<Profile>>(provider =>
{
    var firebaseClient = provider.GetRequiredService<FirebaseClient>();
    return new GenericRepository<Profile>(firebaseClient, "profile");
});

builder.Services.AddScoped<IGenericRepositry<Message>>(provider =>
{
    var firebaseClient = provider.GetRequiredService<FirebaseClient>();
    return new GenericRepository<Message>(firebaseClient, "message");
});

builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
