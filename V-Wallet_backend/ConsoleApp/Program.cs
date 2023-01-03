using Application.Abstract;
using Application.Commands;
using Domain;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var diContainer = new ServiceCollection()
                .AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer(@"Data Source=TOPSKI\SQLEXPRESS;Initial Catalog=VWalletDB;Integrated Security=True");
                })
                .AddMediatR(typeof(IUnitOfWork))
                .AddScoped<ICreditCardRepository, CreditCardRepository>()
                .AddScoped<ICryptoRepository, CryptoRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .BuildServiceProvider();

using (var scope = diContainer.CreateScope())
{
    var scopedServices = scope.ServiceProvider;

    var db = scopedServices.GetRequiredService<DataContext>();

    db.Database.EnsureDeleted();

    db.Database.EnsureCreated();
}

var mediator = diContainer.GetRequiredService<IMediator>();

for (int i = 0; i < 15; i++)
{
    var cc = await mediator.Send(new CreateCreditCard());
}