using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using log4net;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.ModLoader;


namespace PVZ.Worldgen
{
    public class BaseUtility
    {
		public static void LogFancy(string prefix, Exception e)
		{
            //
			LogFancy(prefix, null, e);
		}

		public static void LogFancy(string prefix, string logText, Exception e = null)
		{
			ILog logger = LogManager.GetLogger("Terraria");	
			if(e != null)
			{
				logger.Info(">---------<");			
				logger.Error(prefix + e.Message);
				logger.Error(e.StackTrace);		
				logger.Info(">---------<");				
				//ErrorLogger.Log(prefix + e.Message); ErrorLogger.Log(e.StackTrace);	ErrorLogger.Log(">---------<");	
			}else
			{
				logger.Info(">---------<");			
				logger.Info(prefix + logText);	
				logger.Info(">---------<");					
				//ErrorLogger.Log(prefix + logText);
			}		
		}
        public static bool InArray(int[] array, int value, ref int index)
        {
            //
            for (int m = 0; m < array.Length; m++) { if (value == array[m]) { index = m; return true; } }
            return false;
        }
        public static bool InArray(float[] array, float value)
        {
            for (int m = 0; m < array.Length; m++) { if (value == array[m]) { return true; } }
            return false;
        }
        public static bool InArray(float[] array, float value, ref int index)
        {
            for (int m = 0; m < array.Length; m++) { if (value == array[m]) { index = m; return true; } }
            return false;
        }
		public static float MultiLerp(float percent, params float[] floats)
		{
            //
			float per = 1f / ((float)floats.Length - 1);
			float total = per;
			int currentID = 0;
			while ((percent / total) > 1f && (currentID < floats.Length - 2)) { total += per; currentID++; }
			return MathHelper.Lerp(floats[currentID], floats[currentID + 1], (percent - (per * currentID)) / per);
		}
        public static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
        {
            //
            float newPosX = (float)(Math.Cos(rot) * (vecToRot.X - origin.X) - Math.Sin(rot) * (vecToRot.Y - origin.Y) + origin.X);
            float newPosY = (float)(Math.Sin(rot) * (vecToRot.X - origin.X) + Math.Cos(rot) * (vecToRot.Y - origin.Y) + origin.Y);
            return new Vector2(newPosX, newPosY);
        }
        public static float RotationTo(Vector2 startPos, Vector2 endPos)
        {
            return (float)Math.Atan2(endPos.Y - startPos.Y, endPos.X - startPos.X);
        }

    }
}