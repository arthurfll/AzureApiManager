var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Endpoint GET /
app.MapGet("/", () =>
{
    var horarioAtual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    var nomeMaquina = Environment.MachineName;

    return Results.Ok(new
    {
        horario = horarioAtual,
        maquina = nomeMaquina
    });
});

app.Run();
