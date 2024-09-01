using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicPay.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class criacaodobanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    FullName = table.Column<string>(type: "varchar(150)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "varchar(14)", nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false),
                    Password = table.Column<string>(type: "varchar(150)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    PayerId = table.Column<string>(type: "varchar(36)", nullable: false),
                    PayeeId = table.Column<string>(type: "varchar(36)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Wallets_PayeeId",
                        column: x => x.PayeeId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Wallets_PayerId",
                        column: x => x.PayerId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_PayeeId",
                table: "Transfers",
                column: "PayeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_PayerId",
                table: "Transfers",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_DocumentNumber",
                table: "Wallets",
                column: "DocumentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_Email",
                table: "Wallets",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Wallets");
        }
    }
}
