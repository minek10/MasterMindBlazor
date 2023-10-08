using MasterMind.Data.Enum;

namespace MasterMind.Data
{
    public class Piece
    {
        public int Id { get; set; }
        public ColorEnum Color { get; set; }
        public bool IsActive { get; set; } = false;
        public int Turn { get; set; }
    }
}
