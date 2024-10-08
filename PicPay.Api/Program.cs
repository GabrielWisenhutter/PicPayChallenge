using Microsoft.EntityFrameworkCore;
using PicPay.Api.Settings;
using PicPay.Domain.Extensions;
using PicPay.Domain.Handlers;
using PicPay.Domain.Interfaces;
using PicPay.Infra.Data;
using PicPay.Infra.Data.Context;
using PicPay.Infra.Data.Repositories;
using PicPay.Infra.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPicPay(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
