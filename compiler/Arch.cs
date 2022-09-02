using System;

// WARNING:
//  KINDA WET CODE AHEAD

namespace alg.compiler {
    public enum ArchPlatform {
        x64 = 0,
        x32 = 1
    }

    public class Arch {
        public int arch = 0; // 0 = x64
                             // 1 = x32
        
        Dictionary<int, Dictionary<string, Func<string, string, int>>> ops = new();

        Dictionary<int, Dictionary<string, string>> archRegs = new();

        public string code = "";

        public Arch(int arch) {
            Dictionary<string, Func<string, string, int>> x64 = new(){
                {"moveQWordBp", moveQWordRbp64},
                {"moveDWordBp", moveDWordRbp64},
                {"moveRegQWordBp", moveRegQWordRbp64},
                {"moveRegDWordBp", moveRegDWordRbp64},
                {"push", push32_64},
                {"pop", pop32_64},
                {"move", move32_64},
                {"ret", ret32_64},
                {"label", label32_64},
                {"call", call32_64},
            };
            
            Dictionary<string, Func<string, string, int>> x32 = new(){
                {"moveQWordBp", moveQWordEbp32},
                {"moveDWordBp", moveDWordEbp32},
                {"moveRegQWordBp", moveRegQWordEbp32},
                {"moveRegDWordBp", moveRegDWordEbp32},
                {"push", push32_64},
                {"pop", pop32_64},
                {"move", move32_64},
                {"ret", ret32_64},
                {"label", label32_64},
                {"call", call32_64},
            };

            Dictionary<string, string> x32regs = new(){
                {"bp", "ebp"},
                {"sp", "esp"},
                {"ax", "eax"},
            };

            Dictionary<string, string> x64regs = new(){
                {"bp", "rbp"},
                {"sp", "rsp"},
                {"ax", "rax"},
            };

            ops.Add(0, x64);
            ops.Add(1, x32);

            archRegs.Add(0, x64regs);
            archRegs.Add(1, x32regs);

            this.arch = arch;
        }

        public void gen(string op, string left = "", string right = "") {
            try {
                ops[arch][op].Invoke(left, right);
            } catch (Exception e) {
                Console.WriteLine($"Couldnt find instruction {op}: {e}");
            }
        }

        #region REGS

        public string getReg(string reg) {
            return archRegs[arch][reg];
        }

        #endregion

        #region move

        //in x64, qword size is qword, although in x32, its dword

        private int moveQWordRbp64(string left, string right) {
            code += $"\tmov qword [rbp - {left}], {right}\n";
            return 0;
        }

        private int moveQWordEbp32(string left, string right) {
            code += $"\tmov dword [ebp - {left}], {right}\n";
            return 0;
        }
        
        //in both bits, dword is dword (XD?)

        private int moveDWordRbp64(string left, string right) {
            code += $"\tmov dword [rbp - {left}], {right}\n";
            return 0;
        }

        private int moveDWordEbp32(string left, string right) {
            code += $"\tmov dword [ebp - {left}], {right}\n";
            return 0;
        }

        //now when we want only a reg then the word

        private int moveRegQWordRbp64(string left, string right) {
            code += $"\tmov {left}, qword [rbp - {right}]\n";
            return 0;
        }

        private int moveRegQWordEbp32(string left, string right) {
            code += $"\tmov {left}, dword [ebp - {right}]\n";
            return 0;
        }

        private int moveRegDWordRbp64(string left, string right) {
            code += $"\tmov {left}, dword [rbp - {right}]\n";
            return 0;
        }

        private int moveRegDWordEbp32(string left, string right) {
            code += $"\tmov {left}, dword [ebp - {right}]\n";
            return 0;
        }

        //normal mov
        private int move32_64(string left, string right) {
            code += $"\tmov {left}, {right}\n";
            return 0;
        }

        #endregion

        #region push

        private int push32_64(string left, string right) {
            code += $"\tpush {archRegs[arch][left]}\n";
            return 0;
        }

        #endregion

        #region pop

        private int pop32_64(string left, string right) {
            code += $"\tpop {archRegs[arch][left]}\n";
            return 0;
        }

        #endregion

        #region ret

        private int ret32_64(string left, string right) {
            code += $"\tret\n";
            return 0;
        }

        #endregion

        #region label

        private int label32_64(string left, string right) {
            code += $"{left}:\n";
            return 0;
        }

        #endregion

        #region call
        
        private int call32_64(string left, string right) {
            code += $"\tcall {left}\n";
            return 0;
        }

        #endregion
    }
}