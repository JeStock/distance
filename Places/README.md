# Places service
Service responsible for 2 things
1. Parse the CSV file with airports' data and index all successfully parsed airports the Elasticsearch index.
2. Provide an API to get an info regarding different places of Earth. For simplicity, it's currently limiting by airports, but could be extended by any places which have (or associated to) a static geolocation: cities, ports, etc.
