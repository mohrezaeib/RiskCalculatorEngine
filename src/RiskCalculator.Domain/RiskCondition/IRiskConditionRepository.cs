using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace RiskCalculator.RiskConditions;

public interface IRiskConditionRepository : IRepository<RiskCondition, Guid>
{
    Task<RiskCondition> GetConditionTree(Guid? parentId);
}