using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataGridVueDotnetExample.Migrations
{
    /// <inheritdoc />
    public partial class AddDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "TestDataItems");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "TestDataItems");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Created",
                table: "TestDataItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "TestDataItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<long>(
                name: "PhoneNumber",
                table: "TestDataItems",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "TestDataItems");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "TestDataItems");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "TestDataItems");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "TestDataItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "TestDataItems",
                type: "TEXT",
                nullable: true);
        }
    }
}
