using System;
using alg.lexer;

namespace alg.parser.nodes {
    public static class Var {
        private static Parser parser = Program.parser;

        public static string parseExpr2Str(List<Token> tokens) {
            string final = "";

            foreach (Token tok in tokens) {
                if (tok.type != TokenType.SemiColon && tok.type != TokenType.String) final += tok.data;
                else {
                    Utils.Error("Parser", parser.generateString($"Unexpected token '{TokenUtils.type2Str(parser.currentToken.type)}'"));
                }
            }

            return final;
        }

        public static Node parseVarDef(string type, string name, bool ptr = false) {
            //its current on the value
            if (parser.currentToken.type != TokenType.Int    &&
                parser.currentToken.type != TokenType.Name   &&
                parser.currentToken.type != TokenType.String &&
                parser.currentToken.type != TokenType.LParen &&
                parser.currentToken.type != TokenType.RParen) {

                Utils.Error("Parser", parser.generateString($"Unexpected value '{TokenUtils.type2Str(parser.currentToken.type)}'"));
            }

            string value = "";
            TokenType tType = 0;
            NodeType nType = 0;
            
            List<Token> expr = new();
            
            if (parser.getTokenRelative(1).type != TokenType.SemiColon) {
                //its an expr
                while (parser.currentToken.type != TokenType.SemiColon) {
                    if (parser.currentToken.type == TokenType.EOF) {
                        Utils.Error("Parser", parser.generateString($"Unexpected End-Of-File"));
                    }
                    expr.Add(parser.currentToken);
                    parser.eat(parser.currentToken.type);
                }
                value = parseExpr2Str(expr);
                nType = NodeType.Expr;
            } else {
                value = parser.currentToken.data;
                tType = parser.currentToken.type;
                parser.eat(tType);
            }

            Node data = new(nType, parser.currentToken.line, parser.currentToken.column, value, tType, "", "", "", null, null, null, null, expr);
            
            Node node = new(NodeType.VarDef,
                            parser.currentToken.line,
                            parser.currentToken.column,
                            "",
                            TokenType.Name,
                            type,
                            "",
                            name,
                            data,
                            null,
                            null,
                            null,
                            null,
                            ptr);

            parser.eat(TokenType.SemiColon);

            return node;
        }
    }
}