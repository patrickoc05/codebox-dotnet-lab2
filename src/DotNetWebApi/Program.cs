using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(System.Environment.GetCommandLineArgs())
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<DotNetWebApi.Startup>();
    }).Build().Run();