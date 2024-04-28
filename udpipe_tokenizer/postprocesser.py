# importing the module
import json

# reading the data from the file
with open('Nipón/OUTPUT.txt', encoding='utf8') as f:
    data = f.readlines()


# reconstructing the data as a dictionary
with open('Nipón/OUTPUT_RAW.txt', encoding='utf8', mode='w') as f:
    for line in data:
        js = json.loads(line)
        for element in js['nodes']:
            text = '"' + element['form'] + '"' + "\t" + element['properties']['upos']+'\n'
            f.write(text)