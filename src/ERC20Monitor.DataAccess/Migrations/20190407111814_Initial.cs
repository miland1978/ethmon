using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DLTech.ERC20Monitor.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blocks",
                columns: table => new
                {
                    id = table.Column<byte[]>(maxLength: 32, nullable: false),
                    height = table.Column<long>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    block_time = table.Column<DateTimeOffset>(nullable: false),
                    miner = table.Column<byte[]>(maxLength: 20, nullable: false),
                    created_at = table.Column<DateTimeOffset>(nullable: false),
                    last_updated_at = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blocks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scope_locks",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 16, nullable: false),
                    last_locked_at = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scope_locks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    id = table.Column<byte[]>(maxLength: 32, nullable: false),
                    from = table.Column<byte[]>(maxLength: 20, nullable: false),
                    to = table.Column<byte[]>(maxLength: 20, nullable: false),
                    value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transactions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "confirmations",
                columns: table => new
                {
                    transaction_id = table.Column<byte[]>(maxLength: 32, nullable: false),
                    block_id = table.Column<byte[]>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_confirmations", x => new { x.transaction_id, x.block_id });
                    table.ForeignKey(
                        name: "fk_confirmations_blocks_block_id",
                        column: x => x.block_id,
                        principalTable: "blocks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_confirmations_transactions_transaction_id",
                        column: x => x.transaction_id,
                        principalTable: "transactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transfers",
                columns: table => new
                {
                    transaction_id = table.Column<byte[]>(maxLength: 32, nullable: false),
                    position = table.Column<int>(nullable: false),
                    from = table.Column<byte[]>(maxLength: 20, nullable: false),
                    to = table.Column<byte[]>(maxLength: 20, nullable: false),
                    amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transfers", x => new { x.transaction_id, x.position });
                    table.ForeignKey(
                        name: "fk_transfers_transactions_transaction_id",
                        column: x => x.transaction_id,
                        principalTable: "transactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "scope_locks",
                columns: new[] { "id", "last_locked_at" },
                values: new object[] { "blocks", new DateTimeOffset(new DateTime(2019, 4, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "ix_blocks_status_height",
                table: "blocks",
                columns: new[] { "status", "height" });

            migrationBuilder.CreateIndex(
                name: "ix_confirmations_block_id",
                table: "confirmations",
                column: "block_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_from",
                table: "transactions",
                column: "from");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_to",
                table: "transactions",
                column: "to");

            migrationBuilder.CreateIndex(
                name: "ix_transfers_from",
                table: "transfers",
                column: "from");

            migrationBuilder.CreateIndex(
                name: "ix_transfers_to",
                table: "transfers",
                column: "to");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "confirmations");

            migrationBuilder.DropTable(
                name: "scope_locks");

            migrationBuilder.DropTable(
                name: "transfers");

            migrationBuilder.DropTable(
                name: "blocks");

            migrationBuilder.DropTable(
                name: "transactions");
        }
    }
}
