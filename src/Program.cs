// See https://aka.ms/new-console-template for more information

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
IServiceCollection services = new ServiceCollection()
		.Configure<Settings>(config.GetSection(Settings.SectionName))
		.AddOptions()
		.AddSingleton<Runner>();

var serviceProvider = services.BuildServiceProvider();

Utils.WriteBanner();

var runner = serviceProvider.GetService<Runner>();
runner.Run();

Console.WriteLine($"{Utils.NL}Done!");

