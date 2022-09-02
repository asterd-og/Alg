using System;
using alg.parser;
using alg.compiler.funcs;

namespace alg.compiler {
    public class Compiler {
        public Arch arch;

        Dictionary<NodeType, FuncComp> funcs = new();

        public int platform = 0; // 0 = windows
                                 // 1 = linux
                                 // 2 = custom

        public Compiler(int arch = 0, int platform = 0) {
            this.arch = new(arch);
            this.platform = platform;
        }

        public void setUp() {
            Vars.setUp();
            funcs.Add(NodeType.FuncDef, new FuncDef());
            funcs.Add(NodeType.FuncCall, new FuncCall());
            funcs.Add(NodeType.VarDef, new VarDef());
        }

        public string Compile(List<Node> tree) {
            foreach (Node node in tree) {
                funcs[node.type].run(node);
                if (node.type == NodeType.FuncDef) {
                    Compile(node.innerNodes);
                    arch.gen("pop", "bp");
                    arch.gen("ret");
                }
            }

            return arch.code;
        }
    }
}