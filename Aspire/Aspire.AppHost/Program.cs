using TIK.Shared.Helper;

var builder = DistributedApplication.CreateBuilder(args);

var keyVault = builder.AddAzureKeyVault("secrets");

//var sql = builder.AddSqlServer("SqlServer")
//                 .AddDatabase("sqldata");

var backend = builder.AddProject<Projects.TIK_Backend>(Constants.BACKEND)
     .WithReference(keyVault)
     //.WithReference(sql)
     ;

builder.AddProject<Projects.TIK_Frontend_Server>(Constants.FRONTEND)
    .WithReference(backend);


builder.Build().Run();


//#if DEBUG
//    Process.Start("http://localhost:18888");
//#endif
