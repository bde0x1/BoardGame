using System;

namespace BoardGame.Domain_Model
{
    public static class Cube
    {
        private static Random random = new Random();
 
        public static int ThrownValue => random.Next(1, 7);        
    }  
}
