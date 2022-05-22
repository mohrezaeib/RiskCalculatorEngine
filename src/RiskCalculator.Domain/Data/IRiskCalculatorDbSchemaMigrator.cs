using System.Threading.Tasks;

namespace RiskCalculator.Data;

public interface IRiskCalculatorDbSchemaMigrator
{
    Task MigrateAsync();
}
