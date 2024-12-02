using Fluxor;

namespace BlazorFluxorPractice.States.Counter
{
    public class CounterEffects
    {
        private readonly IState<CounterState> _counterState;

        public CounterEffects(IState<CounterState> counterState)
        {
            _counterState = counterState;
        }

        [EffectMethod]
        public Task LogIncreasecounter(IncreaseCounter action, IDispatcher dispatcher)
        {
            Console.WriteLine($"Current count: {_counterState.Value.Count}. Counter increased with step {action.Step}");

            return Task.CompletedTask;
        }
    }
};

