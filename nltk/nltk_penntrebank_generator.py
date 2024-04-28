import nltk.corpus as corpus
import re

# phrases = corpus.treebank.parsed_sents()
# phrases = corpus.sinica_treebank.parsed_sents()
# phrases = corpus.alpino.parsed_sents()
# phrases = corpus.floresta.parsed_sents()
# phrases = corpus.cess_esp.parsed_sents()
# phrases = corpus.cess_cat.parsed_sents()
phrases = [re.sub(r'<node|.*=".*"|.*/>', '',str(phrase).replace(" )", " .)")[:-1]+" (. .)"+str(phrase)[-1:]) for phrase in corpus.sinica_treebank.parsed_sents() if phrase.height() > 2]
# phrases = [str(phrase).replace(" )"   , " .)") for phrase in corpus.treebank.parsed_sents() if phrase.height() > 2]


n_phrases = len(phrases)
ini_dev = n_phrases*80//100
ini_test = n_phrases*90//100


train = phrases[0:ini_dev]
dev = phrases[ini_dev:ini_test]
test = phrases[ini_test:-1]

print("Bank len:",n_phrases)
print("Phrase example:",str(phrases[0]))
print("Train len:",len(train))
print("Dev len:",len(dev))
print("Test len:",len(test))

with open('train.penn', encoding='utf8', mode='w') as f:
    for phrase in train:
        f.write(str(phrase)+"\n")

with open('dev.penn', encoding='utf8', mode='w') as f:
    for phrase in dev:
        f.write(str(phrase)+"\n")

with open('test.penn', encoding='utf8', mode='w') as f:
    for phrase in test:
        f.write(str(phrase)+"\n")