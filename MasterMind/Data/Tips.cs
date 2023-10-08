using System.Drawing;
using MasterMind.Data.Enum;

namespace MasterMind.Data
{
    public class Tips
    {
        public int Id { get; set; }
        public ColorEnum Color { get; set; }
        public int Turn { get; set; }
        public bool IsActive { get; set; } = false;
        public int Priority { get; set; } = 3;
    }
}
