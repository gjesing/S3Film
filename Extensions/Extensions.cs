using System;

namespace Extensions
{
    public static class Extensions
    {
       public static string IconFolder = System.IO.Path.GetDirectoryName(System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "") + @"S3Film.GUI\Assets\Img\Icons\";
    }
}
