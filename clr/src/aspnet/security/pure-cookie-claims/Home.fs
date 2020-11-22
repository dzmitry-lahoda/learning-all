
namespace pureclaims

open Microsoft.AspNetCore
open Microsoft.AspNetCore.Mvc
open Microsoft.AspNetCore.Authentication
open Microsoft.AspNetCore.Authorization
open System.Security.Claims


type Home() =
    inherit Controller()

    member this.Index() =
        "Hello world!"
    
    member this.authenticate() = async {
        let claims = [
            Claim(ClaimTypes.Name, "Bob")
            Claim(ClaimTypes.Email, "bob@fmail.com")
            Claim("I.Say", "Cool person") 
        ]
        let killer = [
            Claim("CanKill", "true")
        ]
        let identity = ClaimsIdentity(claims, "Head of Village")
        let killer = ClaimsIdentity(killer, "Secret.Service")
        let user = ClaimsPrincipal([identity; killer])
        do! this.HttpContext.SignInAsync(user) |> Async.AwaitTask
        return this.Ok()
    }

    [<Authorize>]
    member this.Secured() =
        "Authorized Hello World!"

    
    member this.Diagnostics() =
        this.View()