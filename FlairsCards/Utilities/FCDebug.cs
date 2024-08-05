using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FlairsCards.Utilities
{
    internal class FCDebug
    {
        public static void Log(object message)
        {
            if (Config.isDebugBuild)
            {
                //UnityEngine.Debug.Log(message);
            }

        }
    }
}