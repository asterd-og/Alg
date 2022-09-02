using System;
using alg.lexer;

namespace alg.parser.nodes {
    public static class Body {
        private static Parser parser = Program.parser;

        public static List<Node> parseBody() {
            int open = 0;

            int line = parser.currentToken.line;
            int column = parser.currentToken.column;

            parser.eat(TokenType.LBrace);
            // advanceToken();
            open = 1;

            List<Node> innerNodes = new();

            while (open >= 1) {
                if (parser.currentToken.type == TokenType.EOF) {
                    Utils.Error("Parser", parser.generateString($"Unexpected End-Of-File"));
                }
                if (parser.currentToken.type == TokenType.LBrace) {
                    open++;
                    parser.eat(TokenType.LBrace);
                } else if (parser.currentToken.type == TokenType.RBrace) {
                    open--;
                    parser.eat(TokenType.RBrace);
                } else {                    
                    innerNodes.Add(Name.parseName());
                }
            }

            return innerNodes;
        }

        public static List<Token> parseArgs() {
            List<Token> args = new();

            parser.eat(TokenType.LParen);

            while (parser.currentToken.type != TokenType.RParen) {
                args.Add(parser.currentToken); // Should be a name
                parser.eat(TokenType.Name);
                
                if (parser.currentToken.type != TokenType.RParen) {
                    parser.eat(TokenType.Colon);
                } else {
                    break;
                }
            }

            parser.eat(TokenType.RParen);

            return args;
        }

        public static Node parseFuncDef(string type, string name) {
            int line = parser.currentToken.line;
            int column = parser.currentToken.column;

            List<Node> innerNodes = parseBody();

            Node node = new(NodeType.FuncDef,
                            line,
                            column,
                            "",
                            TokenType.Name,
                            type,
                            "",
                            name,
                            null,
                            null,
                            null,
                            innerNodes
                            );
            
            return node;
        }

        public static Node parseFuncCall(string name, List<Token> args) {
            int line = parser.currentToken.line;
            int column = parser.currentToken.column;

            parser.eat(TokenType.SemiColon);

            Node node = new(NodeType.FuncCall,
                            parser.currentToken.line,
                            parser.currentToken.column,
                            "",
                            TokenType.Name,
                            name,
                            "",
                            "",
                            null,
                            null,
                            null,
                            null,
                            args);

        return node;
        }
    }
}