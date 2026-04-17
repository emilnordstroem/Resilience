var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Resilience_API>("resiliencetestapi");

builder.AddProject<Projects.MyResilientAPI>("myresilientapi");

builder.Build().Run();
