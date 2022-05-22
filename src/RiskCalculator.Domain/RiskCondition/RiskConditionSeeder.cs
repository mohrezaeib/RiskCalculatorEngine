using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace RiskCalculator.RiskConditions;

public class RiskConditionSeeder : IDataSeedContributor, ITransientDependency
{
    private readonly IRiskConditionRepository _riskConditionRepository;
    private readonly IGuidGenerator _guidGenerator;

    public RiskConditionSeeder(IRiskConditionRepository riskConditionRepository, IGuidGenerator guidGenerator)
    {
        _riskConditionRepository = riskConditionRepository;
        _guidGenerator = guidGenerator;
    }


    public async Task SeedAsync(DataSeedContext context)
    {
        //throw new NotImplementedException();

        var riskConditions = await _riskConditionRepository.GetListAsync();
        await _riskConditionRepository.DeleteManyAsync(riskConditions, true);
        
        var rootRiskCondition = CreateRootRiskCondition();
        await _riskConditionRepository.InsertAsync(rootRiskCondition, true);
    }

    public RiskCondition CreateRootRiskCondition()
    {
        return new RiskCondition(_guidGenerator.Create())
        {
            MappingTitle = nameof(RiskAssessmentDto),
            Title = "Overall Risk",

            EffectRatio = 1,
            // When diving type define divide filter 
            // define divide value in children in order to filter 
            // according to input value for corresponding MappingTitle
            // For example for mapping Title "ContactType"
            // and input value "Personal" only child condition  with value "Personal" 
            // are used in risk calculation

            Type = Type.FilterByConstantString,
            FilterMappingTitle = nameof(RiskAssessmentDto.ContactType),

            Operation = Operation.Sum,
            Children = new List<RiskCondition>()
            {
                new RiskCondition(_guidGenerator.Create())
                {
                    MappingTitle = nameof(RiskAssessmentDto),
                    Title = "Personal Contact Risk",
                    FilterValue = "Personal",
                    EffectRatio = 1,
                    Type = Type.ListOfLists,
                    Operation = Operation.Sum,
                    Children = new List<RiskCondition>()
                    {
                        new RiskCondition(_guidGenerator.Create())
                        {
                            MappingTitle = nameof(RiskAssessmentDto.DueDiligence),
                            Title = "DueDiligence Risk",
                            EffectRatio = 1.0,
                            Type = Type.ListOfLists,
                            Operation = Operation.Sum,
                            Children = new List<RiskCondition>()
                            {
                                new RiskCondition(_guidGenerator.Create())
                                {
                                    MappingTitle = nameof(DueDiligenceDto.EmploymentStatus),
                                    EffectRatio = 2,
                                    Title = "Employment Status Risk",
                                    Type = Type.ListOfStrings,
                                    Operation = Operation.Sum,
                                    Children = new List<RiskCondition>()
                                    {
                                        new RiskCondition(_guidGenerator.Create())
                                        {
                                            Score = 5,
                                            ConstantString = "FullTime",
                                            Title = "Full Time Employment Risk of Personal Contact",
                                        },
                                        new RiskCondition(_guidGenerator.Create())
                                        {
                                            Score = 15,
                                            ConstantString = "PartTime",
                                            Title = "Part Time Employment Risk Of Personal Contact",
                                        }
                                    }
                                },
                                new RiskCondition(_guidGenerator.Create())
                                {
                                    MappingTitle = nameof(DueDiligenceDto.IndustriesRiskLevel),
                                    EffectRatio = 0.25,
                                    Title = "Industries Risk Level",
                                    Type = Type.ListOfRanges,
                                    Operation = Operation.Max,
                                    Children = new List<RiskCondition>()
                                    {
                                        new RiskCondition(_guidGenerator.Create())
                                        {
                                            Score = 0,
                                            StartRange = 0,
                                            EndRange = 0,
                                            Title = "Industries Risk Low - Personal Contact",
                                        },
                                        new RiskCondition(_guidGenerator.Create())
                                        {
                                            Score = 15,
                                            StartRange = 1,
                                            EndRange = 5,
                                            Title = "Industries Risk Moderate - Personal Contact",
                                        },
                                        new RiskCondition(_guidGenerator.Create())
                                        {
                                            Score = 30,
                                            StartRange = 6,
                                            EndRange = 20,
                                            Title = "Industries Risk High - Personal Contact",
                                        },
                                    }
                                },
                            }
                        },
                        new RiskCondition(_guidGenerator.Create())
                        {
                            MappingTitle = nameof(RiskAssessmentDto.FirstPartyFrauds),
                            Type = Type.Range,
                            Title = "First Party Fraud Risk",
                            EffectRatio = 3,
                            Operation = Operation.Sum,
                            Children = new List<RiskCondition>()
                            {
                                new RiskCondition(_guidGenerator.Create())
                                {
                                    Score = 0,
                                    StartRange = 0,
                                    EndRange = 0,
                                    Title = "First Party Fraud Low - Personal Contact",
                                },
                                new RiskCondition(_guidGenerator.Create())
                                {
                                    Score = 10,
                                    StartRange = 1,
                                    EndRange = 3,
                                    Title = "First Party Fraud Moderate - Personal Contact",
                                },
                                new RiskCondition(_guidGenerator.Create())
                                {
                                    Score = 20,
                                    StartRange = 4,
                                    EndRange = 20,
                                    Title = "First Party Fraud High - Personal Contact",
                                },
                            }
                        }
                    }
                },

                new RiskCondition(_guidGenerator.Create())
                {
                    MappingTitle = nameof(RiskAssessmentDto),
                    Title = "Business Contact Risk",
                    FilterValue = "Business",
                    EffectRatio = 1,
                    Type = Type.ListOfLists,
                    Operation = Operation.Sum,
                    Children = new List<RiskCondition>()
                    {
                        new RiskCondition(_guidGenerator.Create())
                        {
                            MappingTitle = nameof(RiskAssessmentDto.DueDiligence),
                            Title = "DueDiligence Risk",
                            EffectRatio = 5.0,
                            Type = Type.ListOfLists,
                            Operation = Operation.Sum,
                            Children = new List<RiskCondition>()
                            {
                                new RiskCondition(_guidGenerator.Create())
                                {
                                    MappingTitle = nameof(DueDiligenceDto.EmploymentStatus),
                                    EffectRatio = 0.25,
                                    Title = "Employment Status Risk",
                                    Type = Type.ListOfStrings,
                                    Operation = Operation.Sum,
                                    Children = new List<RiskCondition>()
                                    {
                                        new RiskCondition(_guidGenerator.Create())
                                        {
                                            Score = 5,
                                            ConstantString = "FullTime",
                                        },
                                        new RiskCondition(_guidGenerator.Create())
                                        {
                                            Score = 15,
                                            ConstantString = "PartTime",
                                        }
                                    }
                                },
                                new RiskCondition(_guidGenerator.Create())
                                {
                                    MappingTitle = nameof(DueDiligenceDto.IndustriesRiskLevel),
                                    EffectRatio = 8,
                                    Title = "Industries Risk Level",
                                    Type = Type.ListOfRanges,
                                    Operation = Operation.Max,
                                    Children = new List<RiskCondition>()
                                    {
                                        new RiskCondition(_guidGenerator.Create())
                                        {
                                            Score = 0,
                                            StartRange = 0,
                                            EndRange = 0,
                                            Title = "Industries Low - Business Contact",
                                        },
                                        new RiskCondition(_guidGenerator.Create())
                                        {
                                            Score = 15,
                                            StartRange = 1,
                                            EndRange = 5,
                                            Title = "Industries Moderate - Business Contact",
                                        },
                                        new RiskCondition(_guidGenerator.Create())
                                        {
                                            Score = 30,
                                            StartRange = 6,
                                            EndRange = 20,
                                            Title = "Industries High - Business Contact",
                                        },
                                    }
                                },
                            }
                        },
                        new RiskCondition(_guidGenerator.Create())
                        {
                            MappingTitle = nameof(RiskAssessmentDto.FirstPartyFrauds),
                            Type = Type.Range,
                            Title = "First Party Fraud Risk",
                            EffectRatio = 0.25,
                            Operation = Operation.Sum,
                            Children = new List<RiskCondition>()
                            {
                                new RiskCondition(_guidGenerator.Create())
                                {
                                    Score = 0,
                                    StartRange = 0,
                                    EndRange = 0,
                                    Title = "First Party Fraud Low - Business Contact",
                                },
                                new RiskCondition(_guidGenerator.Create())
                                {
                                    Score = 10,
                                    StartRange = 1,
                                    EndRange = 3,
                                    Title = "First Party Fraud Moderate - Business Contact",
                                },
                                new RiskCondition(_guidGenerator.Create())
                                {
                                    Score = 20,
                                    StartRange = 4,
                                    EndRange = 20,
                                    Title = "First Party Fraud High - Business Contact",
                                },
                            }
                        }
                    }
                }
            }
        };
    }
}