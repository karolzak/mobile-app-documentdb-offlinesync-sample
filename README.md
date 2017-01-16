
# Azure Mobile Apps using DocumentDb + Incremental and online sync

This is a proof of concept project where we tight up a DocumentDB instance with complex (nested) types with several SQLite mobile clients using Azure Mobile Apps with Offline Sync. In this project we are trying to achieve following features:

- [x] Accessing data from DocumentDB on mobile clients through Azure Mobile Apps
- [x] Ability for mobile clients to work with downloaded data in offline mode
- [x] Incremental sync on client to download only updated/added/data (limits the data transfer)
- [x] Support for flat DocumentDB objects
- [ ] Support for more complex nested objects


Key points:
- [Custom DomainManager for TableController](https://github.com/karolzak/Mobile-App-DocumentDB-OfflineSync-Sample/blob/master/MobileAppDocDBOfflineSyncSample.API/Helpers/DocumentDBDomainManager.cs) - it allows us to sync data on client through Azure Mobile Apps Tables to work with DocumentDB objects
- Azure Mobile Apps client SDK with offline sync configuration uses "createdAt" and "updatedAt" attributes to determine which objects needs to be synced. [THESE ARE CASE SENSITIVE!!](https://github.com/karolzak/Mobile-App-DocumentDB-OfflineSync-Sample/blob/master/MobileAppDocDBOfflineSyncSample.API/Helpers/DocumentResource.cs) We learned it the hard way...
- At this point of time (16.01.2017), Azure Mobile Apps client SDK offline sync feature works only for flat objects. It's not suitable for more complex and nested types
- Best we could achieve was to implement offline sync for level 1 nested objects similar to this:
```JSON
{
 "text": "Mew task ios",
 "nested": {
   "nestedText": "144 J B Hazra Road",
   "nestedBool": "false"
 },
 "nestedItems": [
   {
     "nestedText": "xxoxoxoxoxox",
     "nestedBool": "true"
   }
 ],
 "complete": true,
 "id": "8d16700a-fcc0-4453-b740-e3eef8c0c340",
 "version": null,
 "createdat": "2017-01-11T12:27:24.1394782+00:00",
 "updatedat": "2017-01-11T14:24:03.4335527+00:00",
 "deleted": false
}
```
Unfortunately, nested objects/collections can only be stored as strings/JSON in SQLite store on mobile client: 
![alt text](https://github.com/karolzak/Mobile-App-DocumentDB-OfflineSync-Sample/blob/master/sqlite_stored_data.png "SQLite data")
