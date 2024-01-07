using NetMenu.AppLib.Configuration;

var builder = WebApplication.CreateBuilder(args)
    ._ConfigureServices();

var app = builder.Build()
    ._Configure(); ;