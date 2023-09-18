using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Online_Shopping_Cart.Data;
var builder = WebApplication.CreateBuilder(args);
//we are using the trasiant of the HttpAccessor instead of adding.
//builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();// its our choise to make it scope or transient.
string connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));
//previously the session's data was stored in the the ram but by using this our cache stored inthe database
builder.Services.AddDistributedSqlServerCache(m =>
{
    m.ConnectionString = connectionString;
    m.SchemaName = "dbo";
    //m.DefaultSlidingExpiration = TimeSpan.FromMinutes(20);
    m.TableName = "SessionData";//now our data will store in the database table and pick easily from table
});
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSession(m =>
{
    m.IdleTimeout = TimeSpan.FromMinutes(20);
    //m.IOTimeout = TimeSpan.FromMinutes(20);
     
});//adding the service of the session to the builder

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();//using the session
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
