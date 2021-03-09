#region Using namespaces

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.API.Infrastructure.Migrations
{
	public partial class RemoveCaseBug : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>("VideoMemoryType",
												 "VideoCards",
												 "nvarchar(8)",
												 maxLength :8,
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(7)",
												 oldMaxLength :7);

			migrationBuilder.AlterColumn<string>("RAMMode",
												 "Motherboards",
												 "nvarchar(3)",
												 maxLength :3,
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(2)",
												 oldMaxLength :2);

			migrationBuilder.AlterColumn<string>("Type",
												 "Cases",
												 "nvarchar(40)",
												 maxLength :40,
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(3)",
												 oldMaxLength :3);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>("VideoMemoryType",
												 "VideoCards",
												 "nvarchar(7)",
												 maxLength :7,
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(8)",
												 oldMaxLength :8);

			migrationBuilder.AlterColumn<string>("RAMMode",
												 "Motherboards",
												 "nvarchar(2)",
												 maxLength :2,
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(3)",
												 oldMaxLength :3);

			migrationBuilder.AlterColumn<string>("Type",
												 "Cases",
												 "nvarchar(3)",
												 maxLength :3,
												 nullable :false,
												 oldClrType :typeof(string),
												 oldType :"nvarchar(40)",
												 oldMaxLength :40);
		}
	}
}