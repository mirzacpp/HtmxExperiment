using Htmx.TagHelpers;
using HtmxProject.Database;
using HtmxProject.Infrastructure.StaticContent;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHtmxDbContext(builder.Environment.IsDevelopment());
builder.Services.AddApplicationDependencies();
builder.Services.AddScoped<HtmlContentManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapHtmxAntiforgeryScript();
app.MapRazorPages();

app.Run();
