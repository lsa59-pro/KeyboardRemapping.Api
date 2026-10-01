using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyboardRemapping.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keyboards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyboards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyboardKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KeyboardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    HidUsageCode = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyboardKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyboardKeys_Keyboards_KeyboardId",
                        column: x => x.KeyboardId,
                        principalTable: "Keyboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KeyboardId = table.Column<int>(type: "INTEGER", nullable: false),
                    SourceKeyId = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetKeyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyMappings_KeyboardKeys_SourceKeyId",
                        column: x => x.SourceKeyId,
                        principalTable: "KeyboardKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeyMappings_KeyboardKeys_TargetKeyId",
                        column: x => x.TargetKeyId,
                        principalTable: "KeyboardKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeyMappings_Keyboards_KeyboardId",
                        column: x => x.KeyboardId,
                        principalTable: "Keyboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyboardKeys_KeyboardId_HidUsageCode",
                table: "KeyboardKeys",
                columns: new[] { "KeyboardId", "HidUsageCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_Name",
                table: "Keyboards",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeyMappings_KeyboardId_SourceKeyId",
                table: "KeyMappings",
                columns: new[] { "KeyboardId", "SourceKeyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeyMappings_SourceKeyId",
                table: "KeyMappings",
                column: "SourceKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyMappings_TargetKeyId",
                table: "KeyMappings",
                column: "TargetKeyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyMappings");

            migrationBuilder.DropTable(
                name: "KeyboardKeys");

            migrationBuilder.DropTable(
                name: "Keyboards");
        }
    }
}
