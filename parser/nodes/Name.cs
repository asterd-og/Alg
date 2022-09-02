using System;
using alg.lexer;

namespace alg.parser.nodes {
    public static class Name {
        private static Parser parser = Program.parser;

        public static Node parseName() {
            string type = parser.currentToken.data;
            bool ptr = false;
            
            parser.eat(TokenType.Name);

            if (parser.currentToken.type == TokenType.Mul) {
                ptr = true;
                parser.eat(TokenType.Mul);
            }

            if (parser.currentToken.type == TokenType.Name) {
                //name name() {}
                string name = parser.currentToken.data;
                parser.eat(parser.currentToken.type);
                if (parser.currentToken.type == TokenType.LParen) {
                    //its a func def

                    //we will parse arguments later
                    parser.eat(TokenType.LParen);
                    parser.eat(TokenType.RParen);

                    return Body.parseFuncDef(type, name);
                } else if (parser.currentToken.type == TokenType.Eq) {
                    parser.eat(TokenType.Eq);

                    return Var.parseVarDef(type, name, ptr);
                }
            } else if (parser.currentToken.type == TokenType.LParen) {
                //name() its a func call
                
                List<Token> args = Body.parseArgs();

                return Body.parseFuncCall(type, args);
            }
            Utils.Error("Parser", parser.generateString($"Unexpected token '{TokenUtils.type2Str(parser.currentToken.type)}'"));
            return null;
        }
    }
}