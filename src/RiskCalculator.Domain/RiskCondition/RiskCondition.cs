using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace RiskCalculator.RiskConditions;

public class RiskCondition : FullAuditedAggregateRoot<Guid>
{
    public RiskCondition(Guid id)
    {
        Id = id;
    }

    public string MappingTitle { get; set; }
    public string Title { get; set; }
    public double? StartRange { get; set; }
    public double? EndRange { get; set; }
    public string ConstantString { get; set; }
    public string FilterMappingTitle { get; set; }
    public string FilterValue { get; set; }
    public double Score { get; set; }
    public double EffectRatio { get; set; }

    public Type Type { get; set; }
    public Operation Operation { get; set; }
    public Guid? ParentId { get; set; }
    public RiskCondition Parent { get; set; }
    public virtual ICollection<RiskCondition> Children { get; set; }
}