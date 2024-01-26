using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhiteHat.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedforUserInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserInfoes",
                columns: new[] { "UserId", "DOB", "FirstName", "GenderId", "Image", "IsDeleted", "LastName", "MiddleName", "Mobile" },
                values: new object[] { new Guid("9668dc45-23af-4cae-a85b-3711bb1b4247"), null, "Test", 1, null, false, "User", null, "+9808311151" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserInfoes",
                keyColumn: "UserId",
                keyValue: new Guid("9668dc45-23af-4cae-a85b-3711bb1b4247"));
        }
    }
}
