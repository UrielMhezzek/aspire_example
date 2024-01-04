using System.Diagnostics;
using TIK.Shared.Helper;
using Aspire.Azure.Security.KeyVault;
using Microsoft.Extensions.Hosting; 

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
