namespace DmxLightControlDemo.Core.Effects;

public abstract class EffectBase
{
    protected EffectBase(IStateManager stateManager, float lowValue, float highValue, IEnumerable<DmxParameter> dmxParameters)
    {
        _dmxParameters.AddRange(dmxParameters);
        _stateManager = stateManager;
        LowValue = lowValue;
        HighValue = highValue;
    }
    
    private readonly IStateManager _stateManager;
    private readonly List<DmxParameter> _dmxParameters = new();
    private int _currentParameterIndex;
    private TimeSpan _currentElapsedTimeSpan  = TimeSpan.Zero;
    
    public TimeSpan TotalEffectTimeSpan { get; set; } = TimeSpan.Zero;
    public float LowValue { get; set; }
    public float HighValue { get; set; }

    // assume that this is called by StateManager's UpdateCycled event
    public void OnUpdateCycled(object? sender, UpdateCycledEventArgs e)
    {
        if (TotalEffectTimeSpan.TotalMilliseconds == 0)
            return;
        if (_dmxParameters.Count == 0)
            return;
        _currentElapsedTimeSpan += e.DeltaTime;
        
        var progressPercentage = (float)(_currentElapsedTimeSpan.TotalMilliseconds / TotalEffectTimeSpan.TotalMilliseconds);
        var parameter = _dmxParameters[_currentParameterIndex];
        if (progressPercentage >= 1)
        {
            var finalValue = CalculateNewValue(parameter, 1);
            parameter.CurrentValue = finalValue;
            _stateManager.SetDmxValue(parameter, finalValue);
            
            _currentElapsedTimeSpan = TimeSpan.Zero;
            progressPercentage = 0;
            _currentParameterIndex++;
            if (_currentParameterIndex >= _dmxParameters.Count)
                _currentParameterIndex = 0;
            parameter = _dmxParameters[_currentParameterIndex];
            return;
        }

        var newValue = CalculateNewValue(parameter, progressPercentage);
        parameter.CurrentValue = newValue;
        _stateManager.SetDmxValue(parameter, newValue);
    }
    
    /// <summary>
    /// Implementations will calculate the parameter's new value based on the progress through the effect's <see cref="TotalEffectTimeSpan"/>.
    /// </summary>
    /// <param name="parameter">The parameter we're going to manipulate.</param>
    /// <param name="progressPercentage">The percentage of progress made through the effect's <see cref="TotalEffectTimeSpan"/>.</param>
    /// <returns></returns>
    protected abstract float CalculateNewValue(DmxParameter parameter, float progressPercentage);
}