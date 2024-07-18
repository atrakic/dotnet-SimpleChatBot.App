var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/api/Chat", async (string question) =>
{
    return GetPrompt(question);
})
.WithName("ChatPrompt")
.WithOpenApi();


app.Run();

static async IAsyncEnumerable<string> GetPrompt(string question)
{
    foreach (var msg in question.Split(" "))
    {
        await Task.Delay(100);
        yield return msg + " ";
    }
}
