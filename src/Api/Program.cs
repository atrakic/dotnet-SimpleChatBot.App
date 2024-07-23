using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKernel().AddOpenAIChatCompletion(
    "GPT-4",
    //apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new InvalidOperationException("OPENAI_API_KEY is required")
    apiKey: builder.Configuration["OpenAI:ApiKey"]!
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/Chat", (string question, Kernel kernel) =>
{
    return kernel.InvokePromptStreamingAsync<string>(question);
})
.WithName("ChatPrompt")
.WithOpenApi();

app.Run();
