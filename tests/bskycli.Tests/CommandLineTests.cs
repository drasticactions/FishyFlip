// <copyright file="CommandLineTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using bskycli.Tests.Data;
using EasyTestFileXunit;
using FishyFlip;
using static ProgramCommands;

namespace bskycli.Tests;

[UsesEasyTestFile]
public class CommandLineTests
{
    [Fact]
    public async Task CreatePostTestAsync()
    {
        var options = new PostOptions()
        {
            Text = "Hello from XUnit!",
        };

        TestData.SetBaseOptions(options);
        try
        {
            await ProgramCommands.PostAsync(options);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}