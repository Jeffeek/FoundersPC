using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundersPC.Infrastructure.Migrations
{
    public partial class RemoveEntityBaseFromCPUcore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketLaunch",
                table: "VideoCards");

            migrationBuilder.DropColumn(
                name: "MarketLaunch",
                table: "SolidStateDrives");

            migrationBuilder.DropColumn(
                name: "MarketLaunch",
                table: "RandomAccessMemory");

            migrationBuilder.DropColumn(
                name: "MarketLaunch",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "MarketLaunch",
                table: "PowerSupplies");

            migrationBuilder.DropColumn(
                name: "MarketLaunch",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "MarketLaunch",
                table: "HardDrives");

            migrationBuilder.DropColumn(
                name: "MarketLaunch",
                table: "Cases");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "VideoCardCores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "SolidStateDrives",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "RandomAccessMemory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Processors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProcessorCores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Efficiency",
                table: "PowerSupplies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PowerSupplies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RAMSupport",
                table: "Motherboards",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Factor",
                table: "Motherboards",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Motherboards",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "HardDrives",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Depth",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Cases",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Cases",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Cases",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "SolidStateDrives");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "RandomAccessMemory");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PowerSupplies");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "HardDrives");

            migrationBuilder.DropColumn(
                name: "Depth",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Cases");

            migrationBuilder.AddColumn<DateTime>(
                name: "MarketLaunch",
                table: "VideoCards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "VideoCardCores",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "MarketLaunch",
                table: "SolidStateDrives",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MarketLaunch",
                table: "RandomAccessMemory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Processors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "MarketLaunch",
                table: "Processors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProcessorCores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Efficiency",
                table: "PowerSupplies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MarketLaunch",
                table: "PowerSupplies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RAMSupport",
                table: "Motherboards",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(7)",
                oldMaxLength: 7);

            migrationBuilder.AlterColumn<double>(
                name: "Factor",
                table: "Motherboards",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<DateTime>(
                name: "MarketLaunch",
                table: "Motherboards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MarketLaunch",
                table: "HardDrives",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MarketLaunch",
                table: "Cases",
                type: "datetime2",
                nullable: true);
        }
    }
}
