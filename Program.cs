using Happy_Devs_BE;
using Happy_Devs_BE.DiContainer;
using Happy_Devs_BE.Services.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "_AllowLocalDevelopment",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    );
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.OperationFilter<AuthorizationHeaderFilter>();
});
builder.Services.AddExceptionHandler<TraceExceptionLogger>();
DiContainer.registerServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseCors("_AllowLocalDevelopment");
app.UseAuthorization();
app.MapControllers();

// Run something here

app.Run();
