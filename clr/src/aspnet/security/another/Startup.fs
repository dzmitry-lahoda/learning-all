namespace another

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.EntityFrameworkCore
open Microsoft.AspNetCore.Identity
open Microsoft.AspNetCore.Identity.EntityFrameworkCore
open Microsoft.AspNetCore.Mvc

type Db() =
    inherit IdentityDbContext()

type Home() =
    inherit Controller()

type Startup() =

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    member this.ConfigureServices(services: IServiceCollection) =
        services.AddDbContext<Db>(fun config ->
            config.UseInMemoryDatabase("DoNotCare") |> ignore           
        )

        services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<Db>()

        services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", fun config ->
            config.Cookie.Name <- "Test.Test.Cookie2"
            //config.LoginPath <- PathString("/home/authenticate")
            ()
        )

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if env.IsDevelopment() then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseRouting() |> ignore
        app.UseAuthentication() 
        app.UseAuthorization() 

        app.UseEndpoints(fun endpoints ->
            endpoints.MapDefaultControllerRoute() |> ignore
            ) |> ignore
