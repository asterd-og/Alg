using System;

namespace alg.parser {
    public class Ast {
        public List<Node> tree;

        public Ast() {
            tree = new();
        }

        public void pushNode(Node node) {
            tree.Add(node);
        }
    }
}