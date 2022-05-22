using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RiskCalculator.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Guids;

namespace RiskCalculator.RiskConditions;

public class RiskConditionRepository : EfCoreRepository<RiskCalculatorDbContext, RiskCondition, Guid>,
    IRiskConditionRepository
{
    private readonly IGuidGenerator _guidGenerator;

    public RiskConditionRepository(IDbContextProvider<RiskCalculatorDbContext> dbContextProvider,
        IGuidGenerator guidGenerator) : base(
        dbContextProvider)
    {
        _guidGenerator = guidGenerator;
    }


    public async Task<RiskCondition> GetConditionTree(Guid? parentId)
    {
        var dbset = await GetDbSetAsync();
        var result = (await dbset
            .ToListAsync()).First(x => x.ParentId == parentId);
        return result;
    }
}