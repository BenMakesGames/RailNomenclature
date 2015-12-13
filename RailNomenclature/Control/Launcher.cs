using System;

namespace RailNomenclature
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Launcher
    {
        [STAThread]
        static void Main()
        {
            TheGame.Load();
            TheGame.Instance.Run();
        }
    }
#endif
}
