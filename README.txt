
For testing InMemory database is used.
Tags are stored into separated table and connected to links with many-to-many relation. 
This will greatly improve search by tags.
External libriaries used:
mediatr for loose coupling of classes and potentialy later cqrs
fluentvalidation for validating input data
htmlagillitypack for parsing websites
htmlsanitizer for sanitizing input tags
swashbuckle for swagger documenting api
jwt for autentification

For testing and login use:
Email: symphony@gmail.com
Password: symphony

Steps: call autenticate method with these parameters and copy token. In swagger authorize button enter "Bearer {copied token}"
and authorize.
API's should return some test data that are seeded in memory.


