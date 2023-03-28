using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoStudio.Domain.Migrations
{
    /// <inheritdoc />
    public partial class NullableImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "42e490fb-69dd-4ba8-abaf-4c140f91de0a", "AQAAAAIAAYagAAAAEMw3oFxiqbxs4L4OaXE8bsNrDLjblTvhs43BiTurxIKUsWjAZ9OlsCYqHPMtEmCJJQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "138a726b-eea3-4d8d-b99c-9b0a39e25f7c", "AQAAAAIAAYagAAAAEP64bK9pj4m85iU3+B1cfJ7LjNDbRm71fsRL/zAwEkq210ggrg3YX39S3Y/RhMn3mg==" });
        }
    }
}
