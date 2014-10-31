// --------------------------------------------------------------------------------
// <copyright file="KeePassCloudPluginExt.cs" author="Dmitriy Evsyukov (d.evsyukov@gmail.com)">
//   Copyright (c) 2014 Dmitriy Evsyukov (d.evsyukov@gmail.com). All right reserved.
// </copyright>
// --------------------------------------------------------------------------------

namespace KeePassCloudPlugin
{
    using System;
    using System.Windows.Forms;
    using KeePass.Plugins;

    public sealed class KeePassCloudPluginExt : Plugin
    {
        private IPluginHost host;
        private ToolStripSeparator menuSeparator;
        private ToolStripMenuItem menuItem;
        private ToolStripMenuItem menuItemGoogle;

        public KeePassCloudPluginExt()
        {
            this.menuSeparator = new ToolStripSeparator();
            this.menuItem = new ToolStripMenuItem();
            this.menuItem.Text = "KeePassCloud Plugin";

            this.menuItemGoogle = new ToolStripMenuItem("Synchronize with Google Drive");
            this.menuItemGoogle.Click += this.MenuItemGoogleOnClick;

            this.menuItem.DropDownItems.Add(menuItemGoogle);
        }

        public override bool Initialize(IPluginHost host)
        {
            this.host = host;

            ToolStripItemCollection toolsMenu = host.MainWindow.ToolsMenu.DropDownItems;

            toolsMenu.Add(this.menuSeparator);
            toolsMenu.Add(this.menuItem);

            return true;
        }

        public override void Terminate()
        {
            ToolStripItemCollection toolsMenu = this.host.MainWindow.ToolsMenu.DropDownItems;

            this.menuItemGoogle.Click -= this.MenuItemGoogleOnClick;
            this.menuItem.DropDownItems.Remove(menuItemGoogle);
            toolsMenu.Remove(this.menuItem);
            toolsMenu.Remove(this.menuSeparator);
        }

        private void MenuItemGoogleOnClick(object sender, EventArgs eventArgs)
        {
            var database = host.Database;
            if (database == null || !database.IsOpen)
            {
                MessageBox.Show("You have to open database!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                uint numberOfGroups;
                uint numberOfEntries;
                database.RootGroup.GetCounts(true, out numberOfGroups, out numberOfEntries);
                MessageBox.Show(string.Format("You have {0} groups and {1} entries.", numberOfGroups, numberOfEntries), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
    }
}