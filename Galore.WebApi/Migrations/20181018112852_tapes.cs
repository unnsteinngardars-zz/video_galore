using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Galore.WebApi.Migrations
{
    public partial class tapes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tapes",
                columns: new[] { "Id", "DateCreated", "DateModified", "Deleted", "DirectorFirstName", "DirectorLastName", "EIDR", "ReleaseDate", "Title", "Type" },
                values: new object[] { 4, new DateTime(2018, 10, 18, 11, 28, 52, 236, DateTimeKind.Local), new DateTime(2018, 10, 18, 11, 28, 52, 236, DateTimeKind.Local), false, "John", "Lasseter", "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C", new DateTime(1995, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toy Story", "vhs" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tapes",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
