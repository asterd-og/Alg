using System;
using alg.lexer;
using alg.parser;
using alg.compiler;

namespace alg {
    public static class Program {

        private static string code = File.ReadAllText("code.alg");

        public static Lexer lexer = new(code);
        
        public static Parser parser = new();

        public static Compiler compiler;

        public static void Main() {

            lexer.lex();

            parser.setTokens(lexer.tokens);
            parser.parse();

            compiler = new(0, 0);
            compiler.setUp();
            string compiled = compiler.Compile(parser.tree);

            Console.WriteLine("Parser:");
            
            foreach (Node node in parser.tree) {
                Console.WriteLine($"{(node.rightStr == "" ? node.data : node.rightStr)}");
                if (node.type == NodeType.FuncDef) {
                    if (node.innerNodes.Count > 0) {
                        foreach (Node n in node.innerNodes) {
                            if (n.type == NodeType.VarDef) Console.WriteLine($" - {n.rightStr} = {(n.left.data)}");
                        }
                    }
                }
            }

            Console.WriteLine("\nCompiler:");
            Console.WriteLine(compiled);
        }
    }
}