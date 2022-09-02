using System;
using alg.parser;
using alg.lexer;

namespace alg.compiler.funcs {
    public static class FuncUtils {
        public static List<string> regsArgs = new(){"rdi",
                                                    "rsi",
                                                    "rdx",
                                                    "rcx",
                                                    "r8",
                                                    "r9",
                                                    "r10",
                                                    "r11",
                                                    "r12",
                                                    "r13",
                                                    "r14",
                                                    "r15",};
    }

    public class FuncDef : FuncComp {
        public override int run(Node node) {
            arch.gen("label", node.rightStr);

            arch.gen("push", "bp");
            arch.gen("move", $"{arch.getReg("bp")}", $"{arch.getReg("sp")}");
            return 0;
        }
    }

    public class FuncCall : FuncComp {
        public override int run(Node node)
        {
            if (node.innerTokens.Count > 0) {
                //innerTokens is the arguments
                
                int regIndex = 0;
                
                foreach (Token arg in node.innerTokens) {
                    if (arg.type == TokenType.Name) {
                        string word = "DWord";

                        if (Vars.getVarWord(arg.data) == Word.QWord) word = "QWord";

                        arch.gen($"moveReg{word}Bp", arch.getReg("ax"), $"{Vars.getVarLoc(arg.data)}");
                        arch.gen("move", FuncUtils.regsArgs[regIndex], $"{arch.getReg("ax")}");
                    } else {
                        arch.gen("move", FuncUtils.regsArgs[regIndex], arg.data);
                    }
                }
            }

            arch.gen("call", node.leftStr);

            return 0;
        }
    }


}