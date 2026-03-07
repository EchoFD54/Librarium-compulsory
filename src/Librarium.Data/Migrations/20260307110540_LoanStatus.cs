using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librarium.Data.Migrations
{
    /// <inheritdoc />
    public partial class LoanStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Loans",
                type: "integer",
                nullable: false,
                defaultValue: 0);


            //This is to set the status of existing loans to Active if they have no return date and Returned if they do have a return date
             migrationBuilder.Sql("""
                UPDATE "Loans"
                SET "Status" =
                CASE WHEN "ReturnDate" IS NULL THEN 0 ELSE 1
                END;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Loans");
        }
    }
}
