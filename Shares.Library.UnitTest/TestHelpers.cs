using System;

namespace Shares.Library.UnitTest
{
    public static class TestHelpers 
    {
        public static double Round(float value){
            return Math.Round( value * 100f) / 100f ;
        }
    }
}