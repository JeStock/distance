# Places service

## Tech stack
1. ASP.NET Rest Api
2. (_Optional_) gRPC for service-to-service communication
3. Elasticsearch

## TODO
1. Add domain layer which will be responsible to build a search query for airport by the following fields: Name, IataCode, IcaoCode
2. Add Airport domain model
3. Add domain model processing, validation and mapping during populating elasticsearch
4. Add an endpoint which will accept the query as plain text
5. Add unit tests
6. (Optional) add integration tests
7. (Optional) add PureDI
8. (Optional) add logging + Serilog + ELK
9. (Optional) add health checks + ELK
10. Use Elasticsearch Geospatial data for Lat/Lon.