using TIK.Shared.Helper;

var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<Projects.TIK_Backend>(Constants.BACKEND);

builder.AddProject<Projects.TIK_Frontend_Server>(Constants.FRONTEND)
    .WithReference(backend);

builder.Build().Run();
