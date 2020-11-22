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

type MyAuthorizationHandler() =
    inherit AuthorizationHandler<OperationAuthorizationRequirement, Asset>()
    override this.HandleRequirementAsync(context, requirement, resource) = 
        printfn "FOOBARBAZ %A" resource
        let notSet = Unchecked.defaultof<Asset>
        match (context.User, resource, requirement.Name) with
        | (null, notSet, _) -> 
            Task.CompletedTask
        | (user, resource, "Delete") ->
            printfn "FOOBARBAZ %A" resource
            context.Succeed(requirement)
            Task.CompletedTask
        | (_, _, _) -> Task.CompletedTask
        
type MyAuthorizationHandler2() =
    interface IAuthorizationHandler with 
        member this.HandleAsync(context) =            
            printfn "FOASDADASDSADASOBARBAZ"
            Task.CompletedTask