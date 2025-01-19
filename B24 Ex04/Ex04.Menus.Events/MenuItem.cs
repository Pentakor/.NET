using System;
using System.Collections.Generic;

namespace Ex04.Menus.Events
{
    public class MenuItem
    {
        private readonly string r_Title;
        private readonly List<MenuItem> r_Children;
        private readonly bool r_IsMainMenu;
        public event Action SelectedMenuItem;

        public MenuItem(string i_Title, bool i_IsMainMenu = false)
        {
            r_Title = i_Title;
            r_IsMainMenu = i_IsMainMenu;
            r_Children = new List<MenuItem>();
            SelectedMenuItem = null;
        }

        internal bool IsMainMenu
        {
            get { return r_IsMainMenu; }
        }

        internal List<MenuItem> Children
        {
            get { return r_Children; }
        }

        internal string Title
        {
            get { return r_Title; }
        }

        public void AddChild(MenuItem i_NewMenuItem)
        {
            r_Children.Add(i_NewMenuItem);
        }

        public void RemoveChild(MenuItem i_NewMenuItem)
        {
            r_Children.Remove(i_NewMenuItem);
        }

        private bool isLeaf()
        {
            return (SelectedMenuItem != null);
        }

        public void Select()
        {
            if (isLeaf())
            {
                OnSelectLeaf();
            }
            else
            {
                BaseMenu.Show(this);
            }
        }

        protected virtual void OnSelectLeaf()
        {
            if (SelectedMenuItem != null)
            {
                SelectedMenuItem.Invoke();
            }
        }
    }
}
