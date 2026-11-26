using System;
using System.IO;

try
{
    using var game = new FNA_RPG.Game1();
    game.Run();
}
catch (Exception e)
{
    File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "crash.txt"), e.ToString());
    throw;
}