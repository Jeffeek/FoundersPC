#region Using namespaces

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.Identity.Infrastructure.Migrations
{
	public partial class UserEntityChanges : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>("SendMessageOnEntrance",
											 "Users",
											 "bit",
											 nullable :false,
											 defaultValue :false);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn("SendMessageOnEntrance",
										"Users");
		}
	}
}