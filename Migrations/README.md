## V001_initial_schema  - Setup of the initial schema before starting the requirements.


## V002_initial_schema  -  Tried adding authors to books
### Type: 
Additive (potentially breaking)
### API Impact: 
Api did not have to be changed since I used backwards compatibility. The fields of a book entity remained unchanged, and I simply added a new authors field which is additive. This might break something in clients that dont ignore unkown Json fields. I also added an Author field as I was reading the requirements and forgot to delete it so that might also break something.
### Deployment notes
Migration must be applied before deployment since it now loads the data of the authors when loading books.
### Decisions and tradeoffs
For this migration, I added an authors table to the databse, and a join table to store the relatino between books and authors. For books that were already in the database before adding this change, I created an "unknown author" and linked it to all previously created books since from now on they will all have an author. I couldnt think of a way to ensure that a book needs to have an author on the database level, but that can be solved by the application by simply making sure that a book cant be created without an author in case I later neeed to add a "create book" endpoint. 

## V003_remove_author_field -  Removed author column and field
Simply removed the author field and volumns from Book, as it was unnecesary. Doesnt break anything.

## V004_add_nullable_phone
### Type: 
Additive (non-breaking)
### API Impact:
Api remains unchanged as only the phone field was added as a nullable to ensure compatibility with the current application.
### Deployment notes
The migration needs to be applied before deployment since there is a new field for the member entity.
### Decisions and tradeoffs
Since I made the new phone number field for members nullable, the members that already exist dont have a sudden null field on their phone columns and older versions of the application can operate and prepare for a not null enforcement. So, after this migrations is applied, the members that were already on the database would have their phone numbers added manually, and new members should already be registered with a phone from now on. This basically splits this requirement into 2 migrations, this would be the "setup" migration and then once all pre existing members have a phone number we can change it to not null in the next migration so it is enforced on the database.

For the email requirement, I already had setup the emails as unique on the first schema. But if I did not do that then  I would have renamed the duplicate emails on the database, so for example for every duplicate email I would have used a query to simply add somthing to make it unique, inetad of anna@example.com it would have been anna@example.com+1 pr something similar. Then once the duplicates are renamed I would have added the unique index for emails.





