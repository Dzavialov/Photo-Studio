using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoStudio.Domain.Migrations
{
    /// <inheritdoc />
    public partial class DeletedAdditionalInfoPropertyInEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalInformation",
                table: "EquipmentItems");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EquipmentItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f8cfb855-13ba-4485-a9ba-a2e115665ba2", "AQAAAAIAAYagAAAAEO0fUnA1n5Woo5y9Y4MRFFlmsXkmzFSFpEm6n6IXS6c3EClsTkWW+BJjFahCtBYxvg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rooms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EquipmentItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInformation",
                table: "EquipmentItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "187270aa-6d1d-49fb-81c9-a5fd86550aee", "AQAAAAIAAYagAAAAEMJL5pMiaYCkdFFWKLWD7gi7xEQwE2CLMwJ3Jih27GY373ESFVukUY20g11icZEKog==" });
        }
    }
}
