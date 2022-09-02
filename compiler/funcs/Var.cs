using System;
using alg.parser;
using alg.lexer;
using NCalc;

namespace alg.compiler.funcs {
    public enum Word {
        QWord,
        DWord
    }
    
    public static class Vars {
        public static Dictionary<string, int> typeSize = new(){
            {"v0", 0},
            {"u8t", 1},
            {"u16t", 2},
            {"u32t", 4},
            {"u64t", 8},
            {"int", 4},
        };

        private static Arch arch;
        private static int bp = 0;

        private static Dictionary<string, string> vars = new();
        private static Dictionary<string, Word> varsWord = new();

        private static Dictionary<string, int> varsLoc = new();

        public static void setUp() {
            arch = Program.compiler.arch;
        }

        public static void incBp(string type, bool ptr = false) {
            if (arch.arch == 1) {
                // Its 32 bits
                if (typeSize[type] > 4) {
                    bp += (typeSize[type] / 2);
                    return;
                }
            }
            bp += typeSize[type];
        }

        public static int getBp() {
            return bp;
        }

        public static string getVar(string varName) {
            return vars[varName];
        }

        public static Word getVarWord(string varName) {
            return varsWord[varName];
        }

        public static void setVar(string varName, string value) {
            vars[varName] = value;
        }

        public static void setVarWord(string varName, Word word) {
            varsWord[varName] = word;
        }

        public static int getVarLoc(string varName) {
            return varsLoc[varName];
        }

        public static void setVarLoc(string varName, int loc) {
            varsLoc[varName] = loc;
        }
    }

    public class VarDef : FuncComp {

        int bp = 0;

        // yes, runtime expressions, cuz i dont have the time rn to do other way
        // + its easier to read ig
        
        private string evalExpr(Node n) {
            string input = "";

            foreach (Token t in n.left.innerTokens) {
                if (t.type != TokenType.Name) input += t.data;
                else {
                    input += Vars.getVar(t.data);
                }
            }

            Expression e = new(input);

            return e.Evaluate().ToString();
        }

        public override int run(Node node) {
            Vars.incBp(node.leftStr);

            string data = node.left.data;
            
            if (node.left.type == NodeType.Expr) {
                data = $"{evalExpr(node)}";
            }

            Vars.setVar(node.rightStr, data);
            Vars.setVarLoc(node.rightStr, Vars.getBp());
            Vars.setVarWord(node.rightStr, (node.ptr ? Word.QWord : Word.DWord));

            arch.gen($"{(node.ptr ? "moveQWordBp" : "moveDWordBp")}", $"{Vars.getBp()}", $"{data}");
            return 0;
        }
    }
}