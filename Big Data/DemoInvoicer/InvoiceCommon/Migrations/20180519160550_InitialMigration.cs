using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InvoiceCommon.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoicesToProcess",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Rfc = table.Column<string>(nullable: true),
                    SocialReazon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicesToProcess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoicesDetailsToProcess",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    InvoiceToProcessDbId = table.Column<int>(nullable: true),
                    MeasureUnit = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    UnitValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicesDetailsToProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicesDetailsToProcess_InvoicesToProcess_InvoiceToProcessDbId",
                        column: x => x.InvoiceToProcessDbId,
                        principalTable: "InvoicesToProcess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesDetailsToProcess_InvoiceToProcessDbId",
                table: "InvoicesDetailsToProcess",
                column: "InvoiceToProcessDbId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicesDetailsToProcess");

            migrationBuilder.DropTable(
                name: "InvoicesToProcess");
        }
    }
}
