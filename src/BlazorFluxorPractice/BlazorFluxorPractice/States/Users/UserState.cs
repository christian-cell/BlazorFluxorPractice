using BlazorFluxorPractice.Models;
using Fluxor;

namespace BlazorFluxorPractice.States.Users
{
    [FeatureState]
    public record UserState
    {
        public List<User> Users { get; set; } = new List<User>();
        public bool IsLoading { get; set; } = false;
        public bool IsFirstLoading { get; set; } = true;
    }
};

