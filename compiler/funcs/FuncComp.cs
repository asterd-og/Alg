using alg.parser;

namespace alg.compiler.funcs {
    public class FuncComp {
        public Compiler compiler;
        public Arch arch;

        public FuncComp() {
            compiler = Program.compiler;
            arch = Program.compiler.arch;
        }

        public virtual int run(Node node){return 0;}
    }
}