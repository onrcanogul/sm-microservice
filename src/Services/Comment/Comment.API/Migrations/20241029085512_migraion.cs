using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comment.API.Migrations
{
    /// <inheritdoc />
    public partial class migraion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentOutboxes",
                columns: table => new
                {
                    IdempotentToken = table.Column<Guid>(type: "uuid", nullable: false),
                    OccuredOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProcessedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Payload = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentOutboxes", x => x.IdempotentToken);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentOutboxes");
        }
    }
}
