using AT.WebParsers.CsMarketParser;
using AT.WebParsers.LisSkinsParser;
internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // builder.Services.AddSingleton<ICsMarket, CsMarket>();

        // builder.Services.AddSingleton<ILisSkins, LisSkins>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        LisSkins.GetSuperBistroItems();
        CsMarket.GetSuperBistroItems();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}