namespace App
{
    using TypeFinder;

    public class Program
    {
        static void Main(string[] args)
        {
            DllResolver.HandleUnresolvedDlls(args[0]);
            TsGenerator.Generate(args[0], args[1], args[2]);
        }
    }
}