
using Microsoft.Extensions.Hosting;
using Ngs.Common.AspNetCore.Storage.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddStorage(@"C:\Users\davex\OneDrive\Desktop\StorageRoot");

var host = builder.Build();

await host.RunAsync();