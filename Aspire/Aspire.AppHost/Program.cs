using TIK.Shared.Helper;

var builder = DistributedApplication.CreateBuilder(args);

var keyVault = builder.AddAzureKeyVault("secrets");


var backend = builder.AddProject<Projects.TIK_Backend>(Constants.BACKEND)
     .WithReference(keyVault);

builder.AddProject<Projects.TIK_Frontend_Server>(Constants.FRONTEND)
    .WithReference(backend);

builder.Build().Run();


//#if DEBUG
//    Process.Start("http://localhost:18888");
//#endif
