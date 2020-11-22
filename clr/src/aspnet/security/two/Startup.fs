namespace one

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy;
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Authentication
open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.IdentityModel.JsonWebTokens
open Microsoft.IdentityModel.Tokens
open Microsoft.AspNetCore.Authorization.Infrastructure
open System.Text




type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        let jwtConfig = new TokenValidationParameters(
                            ValidateIssuer = true,
                            ValidIssuer = "lahoda-pro-issuer",

                            ValidateAudience = true,
                            ValidAudience = "lahoda-pro-audience",
                            
                            ValidateLifetime = true,

                            ValidateIssuerSigningKey = true,                
                            IssuerSigningKey = SymmetricSecurityKey(Encoding.UTF8.GetBytes("VerySecretKeyVerySecretKeyVerySecretKey"))
            )

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(fun options -> 
                options.TokenValidationParameters <- jwtConfig
            ) |> ignore
        services.AddControllers(fun options ->
            let policy = AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()
            
            ()            
        ) |> ignore
        services.AddAuthorization(fun options -> 
            options.AddPolicy("Delete", fun b -> 
                                            b.RequireClaim("FooBar") |> ignore
                                            ()
                                        )
            ()
        ) |> ignore
        services.AddSingleton<IAuthorizationHandler, MyAuthorizationHandler>() |> ignore
        services.AddSingleton<IAuthorizationHandler, MyAuthorizationHandler2>() |> ignore
        

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
        
        app.UseHttpsRedirection() |> ignore
        app.UseRouting() |> ignore
        
        app.UseAuthentication() |> ignore
        app.UseAuthorization() |> ignore


        app.UseEndpoints(fun endpoints ->
            endpoints.MapControllers() |> ignore
            ) |> ignore

    member val Configuration : IConfiguration = null with get, set
