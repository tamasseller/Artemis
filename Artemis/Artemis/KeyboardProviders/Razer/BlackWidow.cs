﻿using System;
using System.Drawing;
using Artemis.KeyboardProviders.Razer.Utilities;
using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;

namespace Artemis.KeyboardProviders.Razer
{
    public class BlackWidow : KeyboardProvider
    {
        public BlackWidow()
        {
            Name = "Razer BlackWidow Chroma";
            CantEnableText = "Couldn't connect to your Razer BlackWidow Chroma.\n " +
                             "Please check your cables and try updating Razer Synapse.\n\n " +
                             "If needed, you can select a different keyboard in Artemis under settings.";
        }

        public override bool CanEnable()
        {
            if (!Chroma.IsSdkAvailable())
                return false;

            // Some people have Synapse installed, but not a Chroma keyboard, deal with this
            try
            {
                // Create a bitmap to send as a test
                var b = new Bitmap(22, 6);
                var razerArray = RazerUtilities.BitmapColorArray(b, 6, 22);
                Chroma.Instance.Keyboard.SetGrid(razerArray);
                Chroma.Instance.Keyboard.Clear();
            }
            catch (NullReferenceException)
            {
                return false;
            }
            return true;
        }

        public override void Enable()
        {
            Chroma.Instance.Initialize();
            Height = Constants.MaxRows;
            Width = Constants.MaxColumns;
        }

        public override void Disable()
        {
            Chroma.Instance.Uninitialize();
        }

        public override void DrawBitmap(Bitmap bitmap)
        {
            var razerArray = RazerUtilities.BitmapColorArray(bitmap, Height, Width);
            Chroma.Instance.Keyboard.SetGrid(razerArray);
        }
    }
}