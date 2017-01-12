# Azure Mobile Apps using DocumentDb

This is a proof of concept project where we tight up a DocumentDb backend with complex (nested) types with several SQLite clients using Azure Mobile Apps Offline Sync. In this project we are trying to prove that:

- [x] Azure Mobile Apps Offline sync connects with DocumentDb
- [x] The SDK makes sure that only changed stuff gets synced
- Make sure that updatedAt and createdAt are in correct casing!
- [x] Complex no SQL types can be converted to a relational SQLite database
