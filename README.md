# lnksnp
No frills link/URL shortener

Setup for deployment to docker - Just map an external port and you're off and running.

Example site: https://lnksnp.com


Exposed REST endpoints:

POST /api/LinkSnip - JSON Body: { "longLink": "[your long link]" } - generate a short link from a long link

GET /api/LinkSnip/{shortLink} - gets a long link given the short link

GET /api/LinkSnip/getLinks - gets all the links

GET /{shortLink} - gets a link and redirects. Default behaviour eg https://lnksnp.com/MW
