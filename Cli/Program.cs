using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(config =>
{
    config.AddCommand<ResetDbCommand>("reset-db")
        .WithDescription("Generates code based on the provided input.");
});

return app.Run(args);

public class ResetDbCommand : Command
{
    public override int Execute(CommandContext context)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

        // TODO: use configuration to locate/reset the database
        return 0;
    }

    // Optionally implement these overloads if you want async execution or validation:
    // public Task<int> Execute(CommandContext context, CommandSettings settings) => throw new NotImplementedException();
    // public ValidationResult Validate(CommandContext context, CommandSettings settings) => throw new NotImplementedException();
}