# Untappd.NetCore

.Net Core Implementation of Tommy Parnell's Untappd .NET API https://github.com/TerribleDev/Untappd.Net


## API Coverage

Current both Authenticated and Unauthenticated requests.

More info: https://untappd.com/api/docs

For Authenticated requests, you should already have a valid token, provided via OAuth authentication.

Such authentication can be achieved using Owin OAuth Providers for ASP.NET Web Applications, which already have an Untappd provider.

More info: https://github.com/RockstarLabs/OwinOAuthProviders

## How do I use?

* Request an [API Key](https://untappd.com/api/register?register=new)
* You should be able to make a repository and call the get method with the thing you are requesting.

Note: Additional parameters can be passed into the Get Method with an IDictionary.

```csharp

var ts = new UnAuthenticatedUntappdCredentials("key", "secret");
var t = new Repository().Get<UserDistinctBeers>(ts, "tparnell");
var t = new Repository().Get<BeerInfo>(ts, "BeerIdHere");

```

For Authenticated requests:

```csharp

var ts = new AuthenticatedUntappdCredentials("token");
var t = new Repository().Get<ActivityFeed>(ts);

```

For Actions (usually post requests). Note: Actions return a dynamic object. Usually these responses are not needed, and you should still be able to use the dynamic object's data. If strong typed returns is required feel free to file an issue. However we don't predict people will really need to care about the returns of these actions.


```csharp

var ts = new AuthenticatedUntappdCredentials("token");
var checkin = new CheckIn("-5", "EST", 1044097) { Shout = "Awesome Brew", Rating = 4 };
var response = repository.Post(ts, checkin);

```

## Contributing

* Everyone is welcome to contribute!
* There are no special instructions, submit pull requests against the master branch.

Current contributors:
* Adam McIntosh (https://github.com/AdamMcIntosh)
