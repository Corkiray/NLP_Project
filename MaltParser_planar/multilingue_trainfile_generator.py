# importing the module
import json

# reading the data from the file
with open('ingles/en_gum-ud-train.conllu', encoding='utf8') as f:
    data1 = f.read()

with open('frances/fr_gsd-ud-train.conllu', encoding='utf8') as f:
    data2 = f.read()


# reconstructing the data as a dictionary
with open('multilingue/train.conllu', encoding='utf8', mode='w') as f:
    f.write(data1)
    f.write(data2)