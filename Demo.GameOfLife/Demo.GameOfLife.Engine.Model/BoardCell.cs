using System.Runtime.Serialization;

namespace Demo.GameOfLife.Engine.Model
{
    [DataContract]
    public class BoardCell
    {
        public BoardCell()
        {
            
        }

        public BoardCell(int x, int y, bool alive)
        {
            XPosition = x;
            YPosition = y;
            IsAlive = alive;
        }

        [DataMember]
        public int XPosition { get; set; }

        [DataMember]
        public int YPosition { get; set; }

        [DataMember]
        public bool IsAlive { get; set; }
    }
}
