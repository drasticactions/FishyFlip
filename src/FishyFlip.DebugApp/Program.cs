// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Net.NetworkInformation;
using System.Text.Json;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();

Ioc.Default.ConfigureServices(serviceCollection
       .BuildServiceProvider());

Console.WriteLine("Press any key to exit...");
