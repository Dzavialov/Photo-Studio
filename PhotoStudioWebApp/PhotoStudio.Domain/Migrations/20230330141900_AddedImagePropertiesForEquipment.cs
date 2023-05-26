using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoStudio.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddedImagePropertiesForEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "EquipmentItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "EquipmentItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "00f8aacc-372c-4d0b-af31-4892c55cebb6", "AQAAAAIAAYagAAAAEBbKc3SZa7iRsYlxTxWnY2kiOxtb5/vGsJAlf/TLkVDQW0QF8Sra/ltQ+0L/5+m5hw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "EquipmentItems");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "EquipmentItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f627e4d3-4bb8-4b84-ab69-4039a893eb41", "AQAAAAIAAYagAAAAEL/OUkAWy+0TxJmMe7iaobcL3igPIQtYUc/fFNJD8HvH/rKI1FlpQI82GeBWFCQIaw==" });
        }
    }
}
