using System.Collections.Generic;

namespace RiskCalculator.RiskConditions;

public class DueDiligenceDto
{
    public List<string> EmploymentStatus { get; set; }
    public List<double> IndustriesRiskLevel { get; set; }
}