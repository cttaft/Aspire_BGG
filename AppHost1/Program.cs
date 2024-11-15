using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<API>("backend");
var cache = builder.AddRedis("cache");
var ollama = builder.AddOllama("ollama", 6969)
    .WithDataVolume()
    .AddModel("llama3");
builder.AddProject<Frontend>("frontend")
    .WithReference(backend)
    .WithReference(cache)
    .WaitFor(ollama)
    .WithReference(ollama);

builder.Build().Run();