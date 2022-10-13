using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_column_to_karyawan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Karyawan",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Tanggal_Lahir",
                table: "Karyawan",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Tanggal_Masuk",
                table: "Karyawan",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Karyawan");

            migrationBuilder.DropColumn(
                name: "Tanggal_Lahir",
                table: "Karyawan");

            migrationBuilder.DropColumn(
                name: "Tanggal_Masuk",
                table: "Karyawan");
        }
    }
}
