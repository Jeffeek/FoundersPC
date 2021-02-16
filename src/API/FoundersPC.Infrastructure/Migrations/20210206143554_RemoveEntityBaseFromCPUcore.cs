#region Using derectives

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.Infrastructure.Migrations
{
    public partial class RemoveEntityBaseFromCPUcore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("MarketLaunch",
                                        "VideoCards");

            migrationBuilder.DropColumn("MarketLaunch",
                                        "SolidStateDrives");

            migrationBuilder.DropColumn("MarketLaunch",
                                        "RandomAccessMemory");

            migrationBuilder.DropColumn("MarketLaunch",
                                        "Processors");

            migrationBuilder.DropColumn("MarketLaunch",
                                        "PowerSupplies");

            migrationBuilder.DropColumn("MarketLaunch",
                                        "Motherboards");

            migrationBuilder.DropColumn("MarketLaunch",
                                        "HardDrives");

            migrationBuilder.DropColumn("MarketLaunch",
                                        "Cases");

            migrationBuilder.AlterColumn<string>("Title",
                                                 "VideoCardCores",
                                                 "nvarchar(max)",
                                                 nullable : false,
                                                 defaultValue : "",
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(30)",
                                                 oldMaxLength : 30,
                                                 oldNullable : true);

            migrationBuilder.AddColumn<string>("Title",
                                               "SolidStateDrives",
                                               "nvarchar(100)",
                                               maxLength : 100,
                                               nullable : false,
                                               defaultValue : "");

            migrationBuilder.AddColumn<string>("Title",
                                               "RandomAccessMemory",
                                               "nvarchar(100)",
                                               maxLength : 100,
                                               nullable : false,
                                               defaultValue : "");

            migrationBuilder.AlterColumn<string>("Title",
                                                 "Processors",
                                                 "nvarchar(100)",
                                                 maxLength : 100,
                                                 nullable : false,
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(20)",
                                                 oldMaxLength : 20);

            migrationBuilder.AlterColumn<string>("Title",
                                                 "ProcessorCores",
                                                 "nvarchar(max)",
                                                 nullable : false,
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(50)",
                                                 oldMaxLength : 50);

            migrationBuilder.AlterColumn<int>("Efficiency",
                                              "PowerSupplies",
                                              "int",
                                              nullable : true,
                                              oldClrType : typeof(int),
                                              oldType : "int");

            migrationBuilder.AddColumn<string>("Title",
                                               "PowerSupplies",
                                               "nvarchar(100)",
                                               maxLength : 100,
                                               nullable : false,
                                               defaultValue : "");

            migrationBuilder.AlterColumn<string>("RAMSupport",
                                                 "Motherboards",
                                                 "nvarchar(7)",
                                                 maxLength : 7,
                                                 nullable : false,
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(6)",
                                                 oldMaxLength : 6);

            migrationBuilder.AlterColumn<string>("Factor",
                                                 "Motherboards",
                                                 "nvarchar(10)",
                                                 maxLength : 10,
                                                 nullable : false,
                                                 oldClrType : typeof(double),
                                                 oldType : "float");

            migrationBuilder.AddColumn<string>("Title",
                                               "Motherboards",
                                               "nvarchar(100)",
                                               maxLength : 100,
                                               nullable : false,
                                               defaultValue : "");

            migrationBuilder.AddColumn<string>("Title",
                                               "HardDrives",
                                               "nvarchar(100)",
                                               maxLength : 100,
                                               nullable : false,
                                               defaultValue : "");

            migrationBuilder.AddColumn<int>("Depth",
                                            "Cases",
                                            "int",
                                            nullable : true);

            migrationBuilder.AddColumn<int>("Height",
                                            "Cases",
                                            "int",
                                            nullable : true);

            migrationBuilder.AddColumn<string>("Title",
                                               "Cases",
                                               "nvarchar(100)",
                                               maxLength : 100,
                                               nullable : false,
                                               defaultValue : "");

            migrationBuilder.AddColumn<double>("Weight",
                                               "Cases",
                                               "float",
                                               nullable : true);

            migrationBuilder.AddColumn<int>("Width",
                                            "Cases",
                                            "int",
                                            nullable : true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Title",
                                        "SolidStateDrives");

            migrationBuilder.DropColumn("Title",
                                        "RandomAccessMemory");

            migrationBuilder.DropColumn("Title",
                                        "PowerSupplies");

            migrationBuilder.DropColumn("Title",
                                        "Motherboards");

            migrationBuilder.DropColumn("Title",
                                        "HardDrives");

            migrationBuilder.DropColumn("Depth",
                                        "Cases");

            migrationBuilder.DropColumn("Height",
                                        "Cases");

            migrationBuilder.DropColumn("Title",
                                        "Cases");

            migrationBuilder.DropColumn("Weight",
                                        "Cases");

            migrationBuilder.DropColumn("Width",
                                        "Cases");

            migrationBuilder.AddColumn<DateTime>("MarketLaunch",
                                                 "VideoCards",
                                                 "datetime2",
                                                 nullable : true);

            migrationBuilder.AlterColumn<string>("Title",
                                                 "VideoCardCores",
                                                 "nvarchar(30)",
                                                 maxLength : 30,
                                                 nullable : true,
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>("MarketLaunch",
                                                 "SolidStateDrives",
                                                 "datetime2",
                                                 nullable : true);

            migrationBuilder.AddColumn<DateTime>("MarketLaunch",
                                                 "RandomAccessMemory",
                                                 "datetime2",
                                                 nullable : true);

            migrationBuilder.AlterColumn<string>("Title",
                                                 "Processors",
                                                 "nvarchar(20)",
                                                 maxLength : 20,
                                                 nullable : false,
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(100)",
                                                 oldMaxLength : 100);

            migrationBuilder.AddColumn<DateTime>("MarketLaunch",
                                                 "Processors",
                                                 "datetime2",
                                                 nullable : true);

            migrationBuilder.AlterColumn<string>("Title",
                                                 "ProcessorCores",
                                                 "nvarchar(50)",
                                                 maxLength : 50,
                                                 nullable : false,
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(max)");

            migrationBuilder.AlterColumn<int>("Efficiency",
                                              "PowerSupplies",
                                              "int",
                                              nullable : false,
                                              defaultValue : 0,
                                              oldClrType : typeof(int),
                                              oldType : "int",
                                              oldNullable : true);

            migrationBuilder.AddColumn<DateTime>("MarketLaunch",
                                                 "PowerSupplies",
                                                 "datetime2",
                                                 nullable : true);

            migrationBuilder.AlterColumn<string>("RAMSupport",
                                                 "Motherboards",
                                                 "nvarchar(6)",
                                                 maxLength : 6,
                                                 nullable : false,
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(7)",
                                                 oldMaxLength : 7);

            migrationBuilder.AlterColumn<double>("Factor",
                                                 "Motherboards",
                                                 "float",
                                                 nullable : false,
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(10)",
                                                 oldMaxLength : 10);

            migrationBuilder.AddColumn<DateTime>("MarketLaunch",
                                                 "Motherboards",
                                                 "datetime2",
                                                 nullable : true);

            migrationBuilder.AddColumn<DateTime>("MarketLaunch",
                                                 "HardDrives",
                                                 "datetime2",
                                                 nullable : true);

            migrationBuilder.AddColumn<DateTime>("MarketLaunch",
                                                 "Cases",
                                                 "datetime2",
                                                 nullable : true);
        }
    }
}