using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<API>("backend");
var cache = builder.AddRedis("cache").WithRedisInsight();
var ollama = builder.AddOllama("ollama", 6969)
    .WithDataVolume()
    .WithOpenWebUI()
    .AddModel("llama3");
builder.AddProject<Frontend>("frontend")
    .WithReference(backend)
    .WithReference(cache)
    .WaitFor(ollama)
    .WithReference(ollama);

builder.Build().Run();