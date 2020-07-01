using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructs.Tries
{
    public class Trie
    {
        #region ..| Constants |..

        static byte LOWER_A_INTEGER_VALUE = 97;
        static CharValidation NOT_LETTER = new CharValidation(false);

        #endregion

        private Node[] Root { get; }

        public Trie()
        {
            Root = new Node[26];
        }

        public unsafe void AddWord(string word)
        {
            if (string.IsNullOrEmpty(word))
                return;

            var processedWord = stackalloc CharValidation[word.Length];
            CharValidation* pointer = processedWord;

            for (int i = 0; i < word.Length; i++)
            {
                var aux = CheckChar(word[i]);
                if (aux.LetterIndex == -1)
                    return;

                * pointer = aux;
                pointer++;
            }

            // Check on root
            var response = processedWord[0];
            Node current = Root[response.LetterIndex] ;

            if (current == null)
                Root[response.LetterIndex] = current = new Node(response.LowerValue, null);

            // Check on childs
            for (int i = 1; i < word.Length; i++)
            {
                response = processedWord[i];
                var aux = current.GetChild(ref response);
                if (aux == null)
                {
                    current.AddChild(ref response);
                    current = current.GetChild(ref response);
                }
                else current = aux;
            }

            current.ToWord();
        }

        private CharValidation CheckChar(char inputLetter)
        {
            CharValidation output = NOT_LETTER;
            var lowerLetter = char.ToLower(inputLetter);
            var letterIndex = (byte)lowerLetter;

            if (letterIndex > 96 && letterIndex < 123)
                output = new CharValidation(lowerLetter, letterIndex);

            return output;
        }

        public bool HasWord(string word)
        {
            var aux = Find(word);
            return aux == null ? false : aux.IsWord;
        }

        private Node Find(string word)
        {
            if (string.IsNullOrEmpty(word))
                return null;

            var aux = CheckChar(word[0]);
            if (aux.LetterIndex == -1)
                return null;

            var node = Root[aux.LetterIndex];
            if (node == null)
                return null;

            for (int i = 1; i < word.Length; i++)
            {
                aux = CheckChar(word[i]);
                if (aux.LetterIndex == -1)
                    return null;


                node = node.GetChild(ref aux);
                if (node == null)
                    return null;
            }

            return node;
        }

        public bool Delete(string word)
        {
            var wordNode = Find(word);
            if (wordNode == null || !wordNode.IsWord)
                return false;

            bool removeFromRoot = true;
            while (wordNode.Parent != null)
            {
                wordNode.Parent.RemoveChild(wordNode);
                wordNode = wordNode.Parent;

                if (wordNode.NumberOfChilds > 0)
                {
                    removeFromRoot = false;
                    break;
                }
            }

            if (removeFromRoot)
                Root[wordNode.Value - LOWER_A_INTEGER_VALUE] = null;

            return true;
        }

        #region ..| Nested Classes |..

        private class Node
        {
            public char Value { get; }
            public Node Parent { get; }
            public bool IsWord { get; private set; }
            public byte NumberOfChilds { get; private set; }

            private Node[] Childs { get; }

            public Node(char value, Node parent)
            {
                Value = value;
                IsWord = false;
                Parent = parent;
                NumberOfChilds = 0;
                Childs = new Node[26];
            }

            public void AddChild(ref CharValidation child)
            {
                NumberOfChilds++;
                var node = new Node(child.LowerValue, this);
                Childs[child.LetterIndex] = node;
            }

            public void RemoveChild(Node child)
            {
                NumberOfChilds--;
                Childs[child.Value - LOWER_A_INTEGER_VALUE] = null;
            }

            public Node GetChild(ref CharValidation child) => 
                NumberOfChilds == 0 ? null : Childs[child.LetterIndex];

            public void ToWord() => IsWord = true;
            public void ToNode() => IsWord = false;

            public static implicit operator char(Node n) => n.Value;
        }

        private struct CharValidation
        {
            public readonly char LowerValue;
            public readonly int LetterIndex;

            public CharValidation(bool _)
            {
                LetterIndex = -1;
                LowerValue = char.MinValue;
            }

            public CharValidation(char lowerValue, int letterIndex)
            {
                LowerValue = lowerValue;
                LetterIndex = letterIndex - LOWER_A_INTEGER_VALUE;
            }
        }

        #endregion
    }
}
