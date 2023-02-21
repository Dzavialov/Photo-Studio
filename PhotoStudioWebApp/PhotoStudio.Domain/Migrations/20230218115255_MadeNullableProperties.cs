using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoStudio.Domain.Migrations
{
    /// <inheritdoc />
    public partial class MadeNullableProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "847af593-c927-451e-b597-0eceabf663ef", "AQAAAAIAAYagAAAAEIOS928POzFOhiVbShPIAMbcKrkFrxgaSUGIdit0PIhEScrPiybUvwz8AXiNS6JZmA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "319db23f-ef17-4e9e-9146-d276aa72d6c4", "AQAAAAIAAYagAAAAENMMOiW54SzuIPzbVXLysHb+Wu1MqUwTF+5ek3H+H8K6R402AP+0wpXRpWAQ8bM+uQ==" });
        }
    }
}
