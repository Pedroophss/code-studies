using DataStructs.Tries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructs.test.Trees
{
    public class TriesTest
    {
        [Fact]
        public void AddWord()
        {
            var trie = new Trie();

            trie.AddWord("Batata");
            trie.AddWord("Bacia");
            trie.AddWord("Ba761");
            trie.AddWord(null);
            trie.AddWord("");

            var has = trie.HasWord("xablau");
            has = trie.HasWord("batata");
            has = trie.HasWord("bata");

            has = trie.Delete("xablau");
            has = trie.Delete("bacia");
            has = trie.Delete("batata");
        }
    }
}
