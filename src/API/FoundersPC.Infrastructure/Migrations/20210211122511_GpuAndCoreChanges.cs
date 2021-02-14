#region Using derectives

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.Infrastructure.Migrations
{
	public partial class GpuAndCoreChanges : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
			                            "Frequency",
			                            "VideoCardCores");

			migrationBuilder.RenameColumn(
			                              "Interface",
			                              "VideoCardCores",
			                              "ConnectionInterface");

			migrationBuilder.AddColumn<int>(
			                                "Frequency",
			                                "VideoCards",
			                                "int",
			                                nullable:false,
			                                defaultValue:0);

			migrationBuilder.AddColumn<string>(
			                                   "Series",
			                                   "VideoCards",
			                                   "nvarchar(max)",
			                                   nullable:false,
			                                   defaultValue:"");

			migrationBuilder.AddColumn<string>(
			                                   "Codename",
			                                   "VideoCardCores",
			                                   "nvarchar(30)",
			                                   maxLength:30,
			                                   nullable:true);

			migrationBuilder.AlterColumn<string>(
			                                     "RAMMode",
			                                     "Motherboards",
			                                     "nvarchar(5)",
			                                     maxLength:5,
			                                     nullable:false,
			                                     oldClrType:typeof(string),
			                                     oldType:"nvarchar(3)",
			                                     oldMaxLength:3);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
			                            "Frequency",
			                            "VideoCards");

			migrationBuilder.DropColumn(
			                            "Series",
			                            "VideoCards");

			migrationBuilder.DropColumn(
			                            "Codename",
			                            "VideoCardCores");

			migrationBuilder.RenameColumn(
			                              "ConnectionInterface",
			                              "VideoCardCores",
			                              "Interface");

			migrationBuilder.AddColumn<int>(
			                                "Frequency",
			                                "VideoCardCores",
			                                "int",
			                                nullable:false,
			                                defaultValue:0);

			migrationBuilder.AlterColumn<string>(
			                                     "RAMMode",
			                                     "Motherboards",
			                                     "nvarchar(3)",
			                                     maxLength:3,
			                                     nullable:false,
			                                     oldClrType:typeof(string),
			                                     oldType:"nvarchar(5)",
			                                     oldMaxLength:5);
		}
	}
}