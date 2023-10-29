using Microsoft.AspNetCore.Components.Authorization;
using TournamentCreator.Data;

namespace TournamentCreator.Pages
{
    public partial class Schedule
    {
        public Tournament? Tournament { get; set; }
        public bool IsLoggedIn { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            Tournament = Tournament.CurrentTournament;
            var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (authenticationState.User.Identity.Name is null)
            {
                IsLoggedIn = false;
                return;
            }
            IsLoggedIn = authenticationState.User.Identity.IsAuthenticated;
        }
    }
}