import spacy

with open("Francés/INPUT_RAW.txt", encoding="utf8") as f:
    text = f.read()

nlp = spacy.load("fr_core_news_sm")
doc = nlp(text)

with open("Francés/OUTPUT_RAW.txt", "w", encoding="utf-8") as f:
    for token in doc:
        f.write('"' + token.text + '"' + '\t' + token.pos_+ "\n")