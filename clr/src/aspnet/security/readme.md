

# Overview

- Authentication - if user identified and what claims for authorization does he have
- Authenticatin - who the user is

## Authorization 

Authorization depends on Authentication

- what user is allowed to do accoding his identity and claims 
    - allow
    - challenge (with redirect)
    - forbid actions
- Data protection
- HTTPS enforcements
- App secrets (store secrets without exposing in secrets code)
- Anti-request forgery
- CORS
- Indetity providers 

## Approaches

- roles - 
-- I am allowed to do this
-- kinds: identity role 
-- authorization role (must be cryptocraphy to enforce renew)

- permissions
-- Entity A is allowed to do X
- resource-based
-- how object to act upon provided to authorization
- ACL 
- variants


## OpenID

## OAUth


### Flow 

#### Front channel (in browseer, less secure)
1. example.com connect via foobar.com/login (with Callback and what Grants)
   1.1 Scopes(Data Domain) and Consets(Operation)
2. Redirected to foobar.com
3. Promt so example.com wants to access foobar.com FizBuzz
4. foobar.com calls example.com/callback (with Authorization code)
   
#### Back channel (secure channel)
5. example.com calls foobar.com with  Authorization code to get Access token (with Secret Key only server knows)
6. Got to fizzbuss.foobar.com with Access Token

### Terminologu

- Resourse Owner - real person  (who clicks the button)
- Client - application (example.com)
- Athorization Server (foobar.com)
- Resourse Server fizzbuzz.foobar.com
- Authorization grant - read fizzbuzz.foobar.com YES
- Redirect URI - callback
- Client gets Access token and calls fizzbuzz.foobar.com

## Appliance

- commands
- queries

## Patters

- delegation
- trusted sub system

## Architecture

Authrization provider knows about APIs so can profied different stuff to UI or to API by Identity.

With Admin API (Managment) by Global Roles.

Client API for App Specific Roles + Roles to Permission mappings + resources and rules.

### Integration

Cache-Refresh-Call-TransfromPipeline

# Pipeline

1. Scheme = Configuration + Handler

2. Scheme |> service.Add

3. IApplicationBuilder.UseAuthentication

4. Mark with attributes (setup defaul schmes with OR logic)

5. Middleware -> IAuthenticationService -> Handler

6. Or diectt call with Requriemnte

## Ways to authenicate

### Simple or forms (classical)

(Name, Passord) -> Hash password -> Verify Hash -> Look up User Info -> Athorization Info -> Create  and sign Cookie -> Set-Cokkie with max Age

- minus downside and maintances

### Single sign-on

### Mobile

### Delegated authorization



# JSON Web Token

JWT(Json Web Token) - encrypted and signed JSON data.

JWT can contian key value pairs of claim name and value. Like email, name, etc. To authorize agains resource.

# ERD


user 1 -> * identity

identity -> * claim

## Complex example

We have Organization. 

Organization may have one or more Directories(Tenants).


Organization may have one or more Subscriptions.

Organization may have one or more Identities.

User may have one or more Identities.

Identity may have several Subscriptions.

Organization owns Identity.

Identity may be invited into other Organization and shared Directory(Tenant).

Identity subscription may be shared from other Organization.


# Graph IAm

- Which resources some users have some kind of access to?
- QUestions which needed traversal of tree
- Find your question then model graph
- Read model  to answer questions
- Graph is physled with resources description via queue
-  Conditional layered rules (custom traverse if on each step)
- User has Access-Role
- Autonomouse service
- Updated continously
- What put into?
- Several specific tress per froblem domain
- Replace User A Can Do X on Resource Y, but if Resource Y has action X by User A (reversal)
- ZigZag(U-turn) by adding state
- Many-To-Many
- Need additoonal store for Access Control List (resource to allow-reject)

## References

https://docs.microsoft.com/en-us/aspnet/core/security/authorization/secure-data?view=aspnetcore-3.1 | Create an ASP.NET Core app with user data protected by authorization | Microsoft Docs
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-3.1 | Policy-based authorization in ASP.NET Core | Microsoft Docs
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/secure-data?view=aspnetcore-3.1 | Create an ASP.NET Core app with user data protected by authorization | Microsoft Docs
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/secure-data?view=aspnetcore-3.1 | Create an ASP.NET Core app with user data protected by authorization | Microsoft Docs
https://www.google.com/search?safe=off&sxsrf=ALeKk01TpBDNbdwtf63onH-bgAeFAXmMcg%3A1584627196842&source=hp&ei=_H1zXpmKMYW53APmzIOoCw&q=add+role+into+jwt+token+asp.net+core&oq=add+role+into+jwt+token+asp&gs_l=psy-ab.3.0.33i22i29i30.136.6602..7791...2.0..0.259.4069.4j22j3......0....1..gws-wiz.......0j0i22i30j0i13i30j0i8i13i30j33i160j33i21.SGa4vG5rUaA | add role into jwt token asp.net core - Google Search
https://www.jerriepelser.com/blog/using-roles-with-the-jwt-middleware/ | Using Roles with the ASP.NET Core JWT middleware | Jerrie Pelser's Blog
https://www.jerriepelser.com/blog/using-roles-with-the-jwt-middleware/ | Using Roles with the ASP.NET Core JWT middleware | Jerrie Pelser's Blog
https://stackoverflow.com/questions/42036810/asp-net-core-jwt-mapping-role-claims-to-claimsidentity | c# - ASP.NET Core JWT mapping role claims to ClaimsIdentity - Stack Overflow
https://jasonwatmore.com/post/2019/01/08/aspnet-core-22-role-based-authorization-tutorial-with-example-api | ASP.NET Core 2.2 - Role Based Authorization Tutorial with Example API | Jason Watmore's Blog
https://jasonwatmore.com/post/2019/10/16/aspnet-core-3-role-based-authorization-tutorial-with-example-api | ASP.NET Core 3.1 - Role Based Authorization Tutorial with Example API | Jason Watmore's Blog
https://community.auth0.com/t/add-roles-to-the-access-token/32140 | Add roles to the access token - Auth0 Community
https://dotnetdetail.net/role-based-authorization-in-asp-net-core-3-0/ | Role Based Authorization in Asp.Net Core 3.0 – DOTNET DETAIL
https://docs.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt.jwtregisteredclaimnames?view=azure-dotnet | JwtRegisteredClaimNames Struct (System.IdentityModel.Tokens.Jwt) - Azure for .NET Developers | Microsoft Docs
