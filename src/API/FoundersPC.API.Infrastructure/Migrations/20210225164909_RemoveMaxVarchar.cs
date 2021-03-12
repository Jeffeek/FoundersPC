#region Using namespaces

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.API.Infrastructure.Migrations
{
	public partial class RemoveMaxVarchar : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>("Series",
												 "VideoCards",
												 "nvarchar(30)",
												 maxLength :30,
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(max)");

			migrationBuilder.AlterColumn<string>("Title",
												 "VideoCardCores",
												 "nvarchar(100)",
												 maxLength :100,
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(max)");

			migrationBuilder.AlterColumn<string>("DirectX_Version",
												 "VideoCardCores",
												 "nvarchar(10)",
												 maxLength :10,
												 nullable :true,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(max)",
												 oldNullable :true);

			migrationBuilder.AlterColumn<string>("ArchitectureTitle",
												 "VideoCardCores",
												 "nvarchar(30)",
												 maxLength :30,
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(max)");

			migrationBuilder.AlterColumn<string>("Title",
												 "ProcessorCores",
												 "nvarchar(50)",
												 maxLength :50,
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(max)");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>("Series",
												 "VideoCards",
												 "nvarchar(max)",
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(30)",
												 oldMaxLength :30);

			migrationBuilder.AlterColumn<string>("Title",
												 "VideoCardCores",
												 "nvarchar(max)",
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(100)",
												 oldMaxLength :100);

			migrationBuilder.AlterColumn<string>("DirectX_Version",
												 "VideoCardCores",
												 "nvarchar(max)",
												 nullable :true,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(10)",
												 oldMaxLength :10,
												 oldNullable :true);

			migrationBuilder.AlterColumn<string>("ArchitectureTitle",
												 "VideoCardCores",
												 "nvarchar(max)",
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(30)",
												 oldMaxLength :30);

			migrationBuilder.AlterColumn<string>("Title",
												 "ProcessorCores",
												 "nvarchar(max)",
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(50)",
												 oldMaxLength :50);
		}
	}
}