using System;

namespace alg.lexer {
    public enum TokenType {
        EOF,      // END OF FILE
        Name,     // [a-zA-Z_][0-9]*
        String,   // "*"
        Int,      // [0-9]+
        LParen,   // (
        RParen,   // )
        LBrace,   // {
        RBrace,   // }
        SemiColon,// ;
        Eq,
        EqEq,
        Add,      // +
        AddEq,    // +=
        Sub,      // -
        SubEq,    // -=
        Div,      // /
        DivEq,    // /=
        Mul,      // *
        MulEq,    // *=
        Colon,    // ,
    }

    public static class TokenUtils {
        static Dictionary<TokenType, string> tok2Str = new(){
            {TokenType.EOF,       "EOF"},
            {TokenType.Name,      "Name"},
            {TokenType.String,    "String"},
            {TokenType.Int,       "Int"},
            {TokenType.LParen,    "LParen"},
            {TokenType.RParen,    "RParen"},
            {TokenType.LBrace,    "LBrace"},
            {TokenType.RBrace,    "RBrace"},
            {TokenType.SemiColon, "SemiColon"},
            {TokenType.Eq,        "Eq"},
            {TokenType.EqEq,      "EqEq"},
            {TokenType.Add,       "Add"},
            {TokenType.AddEq,     "AddEq"},
            {TokenType.Sub,       "Sub"},
            {TokenType.SubEq,     "SubEq"},
            {TokenType.Div,       "Div"},
            {TokenType.DivEq,     "DivEq"},
            {TokenType.Mul,       "Mul"},
            {TokenType.MulEq,     "MulEq"},
            {TokenType.Colon,     "Colon"},
        };

        public static string type2Str(TokenType type) {
            return tok2Str[type];
        }
    }

    public class Token {
        public int line = 0;
        public int column = 0;
        public string data = "";

        public TokenType type;

        

        public Token(int line, int column, TokenType type, string data) {
            this.line = line;
            this.column = column;
            this.type   = type;

            this.data = data;
        }
    }
}