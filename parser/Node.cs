using System;
using alg.lexer;

namespace alg.parser {
    public enum NodeType {
        FuncDef,
        FuncCall,
        VarDef,
        Expr
    }

    public class Node {
        public NodeType type;
        public TokenType tokType;
        public int line;
        public int column;

        public string data = "";

        public string leftStr = "";
        public string middleStr = "";
        public string rightStr = "";

        public Node left;
        public Node middle;
        public Node right;

        public List<Node> innerNodes;
        public List<Token> innerTokens;

        public bool ptr;

        public Dictionary<string, string> methodArgs;

        public Node(NodeType type, int line, int column, string data, TokenType tokType = TokenType.Name, string leftStr = "", string middleStr = "", string rightStr = "", Node left = null, Node middle = null, Node right = null, List<Node> innerNodes = null, List<Token> innerTokens = null, bool ptr = false, Dictionary<string, string> methodArgs = null) {
            this.type = type;
            this.tokType = tokType;

            this.line = line;
            this.column = column;

            this.data = data;

            this.leftStr = leftStr;
            this.middleStr = middleStr;
            this.rightStr = rightStr;

            this.left = left;
            this.middle = middle;
            this.right = right;
            
            this.innerNodes = innerNodes;
            this.innerTokens = innerTokens;
        
            this.ptr = ptr;
        
            this.methodArgs = methodArgs;
        }
    }
}