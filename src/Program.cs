using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using AdventOfCode;

// load configuration
var config = new ConfigurationBuilder()
		.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
		.AddUserSecrets<Program>(optional: false, reloadOnChange: false)
		.AddEnvironmentVariables()
		.Build();

// register services
var services = new ServiceCollection()
		.Configure<Settings>(config.GetSection(Settings.SectionName))
		.AddOptions()
		.AddSingleton<Runner>()
		.AddSingleton<Downloader>();

var serviceProvider = services.BuildServiceProvider();

Utils.WriteBanner();

var runner = serviceProvider.GetService<Runner>();
await runner.Run().ConfigureAwait(false);

Console.WriteLine($"{Utils.NL}Done!");
