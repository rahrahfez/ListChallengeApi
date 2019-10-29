using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListChallengeApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "root",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Label = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_root", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "factory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RootId = table.Column<Guid>(nullable: false),
                    RangeLow = table.Column<int>(nullable: false),
                    RangeHigh = table.Column<int>(nullable: false),
                    Label = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_factory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_factory_root_RootId",
                        column: x => x.RootId,
                        principalTable: "root",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "child",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FactoryId = table.Column<Guid>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_child", x => x.Id);
                    table.ForeignKey(
                        name: "FK_child_factory_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "factory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_child_FactoryId",
                table: "child",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_factory_RootId",
                table: "factory",
                column: "RootId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "child");

            migrationBuilder.DropTable(
                name: "factory");

            migrationBuilder.DropTable(
                name: "root");
        }
    }
}
