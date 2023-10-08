using Microsoft.AspNetCore.Components;

namespace MasterMind.Pages
{
    public partial class Index
    {
       [Inject] public NavigationManager NavigationManager { get; set; }

        public void RedirectToGame()
        {
            NavigationManager.NavigateTo("/game");
        }
    }
}
