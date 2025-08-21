using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Models.Migrations
{
    /// <inheritdoc />
    public partial class clusteredindexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

     

            migrationBuilder.Sql(@"

                CREATE INDEX IF NOT EXISTS ""IX_CurrencyRate_CurrencyCode"" ON ""CurrencyRate"" (""CurrencyCode"");
                CLUSTER ""CurrencyRate"" USING ""IX_CurrencyRate_CurrencyCode"";

                CREATE INDEX IF NOT EXISTS ""IX_CalculatedHistory_InputCode_EffectiveDate"" ON ""CalculatedHistory"" (""InputCode"", ""EffectiveDate"");
                CLUSTER ""CalculatedHistory"" USING ""IX_CalculatedHistory_InputCode_EffectiveDate"";
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP INDEX IF EXISTS ""IX_CurrencyRate_CurrencyCode"";
                DROP INDEX IF EXISTS ""IX_CalculatedHistory_InputCode_EffectiveDate""");
        }
    }
}
