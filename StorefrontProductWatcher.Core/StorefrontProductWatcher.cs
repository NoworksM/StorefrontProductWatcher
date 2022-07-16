namespace StorefrontProductWatcher.Core;

using Microsoft.Extensions.CommandLineUtils;

public class StorefrontProductWatcher
{
    public static async Task Main(string[] args)
    {
        var app = new CommandLineApplication();
        app.Name = "sfpw - Storefront Product Watcher";
        app.Description = "Watch storefronts for new products and execute actions one them";
        app.HelpOption("-h --help");

        var configOption = app.Option("-c --config <config>", "Path to config file", CommandOptionType.SingleValue);
        var debugOption = app.Option("-d --debug", "Debug mode", CommandOptionType.NoValue);
        var verboseOption = app.Option("-v --verbose", "Verbose logging mode", CommandOptionType.NoValue);

        app.Command("run", command =>
        {
            command.Description = "Run sfpw";
            command.HelpOption("-h --help");
            command.Options.Add(configOption);
            command.Options.Add(verboseOption);
            command.Options.Add(debugOption);

            command.OnExecute(() =>
            {
                if (configOption.HasValue())
                {

                }
                else
                {
                    command.ShowHelp();
                }

                return 0;
            });
        });

        app.Command("watch", command =>
        {
            command.Description = "Run sfpw in watch mode periodically checking the storefronts";
            command.HelpOption("-h --help");
            command.Options.Add(configOption);
            command.Option("-i --interval", "Interval to check in minutes (default: 30m)", CommandOptionType.SingleValue);
            command.Options.Add(verboseOption);
            command.Options.Add(debugOption);
            

            command.OnExecute(() =>
            {
                if (configOption.HasValue())
                {

                }
                else
                {
                    command.ShowHelp();
                }

                return 0;
            });
        });

        app.OnExecute(() =>
        {
            app.ShowHint();

            return 0;
        });

        app.Execute(args);
    }
}