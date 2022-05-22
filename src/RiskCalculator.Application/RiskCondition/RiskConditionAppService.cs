using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RiskCalculator.RiskConditions;

public interface IRiskConditionAppService : IApplicationService
{
    Task<RiskAssessmentResultDto> CalculateRisk(RiskAssessmentDto dto);
    Task<string> GetRules(Guid? parentId);
}

public class RiskConditionAppService : ApplicationService, IRiskConditionAppService
{
    private readonly IRiskConditionRepository _riskConditionRepository;

    public RiskConditionAppService(IRiskConditionRepository riskConditionRepository)
    {
        _riskConditionRepository = riskConditionRepository;
    }

    [HttpPost("/api/app/CalculateRisk")]
    public async Task<RiskAssessmentResultDto> CalculateRisk(RiskAssessmentDto dto)
    {
        var rootRiskCondition = await _riskConditionRepository.GetConditionTree(null);
        RiskAssessmentResultDto score = Calculate(rootRiskCondition, dto);

        return score;
    }

    [HttpGet("/api/app/Rules")]
    public async Task<string> GetRules(Guid? parentId)
    {
        var root = await _riskConditionRepository.GetConditionTree(parentId);

        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        return JsonSerializer.Serialize(root, options);
    }

    public static RiskAssessmentResultDto Calculate(RiskCondition rootCondition, object value)
    {
        if (rootCondition == null)
        {
            return new RiskAssessmentResultDto()
            {
                //This Tell us What kind of Risk we are calculating
                Title = $"No Condition Found For {value}",
            };
        }
        var scores = new List<double>();
        var result = new RiskAssessmentResultDto()
        {
            //This Tell us What kind of Risk we are calculating
            Title = rootCondition.Title,
            Children = new List<RiskAssessmentResultDto>(),
        };

        if (rootCondition.Type == Type.ListOfLists)
        {
            foreach (var prop in value.GetType().GetProperties())
            {
                var condition = rootCondition.Children?.FirstOrDefault(x => x.MappingTitle == prop.Name);
                var propertyValue = prop.GetValue(value, null);
                if (condition != null)
                {
                    // This is a recursive Function 
                    var childRisks = Calculate(condition, propertyValue);
                    result.Children.Add(childRisks);
                    scores.Add(childRisks.EffectiveRiskScore);
                }
            }
        }

        if (rootCondition.Type == Type.FilterByConstantString)
        {
            var prop = value.GetType().GetProperties().FirstOrDefault(x => rootCondition.FilterMappingTitle == x.Name);
            var propertyValue = prop.GetValue(value, null);

            var condition = rootCondition.Children.FirstOrDefault(x => x.FilterValue == (string) propertyValue);
            // This is a recursive Function 
            var childRisks = Calculate(condition, value);
            result.Children.Add(childRisks);
            scores.Add(childRisks.EffectiveRiskScore);
        }

        if (rootCondition.Type == Type.ListOfStrings)
        {
            var castedValues = (List<string>) value;
            foreach (var castedValue in castedValues)
            {
                var childCondition = rootCondition.Children.FirstOrDefault(x => x.ConstantString == castedValue);
                scores.Add(childCondition.Score);
            }
        }

        if (rootCondition.Type == Type.ListOfRanges)
        {
            var castedValues = (List<double>) value;
            foreach (var castedValue in castedValues)
            {
                var childCondition = rootCondition.Children.FirstOrDefault(x => x.StartRange <= (double) castedValue
                    && x.EndRange >= (double) castedValue);
                scores.Add(childCondition.Score);
            }
        }


        if (rootCondition.Type == Type.Range)
        {
            var castedValue = (double) value;
            var childCondition = rootCondition.Children.FirstOrDefault(x => x.StartRange <= (double) castedValue
                                                                            && x.EndRange >= (double) castedValue);
            scores.Add(childCondition.Score);
        }


        if (rootCondition.Type == Type.ConstantString)
        {
            var castedValue = (string) value;
            var childCondition = rootCondition.Children.FirstOrDefault(x => x.ConstantString == castedValue);
            scores.Add(childCondition.Score);
        }

        result.RiskScore = ApplyOperator(scores, rootCondition.Operation);
        result.EffectiveRiskScore = result.RiskScore * rootCondition.EffectRatio;

        return result;
    }

    private static double ApplyOperator(List<double> scores, Operation operation)
    {
        switch (operation)
        {
            case Operation.Sum: return scores.Sum();
            case Operation.Count: return scores.Count;
            case Operation.Average: return scores.Average();
            case Operation.Max: return scores.Max();
            case Operation.Min: return scores.Min();
            default: return 0;
        }
    }
}

public class RiskAssessmentResultDto
{
    public string Title { get; set; }
    public double EffectiveRiskScore { get; set; }
    public double RiskScore { get; set; }

    public List<RiskAssessmentResultDto> Children { get; set; }
}