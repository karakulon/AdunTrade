using AT.WebParsers.CsMarketParser;
using AT.WebParsers.LisSkinsParser;
internal class Program
{
    private static async Task Main(string[] args)
    {
        LisSkins.GetJson();
        CsMarket.GetJson();

        var builder = WebApplication.CreateBuilder(args);

        // builder.Services.AddSingleton<ICsMarket, CsMarket>();

        // builder.Services.AddSingleton<ILisSkins, LisSkins>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                              });
        });
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseCors(MyAllowSpecificOrigins);
        app.Run();
    }
}