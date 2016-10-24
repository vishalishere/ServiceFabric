using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Demo.GameOfLife.Engine.Model
{
    [DataContract]
    public class GameBoard
    {
        [DataMember]
        public Guid Token { get; set; }

        [DataMember]
        public IEnumerable<BoardCell> Board { get; set; }
    }
}