

# Table of contents
1. [List of operations] (#listoperations)
2. [Parking start](#startparking)
3. [Parking stop](#stopparking)
4. [Get running parking session](#getactivesession)
5. [Start parking session with known endtime](#startbytime)
6. [Extend running parking session](#extendparking)
7. [End running parking session](#endparking)
8. [Get price of desired parking action](#calculateprice)
9. [Get zone information](#getzoneinfooperatorlocation)
10. [Get zone infromation by GPS coordinates](#getzoneinfogps)
11. [Get extensive zone information](#getzoneInfo)


# List of Operations <a name="listoperations"></a>
URI: https://hackatrain.parknowportal.com/

| Operation | Method | Description | 
| ------ | ------ | ------ |
| /parking/start                                      | POST   | Starts a parking action |
| /parking/stop                                       | PUT    | Stops a parking action |
| /parking/{id}                                       | GET    | Get an existing parking action |
| /parking/register                                   | POST   | Register a parking right |
| /parking/extend/{id}                                | PUT    | Extend an existing parking right |
| /parking/end/{id}                                   | PUT    | End an existing parking right |
| /parking/cancel/{id}                                | DELETE | Cancel an existing parking right |
| /rates/calculateprice                               | POST | Retrieves location information, tariffs and calculated prices  |
| /inventory/GetLocationByCode/{locationCode}         | GET | Retrieves location information by code  |
| /inventory/GetLocationByLatLon/{Lat}/{Lon}?radius=5 | GET | Retrieves location information by latitude and longtitude  |


 
# POST /parking/start <a name="startparking"></a>

### Description
Starts a parking action until it stopped or until maximum time reached. Maximum time comes from zone settings.

### Parameters
| Type | Name | Required | Description | Schema | Default | 
| ------ |  ------ | ------ | ------ | ------ | ------ |
| Header | x-api-key               | Yes | authorization using x-api-key | string | "Bearer "
| Body   | startParkingRequest     | Yes | The start parking request     | StartParkingRequest |
 

### Responses

| HTTP Code | Description | Schema | 
| --------- | ----------- | ------ |
| 201 | A parking action started successfully. | ParkingResponse
| 400 | <ul><li>Validation errors → Error code: 2</li><li>Parkingright service validation errors → Error code: 4</li><li>Find location service Validation error → Error code: 23</li><li>PriceCalculation service validation errors → Error code: 11</li></ul> | ErrorResponse
| 401 | Given X-Api-Key is not a valid | ErrorResponse
| 403 | Given X-Api-Key does not contain required scopes to consume this resource | ErrorResponse
| 500 | <ul><li>Internal server error when processing the request → Error code : 1</li><li>Parkingright service internal server error → Error code : 3</li><li>Find location service internal server error → Error code : 5</li><li>PriceCalculation service internal server error → Error code : 12</li></ul> | ErrorResponse

### Consumes
- application/json
- text/json

### Produces 
- application/json
- text/json

### Example HTTP request 
Request body
```JSON
{
  "startDate": "2019-06-05T15:35:09.699Z",
  "licensePlate": "PN-UJB-HC",
  "licensePlateCountry": "DE",
  "locationCode": "100031",
  "externalReferenceId": "HC-1559748909699",
  "extraProperties": {
        "uniqueid": "unique001",
        "residenceAB": "true"
    }
  "amountCurrency": "EUR"
}
```

### Example HTTP response
Response 201
```JSON
{
    "parkingrightId": 5016216102,
    "licensePlate": "PN-UJB-HC",
    "licensePlateStripped": "PNUJBHC",
    "startDate": "2019-06-05T15:35:09.699Z",
    "endDate": "2019-06-05T17:59:59Z",
    "maxEndDate": "2019-06-05T17:59:59Z",
    "amount": 0,
    "vatPercentage": 0,
    "poiId": 65439,
    "profile": null,
    "externalReferenceId": "HC-1559748909699",
    "extraProperties": {
        "uniqueid": "unique001",
        "residenceAB": "true",
        "locationCode": "100031",
        "licensePlateCountry": "DE",
        "amountCurrency": "EUR"
    },
    "totalPayedMinutes": 0
}
````

Response 400: Error code 23
```` JSON
{
    "errors": [
        {
            "code": 23,
            "message": "Location information not found for given zone code invalidZoneCode12"
        }
    ],
    "code": 23,
    "message": "Location information not found for given zone code invalidZoneCode12"
}
````

Response 400: Error code 2
```` JSON
{
    "errors": [
        {
            "field": "startParkingRequest.LicensePlate",
            "message": "LicensePlate must be a string with a maximum length of '50'."
        }
    ],
    "code": 2,
    "message": "Validation errors occurred when creating parkingright."
}
````


# PUT /parking/stop/{id} <a name="stopparking"></a>

### Description
Stops parking action for an existing parking action.

### Parameters
| Type | Name | Required | Description | Schema | Default | 
| ------ |  ------ | ------ | ------ | ------ | ------ |
| Header | x-api-key               | Yes | authorization using x-api-key | string | "Bearer "
| Path   | id                      | Yes | Id of the paking action       | long   | 512045
| Body   | stopParkingRequest      | Yes | The stop parking request     | StopParkingRequest |
 

### Responses

| HTTP Code | Description | Schema | 
| --------- | ----------- | ------ |
| 200 | A parking action stopped successfully. | ParkingResponse
| 400 | <ul><li>Validation error → Error code: 21</li><li>Parkingright not found → Error code: 13</li><li>Parkingright service validation errors → Error code: 8</li><li>PriceCalculation service validation errors → Error code : 11</li></ul> | ErrorResponse
| 401 | Given X-Api-Key is not a valid | ErrorResponse
| 403 | Given X-Api-Key does not contain required scopes to consume this resource | ErrorResponse
| 500 | <ul><li>Internal server error when processing the request → Error code : 1</li><li>Parkingright service internal server error → Error code : 14</li><li>PriceCalculation service internal server error → Error code : 12</li></ul> | ErrorResponse

### Consumes 
- application/json
- text/json

### Produces 
- application/json
- text/json

### Example HTTP request 
Request body
```JSON
{
  "endDate" : "2019-06-05T15:56:25.212Z"
}
```

### Example HTTP response
Response 200
```JSON
{
    "parkingrightId": 5016216105,
    "licensePlate": "PN-L3M-HC",
    "licensePlateStripped": "PNL3MHC",
    "startDate": "2019-06-05T15:55:56.986Z",
    "endDate": "2019-06-05T15:56:25.212Z",
    "maxEndDate": "2019-06-05T17:59:59Z",
    "amount": 0.05,
    "vatPercentage": 0,
    "poiId": 65393,
    "profile": null,
    "externalReferenceId": "HC-1559750156986",
    "extraProperties": {
        "uniqueid": "unique001",
        "residenceAB": "true",
        "locationCode": "100032",
        "licensePlateCountry": "DE",
        "amountCurrency": "EUR"
    },
    "totalPayedMinutes": 1
}
````

Response 400: Validation error code 21
```` JSON
{
    "errors": [
        {
            "code": 1113,
            "message": "The requested parkingright is already stopped."
        }
    ],
    "code": 21,
    "message": "Validation errors occurred when stopping parkingright"
}
````


# GET /parking/{id} <a name="getactivesession"></a>


### Description
Returns an existing parking action.

### Parameters
| Type | Name | Required | Description | Schema | Default | 
| ------ |  ------ | ------ | ------ | ------ | ------ |
| Header | x-api-key               | Yes | authorization using x-api-key | string | "Bearer "
| Path   | id                      | Yes | Id of the paking action       | long   | 512045 


### Responses

| HTTP Code | Description | Schema | 
| --------- | ----------- | ------ |
| 200 | Parkingright found. | GetParkingrightResponse
| 400 | Parkingright not found | ErrorResponse
| 401 | Given X-Api-Key is not a valid token | ErrorResponse
| 403 | Given X-Api-Key does not contain required scopes to consume this resource | ErrorResponse
| 400 | <ul><li>Validation error → Error code: 2</li></ul> | ErrorResponse
| 500 | Internal server error when processing the request -> Error code : 1 | ErrorResponse

### Consumes 
- application/json
- text/json

### Produces 
- application/json
- text/json


### Example HTTP response
Response 200
```JSON
{
    "parkingrightId": 5016216106,
    "poiId": 65437,
    "operatorId": 49101151,
    "systemId": 294,
    "licensePlate": "PN-EJO-HC",
    "licensePlateStripped": "PNEJOHC",
    "createDate": "2019-06-05T16:02:28.829483Z",
    "modifyDate": "2019-06-05T16:02:33.001322Z",
    "cancelDate": null,
    "startDate": "2019-06-05T16:02:28.506Z",
    "endDate": "2019-06-05T16:02:32.407Z",
    "maxEndDate": "2019-06-05T19:59:59Z",
    "amount": 0.05,
    "vatPercentage": 0,
    "identifier": "PN-EJO-HC",
    "externalReferenceId": "HC-1559750548506",
    "profile": null,
    "paidMinutes": 0,
    "extraProperties": {
        "uniqueid": "unique001",
        "residenceAB": "true",
        "locationCode": "100051",
        "licensePlateCountry": "DE",
        "amountCurrency": "EUR"
    }
}
````

Response 400: Validation error code 2
```` JSON
{
    "errors": [
        {
            "code": 1107,
            "message": "No parkingright found"
        }
    ],
    "code": 2,
    "message": "Validation errors occurred when getting parkingright."
}
````


Response 500
```` JSON
{
  "application/json" : {
    "errors" : [ ],
    "code" : 1,
    "message" : "The request could not be processed at this time, please try again later",
    "technicalInfo" : null
  }
}
````

# POST /parking/register <a name="startbytime"></a>

### Description
Creates a parking right with specified period of time and amount.

### Parameters
| Type | Name | Required | Description | Schema | Default | 
| ------ |  ------ | ------ | ------ | ------ | ------ |
| Header | x-api-key               | Yes | authorization using x-api-key | string | "Bearer "
| Body   | parkingrightRequest     | Yes | The parking right request     | RegisterParkingrightRequest |
 

### Responses

| HTTP Code | Description | Schema | 
| --------- | ----------- | ------ |
| 201 | Parking right is successfully created. | RegisterParkingrightResponse
| 400 | * Validation error when processing the request -> Error code : 2 | ErrorResponse
| 401 | Given X-Api-Key or Oauth Token is not a valid token | ErrorResponse
| 403 | Given X-Api-Key or Oauth Token does not contain required scopes to consume this resource | ErrorResponse
| 409 | Duplicate request, existing parkingright is returned | ErrorResponse
| 500 |* Internal server error when processing the request -> Error code : 1 | ErrorResponse

### Consumes 
- application/json
- text/json

### Produces 
- application/json
- text/json


### Example HTTP request
Request
```JSON

{
  "startDate": "2019-06-03T13:40:33.498Z",
  "endDate": "2019-06-03T15:40:33.498Z",
  "licensePlate": "PN-JJ3-HC",
  "licensePlateCountry": "DE",
  "amount": 6,
  "amountCurrency": "EUR",
  "vatPercentage": 15,
  "locationCode": "100011",
  "externalReferenceId": "HC-1559569233498",
  "profile": "VIS",
  "enforcementLocations": [
    "100011"
  ],
  "paidMinutes": 0,
  "extraProperties": {}
}
````


### Example HTTP response
Response 200
```JSON
{
    "parkingrightId": 5016215957,
    "poiId": 65385,
    "operatorId": 49101151,
    "systemId": 294,
    "licensePlate": "PN-JJ3-HC",
    "licensePlateStripped": "PNJJ3HC",
    "createDate": "2019-06-03T13:40:34.8226047Z",
    "modifyDate": null,
    "cancelDate": null,
    "startDate": "2019-06-03T13:40:33.498Z",
    "endDate": "2019-06-03T15:40:33.498Z",
    "maxEndDate": "2019-06-05T15:40:33.498Z",
    "amount": 6,
    "vatPercentage": 15,
    "identifier": "PN-JJ3-HC",
    "externalReferenceId": "HC-1559569233498",
    "profile": "VIS",
    "extraProperties": {
        "locationCode": "100011",
        "licensePlateCountry": "DE",
        "amountCurrency": "EUR",
        "enforcementLocations": "[\"100011\"]"
    }
}
````

Response 400
```` JSON
{
  "application/json" : {
    "errors" : [ ],
    "code" : 6,
    "message" : "Validation errors occurred when getting location information",
    "technicalInfo" : null
  }
}
````


Response 409
```` JSON
{
  "application/json" : {
    "parkingrightId" : 1,
    "poiId" : 5,
    "operatorId" : 13,
    "systemId" : 0,
    "licensePlate" : "4-SXP-80",
    "licensePlateStripped" : "4SXP80",
    "createDate" : "2018-04-13T09:48:02.9602766Z",
    "modifyDate" : null,
    "cancelDate" : null,
    "startDate" : "2018-04-13T09:48:02.9602766Z",
    "endDate" : "2018-04-13T13:48:02.9602766Z",
    "maxEndDate" : "2018-04-13T13:48:02.9602766Z",
    "amount" : 4.76,
    "vatPercentage" : 21.0,
    "identifier" : "PERMIT-AC-12432",
    "externalReferenceId" : null,
    "profile" : null,
    "paidMinutes" : 0,
    "extraProperties" : { }
  }
}
````


Response 500
```` JSON
{
  "application/json" : {
    "errors" : [ ],
    "code" : 1,
    "message" : "The request could not be processed at this time, please try again later",
    "technicalInfo" : null
  }
}
````

# Extends parking session <a name="extendparking"></a>
PUT /parking/extend/{id}

### Description
Extends running parking action with new end time.


### Parameters
| Type | Name | Description | Schema | Default | 
| ------ | ------ | ------ | ------ | ------ |
| Header | Authorization optional | | string | "Bearer "
| Header | X-Trip-Id optioanl | | strign | "3244b22e-0bfd-4a89-ae13-1fc32d1920a1"
| Path | id required | Id of the parkingright to change. | integer |
| Body | parkingrightRequest required | The parking right request. | ExtendParkingrightRequestt | 


### Responses

| HTTP Code | Description | Schema | 
| --------- | ----------- | ------ |
| 204 | Parkingright extended. | 
| 401 | Given X-Api-Key or Oauth Token is not a valid token | ErrorResponse
| 403 | Given X-Api-Key or Oauth Token does not contain required scopes to consume this resource | ErrorResponse
| 500 |* Internal server error when processing the request -> Error code : 1 | ErrorResponse

### Consumes 
- application/json
- text/json

### Produces 
- application/json
- text/json


### Example HTTP request
Request
```JSON

{
  "EndDate": "2019-06-03T16:42:56.986Z",
  "Amount": 17,
  "VatPercentage": 15.0,
  "PaidMinutes": null
}
````


Response 500
```` JSON
{
  "application/json" : {
    "errors" : [ ],
    "code" : 1,
    "message" : "The request could not be processed at this time, please try again later",
    "technicalInfo" : null
  }
}
````

# Ends an parking session <a name="endparking"></a>
PUT /parking/end/{id}

### Description
Ends running parking action.


### Parameters
| Type | Name | Description | Schema | Default | 
| ------ | ------ | ------ | ------ | ------ |
| Header | Authorization optional | | string | "Bearer "
| Header | X-Trip-Id optioanl | | strign | "3244b22e-0bfd-4a89-ae13-1fc32d1920a1"
| Path | id required | Id of the parkingright to change. | integer |
| Body | parkingrightRequest required | The parking right request. | ChangeParkingrightRequest |


### Responses

| HTTP Code | Description | Schema | 
| --------- | ----------- | ------ |
| 204 | Parkingright extended. | 
| 401 | Given X-Api-Key or Oauth Token is not a valid token | ErrorResponse
| 403 | Given X-Api-Key or Oauth Token does not contain required scopes to consume this resource | ErrorResponse
| 500 |* Internal server error when processing the request -> Error code : 1 | ErrorResponse

### Consumes 
- application/json
- text/json

### Produces 
- application/json
- text/json


### Example HTTP request
Request
```JSON

{
  "EndDate": "2019-06-03T16:42:56.986Z",
  "Amount": 17,
  "VatPercentage": 15.0,
  "PaidMinutes": null
}
````


Response 500
```` JSON
{
  "application/json" : {
    "errors" : [ ],
    "code" : 1,
    "message" : "The request could not be processed at this time, please try again later",
    "technicalInfo" : null
  }
}
````

# Get zone info with max parking time and prices <a name="calculateprice"></a>
POST /rates/calculateprice

### Description
Ends running parking action.


### Parameters
| Type | Name | Description | Schema | Default | 
| ------ | ------ | ------ | ------ | ------ |
| Header | Authorization optional | | string | "Bearer "
| Header | X-Trip-Id optioanl | | strign | "3244b22e-0bfd-4a89-ae13-1fc32d1920a1"
| Body | ratesCalculatePriceRequest required | The parking right request. | RatesCalculatePriceRequest |



### Responses

| HTTP Code | Description | Schema | 
| --------- | ----------- | ------ |
| 200 | Success. Price and zone inforamtion returned. | 
| 401 | Given X-Api-Key or Oauth Token is not a valid token | ErrorResponse
| 403 | Given X-Api-Key or Oauth Token does not contain required scopes to consume this resource | ErrorResponse
| 500 |* Internal server error when processing the request -> Error code : 1 | ErrorResponse

### Consumes 
- application/json
- text/json

### Produces 
- application/json
- text/json


### Example HTTP request
Request
```JSON

{
  "operatorId": 49101151,
  "signageCode": "100410",
  "startTime": "2019-06-04T08:29:09.458Z",
  "endTime": "2019-06-04T10:29:09.458Z",
  "timeZoneName": "Europe/Berlin",
  "countryCode": "DE",
  "timeBlockUnit": 1,
  "timeBlockQuantity": 120,
  "identifications": [
    {
      "identityType": 1,
      "identityValue": "VIS"
    }
  ],
  "attributes": [],
  "extraFields": []
}
````


Response 200
```` JSON
{
    "success": true,
    "parkingAllowed": true,
    "errorCode": null,
    "message": null,
    "signageCode": "100410",
    "durationOption": "DurationNotAvailable",
    "stoppable": false,
    "startTime": "2019-06-04T07:00:00Z",
    "endTime": "2019-06-04T09:00:00Z",
    "maxEndTimeType": "StopAtend",
    "maxEndTime": "2019-06-04T21:59:59Z",
    "timeZoneName": "Europe/Berlin",
    "maxDuration": [
        {
            "timeBlockUnit": "Day",
            "timeBlockQuanity": 0
        },
        {
            "timeBlockUnit": "Hour",
            "timeBlockQuanity": 15
        },
        {
            "timeBlockUnit": "Minute",
            "timeBlockQuanity": 30
        }
    ],
    "calculatedRates": [
        {
            "type": "BlockRate",
            "identification": null,
            "amount": "2.00",
            "vatIncluded": true,
            "vatAmount": "0.00",
            "vatPercentage": "0.00",
            "formattedAmount": "2,00 €",
            "formattedVatAmount": "0,00 €",
            "formattedVatPercentage": "0,00 %",
            "currencySymbol": "€",
            "parts": null
        }
    ],
    "tariffs": [
        {
            "feeUnit": "Minute",
            "parkingOption": "CustomMinutes",
            "dayType": "Specialday",
            "startTime": 0,
            "endTime": 1439,
            "incrementFee": 0,
            "initFee": 0,
            "maxParkingTime": 1440,
            "lowerBound": 0,
            "upperBound": 999,
            "hourlyFee": 0,
            "stopAtEnd": false,
            "eligibilityProfile": null
        },
        {
            "feeUnit": "Minute",
            "parkingOption": "CustomMinutes",
            "dayType": "MondaySaturday",
            "startTime": 540,
            "endTime": 1439,
            "incrementFee": 5,
            "initFee": 0,
            "maxParkingTime": 1440,
            "lowerBound": 0,
            "upperBound": 999,
            "hourlyFee": 100,
            "stopAtEnd": true,
            "eligibilityProfile": null
        }
    ],
    "totalPaidTimeUnit": "Minute",
    "totalPaidTimeQuantity": 120,
    "attributes": [],
    "pollutionDays": []
}
````

Response 500
```` JSON
{
  "application/json" : {
    "errors" : [ ],
    "code" : 1,
    "message" : "The request could not be processed at this time, please try again later",
    "technicalInfo" : null
  }
}
````

# Gets zone information <a name="getzoneinfooperatorlocation"></a>
GET rates/zoneinfo/{{operatorId}}/{{locationCode}}

### Description
Ends running parking action.


### Parameters
| Type | Name | Description | Schema | Default | 
| ------ | ------ | ------ | ------ | ------ | 
| Header | Authorization optional | | string | "Bearer "
| Header | X-Trip-Id optioanl | | strign | "3244b22e-0bfd-4a89-ae13-1fc32d1920a1"
| Path | operatorId required | Operator id (Berlin). | string |  49101151
| Path | locationCode required | Location code of the zone. | string |


### Responses

| HTTP Code | Description | Schema | 
| --------- | ----------- | ------ |
| 200 | Zone infromation found and returned. | 
| 401 | Given X-Api-Key or Oauth Token is not a valid token | ErrorResponse
| 403 | Given X-Api-Key or Oauth Token does not contain required scopes to consume this resource | ErrorResponse
| 404 | Not found | ErrorResponse
| 500 |* Internal server error when processing the request -> Error code : 1 | ErrorResponse

### Consumes 
- application/json
- text/json

### Produces 
- application/json
- text/json


Response 200
```JSON

{
    "success": true,
    "parkingAllowed": true,
    "errorCode": null,
    "message": null,
    "signageCode": "100015",
    "durationOption": "DurationNotAvailable",
    "stoppable": false,
    "startTime": "2019-06-04T09:04:53.0418134Z",
    "endTime": "2019-06-04T23:59:59.059Z",
    "maxEndTimeType": "StopAtend",
    "maxEndTime": "2019-06-04T21:59:59Z",
    "timeZoneName": "UTC",
    "maxDuration": [
        {
            "timeBlockUnit": "Day",
            "timeBlockQuanity": 0
        },
        {
            "timeBlockUnit": "Hour",
            "timeBlockQuanity": 13
        },
        {
            "timeBlockUnit": "Minute",
            "timeBlockQuanity": 0
        }
    ],
    "calculatedRates": null,
    "tariffs": [
        {
            "feeUnit": "Minute",
            "parkingOption": "CustomMinutes",
            "dayType": "Specialday",
            "startTime": 0,
            "endTime": 1439,
            "incrementFee": 0,
            "initFee": 0,
            "maxParkingTime": 1440,
            "lowerBound": 0,
            "upperBound": 999,
            "hourlyFee": 0,
            "stopAtEnd": false,
            "eligibilityProfile": null
        },
        {
            "feeUnit": "Minute",
            "parkingOption": "CustomMinutes",
            "dayType": "MondaySaturday",
            "startTime": 540,
            "endTime": 1320,
            "incrementFee": 5,
            "initFee": 0,
            "maxParkingTime": 1440,
            "lowerBound": 0,
            "upperBound": 999,
            "hourlyFee": 100,
            "stopAtEnd": true,
            "eligibilityProfile": null
        }
    ],
    "totalPaidTimeUnit": null,
    "totalPaidTimeQuantity": null,
    "attributes": [],
    "pollutionDays": null
}
````

Response 500
```` JSON
{
  "application/json" : {
    "errors" : [ ],
    "code" : 1,
    "message" : "The request could not be processed at this time, please try again later",
    "technicalInfo" : null
  }
}
````

# Gets zone information <a name="getzoneinfogps"></a>
GET inventory/GetLocationByLatLon/{lat}/{lon}?radius={rad}

### Description
Gets zone info by GPS coordinates


### Parameters
| Type | Name | Description | Schema | Default | 
| ------ | ------ | ------ | ------ | ------ |
| Header | Authorization optional | | string | "Bearer "
| Header | X-Trip-Id optioanl | | strign | "3244b22e-0bfd-4a89-ae13-1fc32d1920a1"
| Path | lat required | Lattitude. | string |  
| Path | lon required | Longtitude. | string |  
| Path | rad optional | Radius to search pased on provided lattitude and longtitude. | string | 


### Responses

| HTTP Code | Description | Schema | 
| --------- | ----------- | ------ |
| 200 | Success zone information returned. | 
| 401 | Given X-Api-Key or Oauth Token is not a valid token | ErrorResponse
| 403 | Given X-Api-Key or Oauth Token does not contain required scopes to consume this resource | ErrorResponse
| 404 | Not found | 
| 500 |* Internal server error when processing the request -> Error code : 1 | ErrorResponse

### Consumes 
- application/json
- text/json

### Produces 
- application/json
- text/json


Response 200
```JSON
{
    "poiCollection": [
        {
            "id": 65387,
            "name": "Berlin - Mitte",
            "description": "Berlin - Mitte",
            "signageCode": "100021",
            "poiTypeId": 1,
            "poiUsageTypeId": 1,
            "propertyTypeId": 1,
            "primaryBrandId": 349,
            "primaryBrandKeyId": 1,
            "operatorId": 49101151,
            "statusId": 4,
            "poiSpaceCollection": [
                {
                    "id": 5439,
                    "poiId": 65387,
                    "name": "Berlin - Mitte",
                    "description": "Berlin - Mitte",
                    "isLabelled": false,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1,
                    "gisCollection": [
                        {
                            "id": 79359,
                            "gisTypeId": 1,
                            "gisSubTypeId": 1,
                            "latitude": 52.520042,
                            "longitude": 13.395834,
                            "streetViewEnabled": false,
                            "distance": 0,
                            "poiId": 65387,
                            "spaceId": 5439,
                            "operatorId": 49101151
                        }
                    ]
                }
            ],
            "attributeCollection": [
                {
                    "id": 250485,
                    "name": "tariffAreaId",
                    "configId": 563,
                    "value": "850",
                    "visibilityId": 1
                },
                {
                    "id": 250487,
                    "name": "Parking Mode",
                    "description": "Identifies whether the parking is initiated by buying time (duration) or through a start-stop approach",
                    "configId": 12,
                    "value": "4",
                    "visibilityId": 1
                },
                {
                    "id": 250489,
                    "name": "tariffAreaGeoId",
                    "configId": 565,
                    "value": "0",
                    "visibilityId": 1
                },
                {
                    "id": 250491,
                    "name": "Stoppable",
                    "configId": 567,
                    "value": "True",
                    "visibilityId": 1
                }
            ]
        }
    ]
}
````

Response 500
```` JSON
{
  "application/json" : {
    "errors" : [ ],
    "code" : 1,
    "message" : "The request could not be processed at this time, please try again later",
    "technicalInfo" : null
  }
}
````


# Get zone information <a name="getzoneInfo"></a>
GET inventory/getlocationbycode/{{locationcode}}

### Description
Returns extensive zone information. With all subzones.


### Parameters
| Type | Name | Description | Schema | Default | 
| ------ | ------ | ------ | ------ | ------ |
| Header | Authorization optional | | string | "Bearer "
| Header | X-Trip-Id optioanl | | strign | "3244b22e-0bfd-4a89-ae13-1fc32d1920a1"
| Path | locationcode required | Location code of the zone. | string |  


### Responses

| HTTP Code | Description | Schema | 
| --------- | ----------- | ------ |
| 200 | Success zone information returned. | 
| 401 | Given X-Api-Key or Oauth Token is not a valid token | ErrorResponse
| 403 | Given X-Api-Key or Oauth Token does not contain required scopes to consume this resource | ErrorResponse
| 500 |* Internal server error when processing the request -> Error code : 1 | ErrorResponse

### Consumes 
- application/json
- text/json

### Produces 
- application/json
- text/json


Response 200
```JSON
{
    "poiCollection": [
        {
            "id": 65385,
            "name": "Berlin - Charlottenburg-Wilmersdorf",
            "description": "Berlin - Charlottenburg-Wilmersdorf",
            "signageCode": "100011",
            "poiTypeId": 1,
            "poiUsageTypeId": 1,
            "propertyTypeId": 1,
            "primaryBrandId": 349,
            "primaryBrandKeyId": 1,
            "operatorId": 49101151,
            "statusId": 4,
            "poiSpaceCollection": [
                {
                    "id": 5393,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1,
                    "gisCollection": [
                        {
                            "id": 79219,
                            "gisTypeId": 1,
                            "gisSubTypeId": 1,
                            "latitude": 52.496512,
                            "longitude": 13.337249,
                            "streetViewEnabled": false,
                            "spaceId": 5393
                        }
                    ]
                },
                {
                    "id": 5399,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5407,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5417,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5421,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5423,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5425,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5427,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5449,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5465,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5477,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5485,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5493,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5497,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5501,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5505,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5507,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5513,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5525,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5531,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5557,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5571,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5585,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5599,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5615,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5625,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5633,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5639,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5645,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5657,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5693,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5707,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5719,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5735,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5755,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5763,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5765,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5785,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5787,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5797,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5815,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5837,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5851,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5863,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5873,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5883,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5923,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5971,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 5981,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6007,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6017,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6043,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6063,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6081,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6083,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6095,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6103,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6117,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6153,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6181,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6191,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6225,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6243,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6245,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6251,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6259,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6271,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6281,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6303,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6313,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6319,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6359,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6363,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6379,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6393,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6425,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6433,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6451,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6471,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6485,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6509,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6557,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6565,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6581,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6583,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6595,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6601,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6627,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6639,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6643,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6651,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6679,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6687,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6711,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6733,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6741,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6751,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6775,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6811,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6819,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6843,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6851,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6871,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6875,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6891,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6895,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6921,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6925,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6939,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 6963,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7011,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7021,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7041,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7049,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7063,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7075,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7077,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7083,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7119,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7165,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7215,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7225,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7249,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7287,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7301,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7329,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7331,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7337,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7395,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7445,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7447,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7473,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7481,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7489,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7491,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7537,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7569,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7619,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7637,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7679,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7691,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7693,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7701,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7745,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7749,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7759,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7801,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7805,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7807,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7809,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7811,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7823,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7825,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7831,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7839,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7841,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7869,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7871,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7887,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7905,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7911,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 7997,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8007,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8011,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8035,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8121,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8131,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8169,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8177,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8189,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8205,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8215,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8231,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8265,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8269,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8283,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8301,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8309,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8317,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8319,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8321,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8345,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8361,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8369,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8373,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8381,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8395,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8399,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8403,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8409,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8411,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8413,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8421,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8425,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8431,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8437,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8441,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8449,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8457,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8473,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8491,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8495,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8507,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8511,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8515,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8525,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8539,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8615,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8653,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8713,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8733,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8739,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8741,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8751,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8753,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8769,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8785,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8791,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8793,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8803,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8805,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8809,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8811,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8817,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8823,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8825,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8827,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8841,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8849,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8851,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8857,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8881,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8891,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8895,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8897,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8901,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8915,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8921,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8927,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8933,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8945,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8953,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8955,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8959,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8961,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8965,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8971,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 8989,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9003,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9005,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9009,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9013,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9017,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9019,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9025,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9029,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9037,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9041,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9043,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9045,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9049,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9055,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9063,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9065,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9067,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9075,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9081,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9085,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9087,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9091,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9099,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9103,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                },
                {
                    "id": 9109,
                    "poiId": 65385,
                    "name": "Berlin - Charlottenburg-Wilmersdorf",
                    "description": "Berlin - Charlottenburg-Wilmersdorf",
                    "isLabelled": true,
                    "spaceTypeId": 1,
                    "statusId": 4,
                    "numberOfSpaces": 1
                }
            ],
            "attributeCollection": [
                {
                    "id": 250533,
                    "name": "tariffAreaId",
                    "configId": 563,
                    "value": "849",
                    "visibilityId": 1
                },
                {
                    "id": 250535,
                    "name": "Parking Tips Enabled",
                    "configId": 569,
                    "value": "False",
                    "visibilityId": 1
                },
                {
                    "id": 250537,
                    "name": "Stoppable",
                    "configId": 567,
                    "value": "True",
                    "visibilityId": 1
                },
                {
                    "id": 250539,
                    "name": "tariffAreaGeoId",
                    "configId": 565,
                    "value": "0",
                    "visibilityId": 1
                },
                {
                    "id": 250541,
                    "name": "Parking Mode",
                    "description": "Identifies whether the parking is initiated by buying time (duration) or through a start-stop approach",
                    "configId": 12,
                    "value": "4",
                    "visibilityId": 1
                }
            ]
        }
    ]
}
````

Response 500
```` JSON
{
  "application/json" : {
    "errors" : [ ],
    "code" : 1,
    "message" : "The request could not be processed at this time, please try again later",
    "technicalInfo" : null
  }
}
````
