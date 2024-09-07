using Microsoft.Extensions.Options;
using ChatAppWsignalR.Hubs;
using Microsoft.Azure.SignalR;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSignalR();
builder.Services.AddCors(options=>{
    options.AddDefaultPolicy(
        builder =>{
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
    );
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();
app.UseAuthorization();
app.MapHub<ChatHub>("/chatHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
