using MasterMind.Data;
using MasterMind.Data.Enum;
using Microsoft.AspNetCore.Components;

namespace MasterMind.Pages.Modal
{
    public partial class PlayerModal
    {
        [Parameter] public EventCallback<Player> PlayerCallBack { get; set; }
        public Player Player { get; set; } = new();
        public SexEnum SexEnum { get; set; }

        public async Task ValidPlayer()
        {
            if (String.IsNullOrEmpty(Player.Username))
            {
                if (Player.Sex == SexEnum.female)
                    Player.Username = "Joueuse 1";
                else
                    Player.Username = "Joueuse 2";
            }

            await PlayerCallBack.InvokeAsync(Player);
        }
    }
}
