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
Simply removed the author field and column from Book, as it was unnecesary. Doesnt break anything.

## V004_add_nullable_phone - Added the phone column as nullable
### Type: 
Additive (non-breaking)
### API Impact:
Api remains unchanged as only the phone field was added as a nullable to ensure compatibility with the current application.
### Deployment notes
The migration needs to be applied before deployment since there is a new field for the member entity.
### Decisions and tradeoffs
Since I made the new phone number field for members nullable, the members that already exist dont have a sudden null field on their phone columns and older versions of the application can operate and prepare for a not null enforcement. So, after this migrations is applied, the members that were already on the database would have their phone numbers added manually, and new members should already be registered with a phone from now on. This basically splits this requirement into 2 migrations, this would be the "setup" migration and then once all pre existing members have a phone number we can change it to not null in the next migration so it is enforced on the database.

For the email requirement, I already had setup the emails as unique on the first schema. But if I did not do that then  I would have renamed the duplicate emails on the database, so for example for every duplicate email I would have used a query to simply add somthing to make it unique, inetad of anna@example.com it would have been anna@example.com+1 pr something similar. Then once the duplicates are renamed I would have added the unique index for emails.

## V005_add_not_null_phone - Made the phone column nullable
### Type:
Requires coordination
### API Impact:
Api still remains unchanged as only a single change was made on the member entity. However the client sould by now have added the phone on their post request.
### Deployment notes
Since now the member table enforces the phone number field, the migration would have to be applied after redeploying so the application can account for that.
### Decisions and tradeoffs
Now phones have been also enforced on the database, and since we allowed nullable phones to exist for a time, hopefully all previous members have also a phone number and nothing should break regarding this requirement.

## V006_add_loan_status - Added a status field without breaking current client
### Type:
Additive (non-breaking)
### API Impact:
Since the loanDto was not changed, the Api does not need to be changed as the endpoint for getting loans for members will continue to return the returDate field as normal. The status will be set internally. In the future, the frontend will have to take the new status field into account for their post requests.
### Deployment notes
MIgration can be applied before redeploying as the new column is additive and doesnt break anything.
### Decisions and tradeoffs
For this requirement, I added the status column to the loans table, and mapped the returnDate value to a status, so pre existing loans with a null value will be set to active and those with a return date will be set to returned. This allows the frontend to continue working normally for the duration of the sprints as the dto that is read by the frontend remains unchanged and still contains the returnDate field. For new loans, the dbservice will map then to active as default when created.

## V007_retirable_books - Added a column to determine if books are retired
### Type:
Additive (non-breaking).
### API Impact:
Queries were updated to exclude books that are retired when retreiving books or creating loans. The api itself did not change just the queries on the dbService.
### Deployment notes
Migration can be applied before redeployment as the new column has a default value so that existing rows are unnafected. During the deploy window, the behaviour of the old application shouldn't changhe as it will not reference the new column.
### Decisions and tradeoffs
Dev response:
I accepted your proposal of simply adding a new column to use a IsDeleted flag (I changed the name to retired since it makes more sense as the book is not really deleted) since it solves the issue without having to delete anything and keeps existing books visible as they have a default false value . The filtering that you mentioned was also added on the dbService but only when retreiving books as we don't want retired books to show up on the catalogue. For loans, I did not apply filtering as we want to keep historical data and need those retired books to also show up when looking up a member's loans. Finally, the logic for loan creation was updated so it now rejects creating a loan for a retired book.   







