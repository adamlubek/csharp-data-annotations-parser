namespace App
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal class DllResolver
    {
        public static void HandleUnresolvedDlls(string dllPath) => AppDomain.CurrentDomain.AssemblyResolve += ResolveFactory(dllPath);

        private static string GetBinPath(string dllPath)
        {
            var chunks = dllPath.Split('\\');
            var path = chunks.Take(chunks.Length - 1);
            return string.Join("\\", path);
        }

        private static ResolveEventHandler ResolveFactory(string path) =>
            (_, args) => Assembly.LoadFrom(Path.Combine(GetBinPath(path), args.Name.Split(',')[0] + ".dll"));
    }
}