var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var settings = builder.Configuration.GetSection("Settings").Get<Settings>();

builder.Services.AddHttpClient("MyClient", client =>
{
    client.BaseAddress = new Uri("ttps://todoapi-35735216.azurewebsites.net/api/"); 
    client.DefaultRequestHeaders.Add("ApiKey", settings.ApiKey); 
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
