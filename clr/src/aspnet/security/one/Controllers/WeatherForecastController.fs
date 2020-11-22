namespace one.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Authentication
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.IdentityModel.Tokens
open System.IdentityModel.Tokens
open System.IdentityModel.Tokens.Jwt
open Microsoft.IdentityModel.JsonWebTokens
open System.Text
open Newtonsoft.Json
open one


[<CLIMutable>]
type Credentials = {
    Name:string
    Password:string
}

[<CLIMutable>]
type jwtToken = {
    token:string
}

[<ApiController>]
[<Route("[controller]")>]
type WeatherForecastController (logger : ILogger<WeatherForecastController>) =
    inherit ControllerBase()

    [<HttpPost>]
    [<AllowAnonymous>]
    [<Route("login")>]
    member __.Login([<FromBody>]credentials:Credentials) =
        
        let securityKey = SymmetricSecurityKey(Encoding.UTF8.GetBytes("VerySecretKeyVerySecretKeyVerySecretKey"))
        let credentials = SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        let expires = DateTime.UtcNow.AddMinutes(123.0) |> Nullable
        let token = JwtSecurityToken(
                        "lahoda-pro-issuer", 
                        "lahoda-pro-audience",
                        claims = null,
                        expires =  expires,
                        signingCredentials = credentials
            )

        let tokenString = JwtSecurityTokenHandler().WriteToken(token)
        logger.LogCritical(tokenString)
        __.Ok({token = tokenString})

    [<HttpGet>]
    [<Authorize>]
    [<Route("authorized")>]
    member __.Authorized() = async {
        let! user = __.HttpContext.AuthenticateAsync() |> Async.AwaitTask
        logger.LogCritical(
            "{Properties}{TicketScheme}", 
            JsonConvert.SerializeObject(user.Properties),
            JsonConvert.SerializeObject(user.Ticket.AuthenticationScheme))
        return 42
    }

    [<HttpGet>]
    [<AllowAnonymous>]
    [<Route("anonymous")>]
    member __.Anonymous() =
        123
