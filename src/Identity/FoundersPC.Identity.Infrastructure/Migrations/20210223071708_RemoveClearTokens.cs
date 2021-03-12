#region Using namespaces

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.Identity.Infrastructure.Migrations
{
	public partial class RemoveClearTokens : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey("FK_UsersTokens_ApiTokens_ApiAccessTokenId",
											"UsersTokens");

			migrationBuilder.DropTable("ApiTokens");

			migrationBuilder.DropIndex("IX_UsersTokens_ApiAccessTokenId",
									   "UsersTokens");

			migrationBuilder.DropColumn("ApiAccessTokenId",
										"UsersTokens");

			migrationBuilder.AddColumn<string>("HashedToken",
											   "UsersTokens",
											   "nvarchar(88)",
											   maxLength :88,
											   nullable :false,
											   defaultValue :"",
											   fixedLength :true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn("HashedToken",
										"UsersTokens");

			migrationBuilder.AddColumn<int>("ApiAccessTokenId",
											"UsersTokens",
											"int",
											nullable :false,
											defaultValue :0);

			migrationBuilder.CreateTable("ApiTokens",
										 table => new
												  {
														  Id = table.Column<int>("int", nullable :false)
																	.Annotation("SqlServer:Identity", "1, 1"),
														  HashedToken = table.Column<string>("nvarchar(88)", maxLength :88, nullable :false)
												  },
										 constraints :table => table.PrimaryKey("PK_ApiTokens", x => x.Id));

			migrationBuilder.CreateIndex("IX_UsersTokens_ApiAccessTokenId",
										 "UsersTokens",
										 "ApiAccessTokenId");

			migrationBuilder.CreateIndex("IX_ApiTokens_Id",
										 "ApiTokens",
										 "Id");

			migrationBuilder.AddForeignKey("FK_UsersTokens_ApiTokens_ApiAccessTokenId",
										   "UsersTokens",
										   "ApiAccessTokenId",
										   "ApiTokens",
										   principalColumn :"Id",
										   onDelete :ReferentialAction.Cascade);
		}
	}
}