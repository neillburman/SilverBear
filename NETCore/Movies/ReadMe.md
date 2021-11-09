## Further information
* The application runs http://localhost:5000 

#### The three urls of interest are and were made using postman

* GET: http://localhost:5000/movies/stats
* GET: http://localhost:5000/metadata/3
* POST http://localhost:5000/metadata

    Below is the message body
```
POST /metadata HTTP/1.1
Host: localhost:5000
Content-Type: application/json
Cache-Control: no-cache
Postman-Token: 8dae425c-ab66-571b-7ba3-049c86d5d95a
{
"movieId": 3,
"title": "Elysium",
"language": "EN",
"duration": "1:49:00",
"releaseYear": 2021
}
```

* Nuget package CsvHelper was used
* Data was retrieved using Linq
* Data retrieval was isolated in a repository
* DI was used to inject the respository into the Api controller in order
to faciliate any unit testing