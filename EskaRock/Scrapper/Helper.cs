using System;
using System.IO;

namespace EskaRock.Scrapper;

public static class Helper
{
    public static string FormatDate(DateTime date)
    {
        return date.ToString("yyyy-MM-dd");
    }

    public static string GetOutDir()
    {
        var debugNet6Dir = Directory.GetCurrentDirectory();
        var debugDir = Directory.GetParent(debugNet6Dir);
        var binDir = debugDir.Parent;
        var srcDir = binDir.Parent;
        var srcDirFullName = srcDir.FullName;

        var outDir = Path.Combine(srcDirFullName, "out");

        CreateDirIfNotExists(outDir);

        return outDir;
    }

    private static void CreateDirIfNotExists(string dir)
    {
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }
}
