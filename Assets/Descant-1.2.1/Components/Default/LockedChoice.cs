// Please see https://omch.tech/descant/#lockedchoice for documentation

using System;
using Descant.Utilities;
using UnityEngine;

namespace Descant.Components
{
    [Serializable, MaxQuantity(Single.PositiveInfinity), NodeType(DescantNodeType.Choice)]
    public class LockedChoice : DescantComponent
    {
        [Inline] public DescantActor Actor;
        
        [ParameterGroup("Index of the choice to change (base 1)")] public int ChoiceNumber;
        
        [ParameterGroup("Variable to check")] public VariableType VariableType;
        [ParameterGroup("Variable to check")] public string VariableName;
        
        [ParameterGroup("Choice will be locked if:")] public ComparisonType ComparisonType;
        [ParameterGroup("Choice will be locked if:")] public float Comparison;

        public override DescantNodeInvokeResult Invoke(DescantNodeInvokeResult result)
        {
            if (
                // Statistic
                (VariableType.Equals(VariableType.Statistic) && DescantComponentUtilities.CompareVariable(
                    Actor.Statistics[VariableName], Comparison, ComparisonType)) ||
                
                // Stat
                (VariableType.Equals(VariableType.Stat) && DescantComponentUtilities.CompareVariable(
                    Actor.Stat.GetValue(VariableName), Comparison, ComparisonType)) ||
                
                // Topic
                (VariableType.Equals(VariableType.Topic) && Actor.Topics.Contains(VariableName)) ||
                
                // Relationship
                (VariableType.Equals(VariableType.Relationship) && DescantComponentUtilities.CompareVariable(
                    Actor.Relationships[VariableName], Comparison, ComparisonType)) ||
                
                // Dialogue attempts
                (VariableType.Equals(VariableType.DialogueAttempts) && DescantComponentUtilities.CompareVariable(
                    Actor.DialogueAttempts, Comparison, ComparisonType)))
            {
                result.Text.RemoveAt(ChoiceNumber - 1);
            }
            else
            {
                Debug.Log("LockedChoice did not meet the requirements for variable \""+ VariableName + "\" and" +
                          " will still be displayed."
                          );
            }
            
            return result;
        }
    }
}