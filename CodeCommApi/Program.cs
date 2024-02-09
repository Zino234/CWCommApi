using CodeCommApi;
using CodeCommApi.Data;
using CodeCommApi.Dependencies.interfaces;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dependencies.Services;
using CodeCommApi.Models.Hubs;
using CodeCommApi.Models.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IDirectMessageService, DirectMessageService>();
builder.Services.AddScoped<IGroupMessageService, GroupMessageService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IJobService, JobService>();


//OFFLINE DATABASE
builder.Services.AddDbContext<CodeCommDbContext>(
    options => {
        options.UseSqlite(builder.Configuration.GetConnectionString("Offline"));
}
);



//ONLINE DATABASE
// builder.Services.AddDbContext<CodeCommDbContext>(options=>{
//     options.UseSqlServer(builder.Configuration.GetConnectionString("Online")
//     );
// });




var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll");
app.UseHsts();
app.UseStaticFiles();

//REGISTERING THE HUBS FOR REAL TIME CONNECTION.
app.MapHub<CodeCommChatHub>("/hub/chatHub");
app.MapHub<CodeCommGroupHub>("/hub/groupHub");
app.MapHub<CodecommMessageHub>("/hub/messageHub");
app.MapHub<CodeCommNotificationHub>("/hub/notificationHub");




app.MapControllers();

app.Run();
