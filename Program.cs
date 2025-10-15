using _1121754.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
// �[�J Session �һݪA��
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache(); // �ϥΰO�����x�s Session
// Add services to the container.
builder.Services.AddControllersWithViews();


//���o�պA����Ʈw�s�u�]�w
string connectionString = builder.Configuration.GetConnectionString("CmsContext");

//���UEF Core��CmsContext
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
// �ϥ� Session�]���ǭn���T�A��b UseRouting �M UseAuthorization �����^
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
