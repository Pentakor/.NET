using Ex04.Menus.Test.Event;
using Ex04.Menus.Test.Interface;

namespace Ex04.Menus.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestInterface test1 = new TestInterface();
            test1.Run();

            TestEvents test2 = new TestEvents();
            test2.Run();
        }
    }
}
