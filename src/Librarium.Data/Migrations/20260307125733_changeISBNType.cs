using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librarium.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeISBNType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //First we create a new column to hold the string ISBN values  
            migrationBuilder.AddColumn<string>(
                name: "StringIsbn",
                table: "Books",
                type: "text",
                nullable: true);

            //Since we cant recover the data, we set all existing ISBN values to a default value to avoid null values in the new column
            migrationBuilder.Sql(
                @"UPDATE ""Books""
                  SET ""StringIsbn"" = 'INVALID-ISBN'");

            //Then we alter the new column to be non-nullable
            migrationBuilder.AlterColumn<string>(
                name: "StringIsbn",
                table: "Books",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            //Lastly, we drop the old ISBN column and rename the new column to ISBN
            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Books");

             migrationBuilder.RenameColumn(
                name: "StringIsbn",
                table: "Books",
                newName: "ISBN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ISBN_INT",
                table: "Books",
                type: "integer",
                nullable: true);
            
            migrationBuilder.Sql(
                """
                UPDATE "Books"
                SET "ISBN_INT" = 0
                """
            );

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "ISBN_INT",
                table: "Books",
                newName: "ISBN");

            migrationBuilder.AlterColumn<int>(
                name: "ISBN",
                table: "Books",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

        }
    }
}
