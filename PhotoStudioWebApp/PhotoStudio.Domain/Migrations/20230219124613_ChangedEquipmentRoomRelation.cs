using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoStudio.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangedEquipmentRoomRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentItems_Rooms_RoomId",
                table: "EquipmentItems");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentItems_RoomId",
                table: "EquipmentItems");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "EquipmentItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "67cbd9c4-90a9-409d-9898-6b1618ac1841", "AQAAAAIAAYagAAAAEN8CkQ7YdzgBDu4VBS5I/0GaX/zFuBATlc9RgYqY8nBbOsYREhKqUV7wOneH0hXUCg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "EquipmentItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "847af593-c927-451e-b597-0eceabf663ef", "AQAAAAIAAYagAAAAEIOS928POzFOhiVbShPIAMbcKrkFrxgaSUGIdit0PIhEScrPiybUvwz8AXiNS6JZmA==" });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentItems_RoomId",
                table: "EquipmentItems",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentItems_Rooms_RoomId",
                table: "EquipmentItems",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
