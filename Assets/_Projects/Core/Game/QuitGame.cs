﻿using UnityEngine;

namespace Core.Game
{
    public static class QuitGame
    {
        public static void Quit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif

            Application.Quit();
        }
    }
}
