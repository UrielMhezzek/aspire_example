var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.TIK_Backend>("tik.backend");

builder.AddProject<Projects.TIK_Frontend_Server>("tik.frontend.server");

builder.Build().Run();
