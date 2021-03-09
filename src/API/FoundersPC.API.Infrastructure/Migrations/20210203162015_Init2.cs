#region Using namespaces

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.API.Infrastructure.Migrations
{
	public partial class Init2 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn("Name",
										  "Processors",
										  "Title");

			migrationBuilder.AddColumn<string>("Title",
											   "VideoCards",
											   "nvarchar(100)",
											   maxLength :100,
											   nullable :false,
											   defaultValue :"");

			migrationBuilder.AddColumn<string>("Series",
											   "Processors",
											   "nvarchar(15)",
											   maxLength :15,
											   nullable :false,
											   defaultValue :"");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn("Title",
										"VideoCards");

			migrationBuilder.DropColumn("Series",
										"Processors");

			migrationBuilder.RenameColumn("Title",
										  "Processors",
										  "Name");
		}
	}
}