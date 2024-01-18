namespace DmxLightControlDemo.Core.Effects;

public class Chaser : EffectBase
{
    public Chaser(IStateManager stateManager, float lowValue, float highValue, IEnumerable<DmxParameter> dmxParameters) : base(stateManager, lowValue, highValue, dmxParameters)
    {
    }
    
    private bool _isIncreasing = true;
    
    protected override float CalculateNewValue(DmxParameter parameter, float progressPercentage)
    {
        _isIncreasing = progressPercentage <= .5;
        float newValue;
        if (_isIncreasing)
        {
            var stepValue = (HighValue - LowValue) * (progressPercentage * 2);
            newValue = stepValue;
            if (newValue >= HighValue)
            {
                newValue = HighValue;
            }
        }
        else
        {
            var stepValue = (HighValue - LowValue) * ((progressPercentage - .5f) * 2);
            newValue = HighValue - stepValue;
            if (newValue <= LowValue)
            {
                newValue = LowValue;
            }
        }

        return newValue;
    }
}