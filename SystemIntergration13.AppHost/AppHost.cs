var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Resilience_API>("resiliencetestapi");

builder.Build().Run();
