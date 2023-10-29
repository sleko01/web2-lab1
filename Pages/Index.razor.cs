namespace TournamentCreator.Pages
{
    public partial class Index
    {
        private bool IsLoggedIn { get; set; }
        protected override async Task OnInitializedAsync()
        {
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