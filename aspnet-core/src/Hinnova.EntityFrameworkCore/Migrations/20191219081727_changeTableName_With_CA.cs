using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class changeTableName_With_CA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkDetails",
                table: "WorkDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkAssigns",
                table: "WorkAssigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WordProcessings",
                table: "WordProcessings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeHandes",
                table: "TypeHandes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceiveUnits",
                table: "ReceiveUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promulgateds",
                table: "Promulgateds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemorizeKeywords",
                table: "MemorizeKeywords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentHandlingDetails",
                table: "DocumentHandlingDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentDetails",
                table: "DocumentDetails");

            migrationBuilder.RenameTable(
                name: "WorkDetails",
                newName: "CA_WorkDetails");

            migrationBuilder.RenameTable(
                name: "WorkAssigns",
                newName: "CA_WorkAssigns");

            migrationBuilder.RenameTable(
                name: "WordProcessings",
                newName: "CA_WordProcessings");

            migrationBuilder.RenameTable(
                name: "TypeHandes",
                newName: "CA_TypeHandes");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "CA_Schedules");

            migrationBuilder.RenameTable(
                name: "ReceiveUnits",
                newName: "CA_ReceiveUnits");

            migrationBuilder.RenameTable(
                name: "Promulgateds",
                newName: "CA_Promulgateds");

            migrationBuilder.RenameTable(
                name: "MemorizeKeywords",
                newName: "CA_MemorizeKeywords");

            migrationBuilder.RenameTable(
                name: "DocumentTypes",
                newName: "CA_DocumentTypes");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "CA_Documents");

            migrationBuilder.RenameTable(
                name: "DocumentHandlingDetails",
                newName: "CA_DocumentHandlingDetails");

            migrationBuilder.RenameTable(
                name: "DocumentDetails",
                newName: "CA_DocumentDetails");

            migrationBuilder.RenameIndex(
                name: "IX_WorkDetails_TenantId",
                table: "CA_WorkDetails",
                newName: "IX_CA_WorkDetails_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkAssigns_TenantId",
                table: "CA_WorkAssigns",
                newName: "IX_CA_WorkAssigns_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_WordProcessings_TenantId",
                table: "CA_WordProcessings",
                newName: "IX_CA_WordProcessings_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeHandes_TenantId",
                table: "CA_TypeHandes",
                newName: "IX_CA_TypeHandes_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_TenantId",
                table: "CA_Schedules",
                newName: "IX_CA_Schedules_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiveUnits_TenantId",
                table: "CA_ReceiveUnits",
                newName: "IX_CA_ReceiveUnits_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Promulgateds_TenantId",
                table: "CA_Promulgateds",
                newName: "IX_CA_Promulgateds_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_MemorizeKeywords_TenantId",
                table: "CA_MemorizeKeywords",
                newName: "IX_CA_MemorizeKeywords_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentTypes_TenantId",
                table: "CA_DocumentTypes",
                newName: "IX_CA_DocumentTypes_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_TenantId",
                table: "CA_Documents",
                newName: "IX_CA_Documents_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentHandlingDetails_TenantId",
                table: "CA_DocumentHandlingDetails",
                newName: "IX_CA_DocumentHandlingDetails_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentDetails_TenantId",
                table: "CA_DocumentDetails",
                newName: "IX_CA_DocumentDetails_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_WorkDetails",
                table: "CA_WorkDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_WorkAssigns",
                table: "CA_WorkAssigns",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_WordProcessings",
                table: "CA_WordProcessings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_TypeHandes",
                table: "CA_TypeHandes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_Schedules",
                table: "CA_Schedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_ReceiveUnits",
                table: "CA_ReceiveUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_Promulgateds",
                table: "CA_Promulgateds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_MemorizeKeywords",
                table: "CA_MemorizeKeywords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_DocumentTypes",
                table: "CA_DocumentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_Documents",
                table: "CA_Documents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_DocumentHandlingDetails",
                table: "CA_DocumentHandlingDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_DocumentDetails",
                table: "CA_DocumentDetails",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_WorkDetails",
                table: "CA_WorkDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_WorkAssigns",
                table: "CA_WorkAssigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_WordProcessings",
                table: "CA_WordProcessings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_TypeHandes",
                table: "CA_TypeHandes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_Schedules",
                table: "CA_Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_ReceiveUnits",
                table: "CA_ReceiveUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_Promulgateds",
                table: "CA_Promulgateds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_MemorizeKeywords",
                table: "CA_MemorizeKeywords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_DocumentTypes",
                table: "CA_DocumentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_Documents",
                table: "CA_Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_DocumentHandlingDetails",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_DocumentDetails",
                table: "CA_DocumentDetails");

            migrationBuilder.RenameTable(
                name: "CA_WorkDetails",
                newName: "WorkDetails");

            migrationBuilder.RenameTable(
                name: "CA_WorkAssigns",
                newName: "WorkAssigns");

            migrationBuilder.RenameTable(
                name: "CA_WordProcessings",
                newName: "WordProcessings");

            migrationBuilder.RenameTable(
                name: "CA_TypeHandes",
                newName: "TypeHandes");

            migrationBuilder.RenameTable(
                name: "CA_Schedules",
                newName: "Schedules");

            migrationBuilder.RenameTable(
                name: "CA_ReceiveUnits",
                newName: "ReceiveUnits");

            migrationBuilder.RenameTable(
                name: "CA_Promulgateds",
                newName: "Promulgateds");

            migrationBuilder.RenameTable(
                name: "CA_MemorizeKeywords",
                newName: "MemorizeKeywords");

            migrationBuilder.RenameTable(
                name: "CA_DocumentTypes",
                newName: "DocumentTypes");

            migrationBuilder.RenameTable(
                name: "CA_Documents",
                newName: "Documents");

            migrationBuilder.RenameTable(
                name: "CA_DocumentHandlingDetails",
                newName: "DocumentHandlingDetails");

            migrationBuilder.RenameTable(
                name: "CA_DocumentDetails",
                newName: "DocumentDetails");

            migrationBuilder.RenameIndex(
                name: "IX_CA_WorkDetails_TenantId",
                table: "WorkDetails",
                newName: "IX_WorkDetails_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_CA_WorkAssigns_TenantId",
                table: "WorkAssigns",
                newName: "IX_WorkAssigns_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_CA_WordProcessings_TenantId",
                table: "WordProcessings",
                newName: "IX_WordProcessings_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_CA_TypeHandes_TenantId",
                table: "TypeHandes",
                newName: "IX_TypeHandes_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_CA_Schedules_TenantId",
                table: "Schedules",
                newName: "IX_Schedules_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_CA_ReceiveUnits_TenantId",
                table: "ReceiveUnits",
                newName: "IX_ReceiveUnits_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_CA_Promulgateds_TenantId",
                table: "Promulgateds",
                newName: "IX_Promulgateds_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_CA_MemorizeKeywords_TenantId",
                table: "MemorizeKeywords",
                newName: "IX_MemorizeKeywords_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_CA_DocumentTypes_TenantId",
                table: "DocumentTypes",
                newName: "IX_DocumentTypes_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_CA_Documents_TenantId",
                table: "Documents",
                newName: "IX_Documents_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_CA_DocumentHandlingDetails_TenantId",
                table: "DocumentHandlingDetails",
                newName: "IX_DocumentHandlingDetails_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_CA_DocumentDetails_TenantId",
                table: "DocumentDetails",
                newName: "IX_DocumentDetails_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkDetails",
                table: "WorkDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkAssigns",
                table: "WorkAssigns",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WordProcessings",
                table: "WordProcessings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeHandes",
                table: "TypeHandes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceiveUnits",
                table: "ReceiveUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promulgateds",
                table: "Promulgateds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemorizeKeywords",
                table: "MemorizeKeywords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentHandlingDetails",
                table: "DocumentHandlingDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentDetails",
                table: "DocumentDetails",
                column: "Id");
        }
    }
}
