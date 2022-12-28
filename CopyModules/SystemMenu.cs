﻿using System.Runtime.InteropServices;
using System.Windows.Interop;

/// <summary>
/// This module is included here because it can be used in any WPF project to add
/// entries to the settings menu accessed from the window icon.  To use this code,
/// it muse be copied into the Main.xaml.cs module.  The 'using' statements at the
/// top must also be added to the Main.xaml.cs module.
/// </summary>
/// 
/*
        #region System Menu

        // Related to managing the System Menu
        private IntPtr Handle
        {
            get { return new WindowInteropHelper(this).Handle; }
        }

        private const Int32 AboutMenuID = 1000;
        //private const Int32 ScrapeMenuID = 1001;

        private static IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // Check if a System Command has been executed
            if (msg == WM_SYSCOMMAND)
            {
                // Execute the appropriate code for the clicked System Menu item
                switch (wParam.ToInt32())
                {
                    case AboutMenuID:
                        AboutWindow wdw = new AboutWindow(aboutProperties);
                        wdw.ShowDialog();
                        handled = true;
                        break;
                        //case ScrapeMenuID:
                        //    var content = vm.GetPageHtml(browser.Document);
                        //    vm.WriteToFile(content);
                        //    MessageBox.Show("\"Settings\" was clicked");
                        //    handled = true;
                        //    break;
                }
            }

            return IntPtr.Zero;
        }

        private void AddMenuItemsToSystemMenu()
        {
            // Get the Handle for the Forms System Menu
            IntPtr systemMenuHandle = GetSystemMenu(this.Handle, false);

            // Create new System Menu items just before the Close menu item
            InsertMenu(systemMenuHandle, 5, MF_BYPOSITION | MF_SEPARATOR, 0, string.Empty); // separator
            InsertMenu(systemMenuHandle, 6, MF_BYPOSITION, AboutMenuID, "About");
            //InsertMenu(systemMenuHandle, 7, MF_BYPOSITION, ScrapeMenuID, "Scrape");

            // Attach the WndProc handler
            HwndSource source = HwndSource.FromHwnd(this.Handle);
            source.AddHook(new HwndSourceHook(WndProc));
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern bool InsertMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags, Int32 wIDNewItem, string lpNewItem);

        [DllImport("user32.dll")]
        private static extern bool AppendMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags, Int32 wIDNewItem, string lpNewItem);

        // Constants
        public const Int32 WM_SYSCOMMAND = 0x112;
        public const Int32 MF_SEPARATOR = 0x800;
        public const Int32 MF_BYPOSITION = 0x400;
        public const Int32 MF_STRING = 0x0;

        #endregion
*/