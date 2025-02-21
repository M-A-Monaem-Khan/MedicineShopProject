using Blazored.Toast;
using VaiVaiPharmacy.UI.Components;
using VaiVaiPharmacy.UI.Configuration;
using VaiVaiPharmacy.UI.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<sessionData>();

//for Serversite render mode
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor().AddHubOptions(options =>
{
    options.MaximumReceiveMessageSize = 10485760; //10MB 
});
builder.Services.AddCascadingAuthenticationState();

//configured Service
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureJsonNamingConvention();
builder.Services.ConfigureRepositoryWrapper();

builder.Services.AddBlazoredToast();


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();
app.UseHttpsRedirection();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
