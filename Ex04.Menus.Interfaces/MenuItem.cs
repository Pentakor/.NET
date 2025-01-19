using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private readonly List<IListener> r_Listeners = new List<IListener>();
        private readonly string r_Title;
        private readonly List<MenuItem> r_Children;
        private readonly bool r_IsMainMenu;

        public MenuItem(string i_Title, bool i_IsMainMenu = false)
        {
            r_Title = i_Title;
            r_IsMainMenu = i_IsMainMenu;
            r_Children = new List<MenuItem>();
        }

        internal string Title
        {
            get { return r_Title; }
        }

        internal bool IsMainMenu
        {
            get { return r_IsMainMenu; }
        }

        internal List<MenuItem> Children
        {
            get { return r_Children; }
        }

        public void AddListener(IListener i_Listener)
        {
            r_Listeners.Add(i_Listener);
        }

        public void DeleteListener(IListener i_Listener)
        {
            r_Listeners.Remove(i_Listener);
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
            return (r_Children.Count == 0);
        }

        private void notifyListeners()
        {
            foreach (IListener listener in r_Listeners)
            {
                listener.Notify(r_Title);
            }
        }

        internal void Select()
        {
            if (isLeaf())
            {
                notifyListeners();
            }
            else
            {
                BaseMenu.Show(this);
            }
        }
    }
}
