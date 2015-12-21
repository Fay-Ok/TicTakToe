using System.Collections.Generic;

namespace TicTakToe3
{
    public class Board
    {

        public const string MarkerBlank = " ";
        public const string MarkerX = "X";
        public const string MarkerO = "O";


        public const string TopLeft = "TopLeft";
        public const string TopMiddle = "TopMiddle";
        public const string TopRight = "TopRight";

        public const string MiddleLeft = "MiddleLeft";
        public const string MiddleCenter = "MiddleCenter";
        public const string MiddleRight = "MiddleRight";

        public const string BottomLeft = "BottomLeft";
        public const string BottomMiddle = "BottomMiddle";
        public const string BottomRight = "BottomRight";




        private Dictionary<string, string> Locations = new Dictionary<string, string>()
        {
            {"TopLeft",MarkerBlank},
            {"TopMiddle",MarkerBlank},
            {"TopRight",MarkerBlank },

            {"MiddleLeft",MarkerBlank },
            {"MiddleCenter",MarkerBlank },
            {"MiddleRight",MarkerBlank },

            {"BottomLeft",MarkerBlank },
            {"BottomMiddle",MarkerBlank},
            {"BottomRight",MarkerBlank }

        };



        public override string ToString()
        {
            return $@" {Locations[TopLeft]} | {Locations[TopMiddle]} | {Locations[TopRight]} 
---+---+---
 {Locations[MiddleLeft]} | {Locations[MiddleCenter]} | {Locations[MiddleRight]} 
---+---+---
 {Locations[BottomLeft]} | {Locations[BottomMiddle]} | {Locations[BottomRight]} "
   ;
        }

        public void Place(string marker, string location)
        {
            Locations[location] = marker;
        }

        public bool IsWinner()
         {
            return (CheckMethod(TopLeft, TopMiddle, TopMiddle)
                 || CheckMethod(MiddleLeft, MiddleCenter, MiddleRight) 
                 || CheckMethod(BottomLeft, BottomMiddle, BottomRight)
                 || CheckMethod(TopLeft,MiddleCenter,BottomRight)
                 || CheckMethod(TopRight,MiddleCenter,BottomLeft));
                             
         }

        private bool CheckMethod(string FLocation, string SLocation, string tLocation)
        {
            return (Locations[FLocation] == Locations[SLocation] && Locations[SLocation] == Locations[tLocation] && Locations[SLocation] != MarkerBlank);
        }
    }
}