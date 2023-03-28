using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoStudio.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangedImageType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f627e4d3-4bb8-4b84-ab69-4039a893eb41", "AQAAAAIAAYagAAAAEL/OUkAWy+0TxJmMe7iaobcL3igPIQtYUc/fFNJD8HvH/rKI1FlpQI82GeBWFCQIaw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "42e490fb-69dd-4ba8-abaf-4c140f91de0a", "AQAAAAIAAYagAAAAEMw3oFxiqbxs4L4OaXE8bsNrDLjblTvhs43BiTurxIKUsWjAZ9OlsCYqHPMtEmCJJQ==" });
        }
    }
}
