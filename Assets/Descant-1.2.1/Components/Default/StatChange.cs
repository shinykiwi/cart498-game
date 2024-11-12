// Please see https://omch.tech/descant/#statisticchange for documentation

using System;
using Descant.Utilities;

namespace Descant.Components
{
    [Serializable, MaxQuantity(Single.PositiveInfinity), NodeType(DescantNodeType.Any)]
    public class StatChange : DescantComponent
    {
        [Inline] public DescantActor Actor;
        
        [ParameterGroup("Statistic to change")] public string StatisticName;
        [ParameterGroup("Operation to perform")] public OperationType OperationType;
        [ParameterGroup("Operation to perform")] public float OperationValue;

        public override DescantNodeInvokeResult Invoke(DescantNodeInvokeResult result)
        {
            switch (OperationType)
            {
                case OperationType.IncreaseBy:
                    Actor.Stat.IncreaseBy(StatisticName, OperationValue);
                    break;
                
                case OperationType.DecreaseBy:
                    Actor.Stat.DecreaseBy(StatisticName, OperationValue);
                    break;
                
                case OperationType.Set:
                    Actor.Stat.SetEntry(StatisticName, OperationValue);
                    break;
            }
            
            return result;
        }
    }
}