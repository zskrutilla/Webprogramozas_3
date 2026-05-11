using System.Text;

namespace WebApplicationASPNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseStaticFiles();

            app.MapGet("/welcome", async t =>
            {
                string name = t.Request.Query["name"].ToString();
                string pass = t.Request.Query["pass"].ToString();

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < 10; i++)
                {
                    sb.Append(name + i + "<br>");
                }

                await t.Response.WriteAsync($"<h1>Welcome {name}</h1>");
                await t.Response.WriteAsync(sb.ToString());
            });

            //app.MapGet("/", () => "<h1>Hello World!</h1>");
            //app.MapGet("/time", () => DateTime.Now.ToLongTimeString());
            //app.MapGet("/more", () =>
            //{
            //    return new
            //    {
            //        Message = "Hello, this is more information.",
            //        Date = DateTime.Now,
            //        Framework = ".NET 10.0",
            //        LanguageVersion = "C# 14.0"
            //    };
            //});
            app.Run();
        }
    }
}
