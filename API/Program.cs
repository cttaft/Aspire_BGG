using BoardGamer.BoardGameGeek.BoardGameGeekXmlApi2;
using BoardGamer.BoardGameGeek.BoardGameGeekXmlApi2.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IBoardGameGeekXmlApi2Client, BoardGameGeekXmlApi2Client>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/games",
        async (IBoardGameGeekXmlApi2Client client) =>
            (await client.GetCollectionAsync(new CollectionRequest("cttaft")))
        .Result)
    .WithName("GetGames")
    .WithOpenApi();

app.MapGet("/games/{id}",async  (IBoardGameGeekXmlApi2Client client, int id) => await client.GetGameAsync(id))
    .WithName("GetGame")
    .WithOpenApi();

app.Run();
