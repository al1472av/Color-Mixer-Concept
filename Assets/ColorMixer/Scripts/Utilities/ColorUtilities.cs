using System;
using UnityEngine;

namespace Utilities
{
    public static class ColorUtilities
    {
        public static Color MixColors(params Color[] colors)
        {
            Color result = new Color(0, 0, 0, 1);
            
            foreach (Color c in colors)
                result += c;

            result /= colors.Length;
            return result;
        }

        public static float CompareColors(Color colorA, Color colorB)
        {

            float r = Math.Abs(colorA.r - colorB.r);
            float g = Math.Abs(colorA.g - colorB.g);
            float b = Math.Abs(colorA.b - colorB.b);

            return (float)(1 -(r * 0.33 + g * 0.33 + b * 0.33)) * 100;
        }
    }


}