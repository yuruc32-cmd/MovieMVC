using _1121754.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
// 加入 Session 所需服務
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache(); // 使用記憶體儲存 Session
// Add services to the container.
builder.Services.AddControllersWithViews();


//取得組態中資料庫連線設定
string connectionString = builder.Configuration.GetConnectionString("CmsContext");

//註冊EF Core的CmsContext
builder.Services.AddDbContext<CmsContext>(options => options.UseSqlServer(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// 使用 Session（順序要正確，放在 UseRouting 和 UseAuthorization 之間）
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
