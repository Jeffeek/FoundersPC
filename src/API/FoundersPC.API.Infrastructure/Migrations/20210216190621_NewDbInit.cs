#region Using namespaces

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.API.Infrastructure.Migrations
{
    public partial class NewDbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>("DirectX_Version",
                                                 "VideoCardCores",
                                                 "nvarchar(max)",
                                                 nullable : true,
                                                 oldClrType : typeof(int),
                                                 oldType : "int");

            migrationBuilder.AlterColumn<bool>("CPU8PIN",
                                               "PowerSupplies",
                                               "bit",
                                               nullable : true,
                                               oldClrType : typeof(bool),
                                               oldType : "bit");

            migrationBuilder.AlterColumn<bool>("CPU4PIN",
                                               "PowerSupplies",
                                               "bit",
                                               nullable : true,
                                               oldClrType : typeof(bool),
                                               oldType : "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>("DirectX_Version",
                                              "VideoCardCores",
                                              "int",
                                              nullable : false,
                                              defaultValue : 0,
                                              oldClrType : typeof(string),
                                              oldType : "nvarchar(max)",
                                              oldNullable : true);

            migrationBuilder.AlterColumn<bool>("CPU8PIN",
                                               "PowerSupplies",
                                               "bit",
                                               nullable : false,
                                               defaultValue : false,
                                               oldClrType : typeof(bool),
                                               oldType : "bit",
                                               oldNullable : true);

            migrationBuilder.AlterColumn<bool>("CPU4PIN",
                                               "PowerSupplies",
                                               "bit",
                                               nullable : false,
                                               defaultValue : false,
                                               oldClrType : typeof(bool),
                                               oldType : "bit",
                                               oldNullable : true);
        }
    }
}