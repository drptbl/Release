﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace RotationEngine
{
    public class Plugin : WoWSharp.IPlugin
    {

        private bool m_IsEnabled = false;
        
        public string Name { get { return "Rotation Engine"; } }

        public string Author { get { return "WoWSharp"; } }

        public bool AutoEnable { get { return false; } }

        public bool IsEnabled { get { return m_IsEnabled; } }

        public void OnLoad()
        {
            BuildGUI();

            WoWSharp.GUI.OnCreateGUI += GUI_OnCreateGUI;
        }

        private void GUI_OnCreateGUI(object sender, WoWSharp.GUI.OnCreateGUIEventArgs e)
        {
            BuildGUI();
        }

        public void OnUnload()
        {
            WoWSharp.GUI.OnCreateGUI -= GUI_OnCreateGUI;
        }

        public void OnEnable()
        {
            if (m_MainWindow != null)
            {
                m_MainWindow.Show();
            }

            WoWSharp.WoW.Pulsator.OnPulse += Rotator.OnPulse;
            WoWSharp.WoW.Graphics.Rendering.ActiveRenderer.OnFrame += ActiveRenderer_OnFrame;

            m_IsEnabled = true;
        }

        private void ActiveRenderer_OnFrame(object sender, WoWSharp.WoW.Graphics.Renderer.OnFrameEventArgs e)
        {

            //using (var l_2DDrawer = WoWSharp.WoW.Graphics.Rendering.ActiveRenderer.CreateDrawer2D())
            //{
            //    l_2DDrawer.DrawLine(new System.Drawing.Point(50, 50), new Point(800, 800), Color.Red);
            //}
        }

        public void OnDisable()
        {
            Rotator.Settings.Enabled = false;
            WoWSharp.WoW.Pulsator.OnPulse -= Rotator.OnPulse;

            if (m_MainWindow != null)
            {
                m_MainWindow.Hide();
            }

            m_IsEnabled = false;
        }

        private MainWindow m_MainWindow = null;
        private void BuildGUI()
        {
            m_MainWindow = WoWSharp.WoW.GameUI.CreateFrame<MainWindow>();
        }
    }
}
