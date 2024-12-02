using Fluxor;

namespace BlazorFluxorPractice.States.Counter
{
    [FeatureState]
    public record CounterState
    {
        public int Count { get; init; }
        
    }
};

