CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "Books" (
    "Id" uuid NOT NULL,
    "Title" text NOT NULL,
    "ISBN" text NOT NULL,
    "PublicationYear" integer NOT NULL,
    CONSTRAINT "PK_Books" PRIMARY KEY ("Id")
);

CREATE TABLE "Members" (
    "Id" uuid NOT NULL,
    "FirstName" text NOT NULL,
    "LastName" text NOT NULL,
    "Email" text NOT NULL,
    CONSTRAINT "PK_Members" PRIMARY KEY ("Id")
);

CREATE TABLE "Loans" (
    "Id" uuid NOT NULL,
    "MemberId" uuid NOT NULL,
    "BookId" uuid NOT NULL,
    "LoanDate" timestamp with time zone NOT NULL,
    "ReturnDate" timestamp with time zone,
    CONSTRAINT "PK_Loans" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Loans_Books_BookId" FOREIGN KEY ("BookId") REFERENCES "Books" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Loans_Members_MemberId" FOREIGN KEY ("MemberId") REFERENCES "Members" ("Id") ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_Books_ISBN" ON "Books" ("ISBN");

CREATE INDEX "IX_Loans_BookId" ON "Loans" ("BookId");

CREATE INDEX "IX_Loans_MemberId" ON "Loans" ("MemberId");

CREATE UNIQUE INDEX "IX_Members_Email" ON "Members" ("Email");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260302102135_InitialSchema', '10.0.3');

COMMIT;

START TRANSACTION;
ALTER TABLE "Books" ADD "Author" text NOT NULL DEFAULT '';

CREATE TABLE "Authors" (
    "Id" uuid NOT NULL,
    "FirstName" text NOT NULL,
    "LastName" text NOT NULL,
    "Biography" text,
    CONSTRAINT "PK_Authors" PRIMARY KEY ("Id")
);

CREATE TABLE "BookAuthors" (
    "AuthorsId" uuid NOT NULL,
    "BooksId" uuid NOT NULL,
    CONSTRAINT "PK_BookAuthors" PRIMARY KEY ("AuthorsId", "BooksId"),
    CONSTRAINT "FK_BookAuthors_Authors_AuthorsId" FOREIGN KEY ("AuthorsId") REFERENCES "Authors" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_BookAuthors_Books_BooksId" FOREIGN KEY ("BooksId") REFERENCES "Books" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_BookAuthors_BooksId" ON "BookAuthors" ("BooksId");

INSERT INTO "Authors" 
("Id","FirstName","LastName")
VALUES ('00000000-0000-0000-0000-000000000001', 'Unknown', 'Author');

INSERT INTO "BookAuthors"
("AuthorsId","BooksId")
SELECT '00000000-0000-0000-0000-000000000001', "Id"
FROM "Books";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260302141450_AddAuthorsToBooks', '10.0.3');

COMMIT;

START TRANSACTION;
ALTER TABLE "Books" DROP COLUMN "Author";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260302155636_RemoveAuthorField', '10.0.3');

COMMIT;

START TRANSACTION;
ALTER TABLE "Members" ADD "PhoneNumber" text;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260302171836_AddNullablePhone', '10.0.3');

COMMIT;

START TRANSACTION;
UPDATE "Members" SET "PhoneNumber" = '' WHERE "PhoneNumber" IS NULL;
ALTER TABLE "Members" ALTER COLUMN "PhoneNumber" SET NOT NULL;
ALTER TABLE "Members" ALTER COLUMN "PhoneNumber" SET DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260306152515_NonNullablePhone', '10.0.3');

COMMIT;

START TRANSACTION;
ALTER TABLE "Loans" ADD "Status" integer NOT NULL DEFAULT 0;

    UPDATE "Loans"
    SET "Status" =
    CASE WHEN "ReturnDate" IS NULL THEN 0 ELSE 1
    END;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260307110540_LoanStatus', '10.0.3');

COMMIT;

START TRANSACTION;
ALTER TABLE "Books" ADD "IsRetired" boolean NOT NULL DEFAULT FALSE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260307114957_retirableBooks', '10.0.3');

COMMIT;

START TRANSACTION;
ALTER TABLE "Books" ADD "StringIsbn" text;

UPDATE "Books"
                  SET "StringIsbn" = 'INVALID-ISBN'

ALTER TABLE "Books" ALTER COLUMN "StringIsbn" SET NOT NULL;

ALTER TABLE "Books" DROP COLUMN "ISBN";

ALTER TABLE "Books" RENAME COLUMN "StringIsbn" TO "ISBN";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260307125733_changeISBNType', '10.0.3');

COMMIT;

