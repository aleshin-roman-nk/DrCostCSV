CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "Categories" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Categories" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL
);

CREATE TABLE "ExpenseDocuments" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ExpenseDocuments" PRIMARY KEY AUTOINCREMENT,
    "Date" TEXT NOT NULL,
    "SellerName" TEXT NULL,
    "Comment" TEXT NULL
);

CREATE TABLE "ExpenseDocumentItems" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ExpenseDocumentItems" PRIMARY KEY AUTOINCREMENT,
    "ExpenseDocumentId" INTEGER NOT NULL,
    "Name" TEXT NOT NULL,
    "Price" decimal(18,2) NOT NULL,
    "Amount" decimal(18,3) NOT NULL,
    "CategoryId" INTEGER NULL,
    CONSTRAINT "FK_ExpenseDocumentItems_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE SET NULL,
    CONSTRAINT "FK_ExpenseDocumentItems_ExpenseDocuments_ExpenseDocumentId" FOREIGN KEY ("ExpenseDocumentId") REFERENCES "ExpenseDocuments" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_ExpenseDocumentItems_CategoryId" ON "ExpenseDocumentItems" ("CategoryId");

CREATE INDEX "IX_ExpenseDocumentItems_ExpenseDocumentId" ON "ExpenseDocumentItems" ("ExpenseDocumentId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260609190410_InitialCreate', '10.0.9');

COMMIT;

