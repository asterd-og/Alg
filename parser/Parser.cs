using System;
using alg.lexer;
using alg.parser.nodes;

namespace alg.parser {
    public class Parser : Ast {

        List<Token> tokens;

        int pos = 0;
        public Token currentToken;

        public void setTokens(List<Token> tokens) {
            this.tokens = tokens;
            this.currentToken = this.tokens[this.pos];
        }

        public void advanceToken() {
            if (pos+1 < tokens.Count) {
                pos++;
                currentToken = tokens[pos];
            }
        }

        public string generateString(string data) {
            return $"{currentToken.line}:{currentToken.column} - {data}";
        }

        public void eat(TokenType type) {
            if (currentToken.type != type) {
                Utils.Error("Parser", generateString($"Expected '{TokenUtils.type2Str(type)}' but got '{TokenUtils.type2Str(currentToken.type)}'"));
            }
            advanceToken();
        }

        public Token getTokenRelative(int pos) {
            if (this.pos + pos >= 0 && this.pos + pos <= this.tokens.Count) {
                return this.tokens[this.pos + pos];
            } else {
                return null;
            }
        }

        public void parse() {
            while (currentToken.type != TokenType.EOF) {
                this.tree.Add(Name.parseName());
            }
        }
    }
}