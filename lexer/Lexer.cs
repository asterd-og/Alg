using System;

namespace alg.lexer {
    public class Lexer {

        // Defs
        private string code = "";
        private char currentChar = '\0';
        private int currentPos = 0;

        private int line = 1;
        private int column = 1;

        public List<Token> tokens = new();

        public Lexer(string code) {
            this.code = code + '\0';
            currentChar = this.code[currentPos];
        }

        public void addToken(TokenType type, string data) {
            tokens.Add(new(line, column, type, data));
        }

        private string generateString(string data) {
            return $"{line}:{column} - {data}";
        }

        public void advanceCharacter() {
            currentPos++;
            currentChar = code[currentPos];
            column++;
        }

        public char checkNextChar() {
            if (currentPos+1 != code.Length) {
                return code[currentPos+1];
            } else {
                return '\0';
            }
        }

        public void peek() {
            if (currentChar == '\n') {
                line++;
                column = 0;
            } else if (currentChar == '\r') {
                column = 0;
            } else if (currentChar >= 'a' && currentChar <= 'z' ||
                       currentChar >= 'A' && currentChar <= 'Z' ||
                       currentChar == '_') {
                
                string data = "";
                
                while (currentChar >= 'a' && currentChar <= 'z' ||
                       currentChar >= 'A' && currentChar <= 'Z' ||
                       currentChar >= '0' && currentChar <= '9' ||
                       currentChar == '_') {
                    
                    data += currentChar;
                    advanceCharacter();
                }
                currentPos--;

                addToken(TokenType.Name, data);
            } else if (currentChar >= '0' && currentChar <= '9') {
                string data = "";
                
                while (currentChar >= '0' && currentChar <= '9') {
                    data += currentChar;
                    advanceCharacter();
                }

                currentPos--;

                addToken(TokenType.Int, data);
            } else if (currentChar == '"') {
                string data = "";
                advanceCharacter();

                while (currentChar != '"') {
                    data += currentChar;
                    advanceCharacter();
                }

                addToken(TokenType.String, data);
            } else {
                switch (currentChar) {
                    case '(': addToken(TokenType.LParen, "("); break;
                    case ')': addToken(TokenType.RParen, ")"); break;
                    case '{': addToken(TokenType.LBrace, "{"); break;
                    case '}': addToken(TokenType.RBrace, "}"); break;
                    case ';': addToken(TokenType.SemiColon, ";"); break;
                    case ',': addToken(TokenType.Colon, ","); break;
                    case '=':
                        if (checkNextChar() == '=') {
                            advanceCharacter();
                            addToken(TokenType.EqEq, "==");
                        } else {
                            addToken(TokenType.Eq, "=");
                        }
                        break;
                    case '+':
                        if (checkNextChar() == '=') {
                            advanceCharacter();
                            addToken(TokenType.AddEq, "+=");
                        } else {
                            addToken(TokenType.Add, "+");
                        }
                        break;
                    case '-':
                        if (checkNextChar() == '=') {
                            advanceCharacter();
                            addToken(TokenType.SubEq, "-=");
                        } else {
                            addToken(TokenType.Sub, "-");
                        }
                        break;
                    case '/':
                        if (checkNextChar() == '=') {
                            advanceCharacter();
                            addToken(TokenType.DivEq, "/=");
                        } else {
                            addToken(TokenType.Div, "/");
                        }
                        break;
                    case '*':
                        if (checkNextChar() == '=') {
                            advanceCharacter();
                            addToken(TokenType.MulEq, "*=");
                        } else {
                            addToken(TokenType.Mul, "*");
                        }
                        break;
                    case '\0': break;
                    case ' ': column++; break;
                    default:
                        Utils.Error("Lexer", generateString("Unknown character."));
                        break;
                }
            }
        }

        public void lex() {
            if (code.Length > 0) {
                for (; currentPos != code.Length;) {
                    currentChar = code[currentPos];
                    peek();
                    currentPos++;
                }
            }
            addToken(TokenType.EOF, "eof");
            // for (int i)
        }
    }
}