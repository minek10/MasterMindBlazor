using MasterMind.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace MasterMind.Data
{
    public class Player
    {
        [MaxLength(25)]
        public string Username { get; set; } = string.Empty;
        public SexEnum Sex { get; set; }

        public int HightScore { get; set; } 
    }
}
