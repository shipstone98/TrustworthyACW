using Microsoft.EntityFrameworkCore.Migrations;

namespace CateringSystemWeb.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApproveLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InternalOrderLink",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HyperTextReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Method = table.Column<int>(type: "int", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayPalOrderID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalOrderLink", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InternalOrderLink_Orders_PayPalOrderID",
                        column: x => x.PayPalOrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalOrderLink_PayPalOrderID",
                table: "InternalOrderLink",
                column: "PayPalOrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalOrderLink");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
