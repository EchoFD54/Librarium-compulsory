## V001_initial_schema  - Setup of the initial schema before starting the requirements.


## V002_initial_schema  -  Tried adding authors to books
### Type: 
Additive (potentially breaking)
### API Impact: 
Api did not have to be changed since I used backwards compatibility. The fields of a book entity remained unchanged, and I simply added a new authors field which is additive. This might break something in clients that dont ignore unkown Json fields. I also added an Author field as I was reading the requirements and forgot to delete it so that might also break something.
### Deployment notes
Migration must be applied before deployment since it now loads the data of the authors when loading books.
### Decisions and tradeoffs
For this migration, I added an authors table to the databse, and a join table to store the relatino between books and authors. For books that were already in the database before adding this change, I created an "unknown author" and linked it to all previously created books since from now on they will all have an author. I couldnt think of a way to ensure that a book needs to have an author on the database level, but that can be solved on the application by simply making sure that a book cant be created without an author in case I later neeed to add a "create book" endpoint. 


