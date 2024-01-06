using TIK.Shared.Helper;

var builder = DistributedApplication.CreateBuilder(args);

var keyVault = builder.AddAzureKeyVault("secrets");


var backend = builder.AddProject<Projects.TIK_Backend>(Constants.BACKEND)
     .WithReference(keyVault)
     //.WithReference(sql)
     ;

builder.AddProject<Projects.TIK_Frontend_Server>(Constants.FRONTEND)
    .WithReference(backend);

builder.AddProject<Projects.TIK_Database>("tik.database");

builder.Build().Run();


//#if DEBUG
//    Process.Start("http://localhost:18888");
//#endif
