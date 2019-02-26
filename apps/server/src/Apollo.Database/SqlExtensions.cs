using SqlKata.Compilers;

namespace Apollo.Database
{
    internal static class SqlExtensions
    {
        private static readonly Compiler Compiler = new MySqlCompiler();

        public static string CompileQuery(this SqlKata.Query query)
        {
            return Compiler.Compile(query).ToString();
        }
    }
}
