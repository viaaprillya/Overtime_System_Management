using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class create_table_gaji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gaji",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KaryawanID = table.Column<int>(nullable: false),
                    Bulan = table.Column<int>(nullable: false),
                    Tahun = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gaji", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gaji");
        }
    }
}
