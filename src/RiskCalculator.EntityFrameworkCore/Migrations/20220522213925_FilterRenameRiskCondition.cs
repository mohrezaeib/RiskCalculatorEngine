using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiskCalculator.Migrations
{
    public partial class FilterRenameRiskCondition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DivideValue",
                table: "RiskConditions",
                newName: "FilterValue");

            migrationBuilder.RenameColumn(
                name: "DivideFilter",
                table: "RiskConditions",
                newName: "FilterMappingTitle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilterValue",
                table: "RiskConditions",
                newName: "DivideValue");

            migrationBuilder.RenameColumn(
                name: "FilterMappingTitle",
                table: "RiskConditions",
                newName: "DivideFilter");
        }
    }
}
