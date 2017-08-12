# Quack.Net #
Quack.Net is a .NET library for interacting with DuckDuckGo's Instant Answer API.
## Requirements ##
Your project must target a framework compatible with .Net Standard 1.3. See [Microsoft's guide on .Net Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) for more information.
## Installation ##
Quack.Net can be installed through Nuget:
```ps1
PM> Install-Package Quack.Net
```
## Documentation ##
There is no online documentation, but the library's XML documentation is sufficient to understand specific properties and methods, and I've provided snippets for getting started:
```cs
// Everything in the library is contained in this namespace
using DuckDuckGo;
...
// You can pass an empty string or null to this constructor, but DuckDuckGo prefers you give a descriptive name
var client = new DuckDuckGoClient("MyApplication"); 
var appleInstantAnswer = await client.GetInstantAnswerAsync("apple");
// Because "apple" returns a disambiguation, we can cast to DisambiguationAnswer
var disambiguationAppleAnswer = appleInstantAnswer as DisambiguationAnswer;
```
Both methods on `DuckDuckGoClient` are async, Task-returning methods and should be awaited wherever possible.

`DisambiguationAnswer`s have a property `DisambiguationGroups`, which each contain a `Name` and array of `Topic`s. For instance, the query above will contain a group of the name "Botany" containing, among others, a topic about Malus, a genus of apple trees.

```cs
// Returns "http://www.imdb.com/find?s=all&q=rushmore"
var redirectUrl = await client.BangAsync("!imdb rushmore");
```
The `BangAsync()` method returns the appropriate redirect for the given !bang command and query. See [DuckDuckGo's page](https://duckduckgo.com/bang) for more information.