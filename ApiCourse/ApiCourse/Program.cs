

using ApiCourse.Persistence;
using Microsoft.Data.SqlClient;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



var connectionstring = builder.Configuration.GetConnectionString("MyConnection") ??
    throw new InvalidOperationException(" Connection string 'MyConnection ' Not Found.");

// Add EF Core with Identity support
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionstring));
//builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddDependency(builder.Configuration);
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.MapIdentityApi<ApplicationUser>();
app.MapControllers();
//app.UseExceptionHandler();
app.Run();
