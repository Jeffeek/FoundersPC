#region Using namespaces

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.API.Infrastructure.Migrations
{
    public partial class AddVolumeToRam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>("Volume",
                                            "RandomAccessMemory",
                                            "int",
                                            nullable : false,
                                            defaultValue : 1);

            migrationBuilder.AlterColumn<string>("Country",
                                                 "Producers",
                                                 "nvarchar(50)",
                                                 maxLength : 50,
                                                 nullable : false,
                                                 defaultValue : "",
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(50)",
                                                 oldMaxLength : 50,
                                                 oldNullable : true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Volume",
                                        "RandomAccessMemory");

            migrationBuilder.AlterColumn<string>("Country",
                                                 "Producers",
                                                 "nvarchar(50)",
                                                 maxLength : 50,
                                                 nullable : true,
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(50)",
                                                 oldMaxLength : 50);
        }
    }
}