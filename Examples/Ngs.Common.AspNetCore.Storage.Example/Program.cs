
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ngs.Common.AspNetCore.Storage.Extensions;
using Ngs.Common.AspNetCore.Storage.Models;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddStorage(@"C:\Users\davex\OneDrive\Desktop\StorageRoot");

var services = builder.Services.BuildServiceProvider();
var storage = services.GetRequiredService<StorageRoot>();

var host = builder.Build();

await host.RunAsync();