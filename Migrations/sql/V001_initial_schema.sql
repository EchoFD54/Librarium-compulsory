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

