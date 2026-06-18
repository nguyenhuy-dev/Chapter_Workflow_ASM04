using MangaWorkflow.Repositories.HuyNQ;
using MangaWorkflow.Services.HuyNQ;
using MangaWorkflow.SoapService.HuyNQ.SoapServices;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSoapCore();
builder.Services.AddScoped<IChapterHuyNqService, ChapterHuyNqService>();
builder.Services.AddScoped<ChapterHuyNqRepository>();
builder.Services.AddScoped<IChapterHuyNqSoapService, ChapterHuyNqSoapService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSoapEndpoint<IChapterHuyNqSoapService>("/Chapter.asmx", new SoapEncoderOptions());

app.Run();
