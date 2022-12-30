using Microsoft.AspNetCore.Authentication;
using MongoDB.Bson.Serialization;
using UT4MasterServer.Authentication;
using UT4MasterServer.Models;
using UT4MasterServer.Services;

namespace UT4MasterServer;

public static class Program
{
	public static void Main(string[] args)
	{
		// register serializer for EpicID type
		BsonSerializer.RegisterSerializationProvider(new EpicIDSerializationProvider());

		// start up asp.net
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers(o =>
		{
			o.RespectBrowserAcceptHeader = true;
		});
		builder.Services.Configure<DatabaseSettings>(
			builder.Configuration.GetSection("UT4EverDatabase")
		);

		// services whose instance is created per-request
		builder.Services
			.AddScoped<DatabaseContext>()
			.AddScoped<FriendService>()
			.AddScoped<AccountService>()
			.AddScoped<SessionService>()
			.AddScoped<CloudstorageService>()
			.AddScoped<MatchmakingService>();

		// services whose instance is created once and are persistent
		builder.Services
			.AddSingleton<CodeService>();

		builder.Services
			.AddAuthentication(/*by default there is no authentication*/)
			.AddScheme<AuthenticationSchemeOptions, BearerAuthenticationHandler>("bearer", null)
			.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("basic", null);
		builder.Host.ConfigureLogging(logging =>
		{
			logging.ClearProviders();
			logging.AddConsole();
		});

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();
		app.UseAuthorization();
		app.UseAuthentication();
		app.MapControllers();
		app.UseStaticFiles();
		//app.UseStaticFiles(new StaticFileOptions()
		//{
		//	FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "StaticWebFiles")),
		//	RequestPath = "/",
		//	OnPrepareResponse = ctx =>
		//	{
		//		// operation to do on all static file responses
		//	}
		//});

		app.Run();
	}
}
