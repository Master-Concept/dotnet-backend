using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
//GraphQL-server Start
using TodoApi.Data;
using TodoApi.Interfaces;
using TodoApi.Repositories;

//GraphQL-server end
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.(Users)
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase"));
});
//GraphQL-server Start
// Register custom services for the superheroes
builder.Services.AddScoped<ISuperheroRepository, SuperheroRepository>();
builder.Services.AddScoped<ISuperpowerRepository, SuperpowerRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddProjections()
                .AddFiltering()
                .AddSorting();
                
// Add Application Db Context options
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase")));

//GraphQL-server End
//Add CORS with default policy and middleware
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Add CORS with default policy and middleware
app.UseCors();

app.UseAuthorization();

app.MapControllers();
//GraphQL-server Start
app.MapGraphQL("/graphql");
//GraphQL-server End
app.Run();
