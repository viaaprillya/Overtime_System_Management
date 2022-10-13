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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
