nouns = ["fossil", "horse", "aardvark", "judge", "chef", "mango",
         "extrovert", "gorilla"]
verbs = ["kicks", "jingles", "bonces", "slurps", "meows", "explodes",
         "curdles"]
adjectives = ["furry", "balding", "incredulous", "fragrant", "exuberant",
              "glistening"]
prepositions = ["against", "after", "into", "beneath", "upon", "for", "in",
                "like", "over", "within"]
adverbs = ["curiously", "extravagantly", "tantalizingly", "furiously",
           "sensously"]

import random

print(random.choice(nouns))

noun1 = random.choice(nouns)
noun2 = random.choice(nouns)
noun3 = random.choice(nouns)

verb1 = random.choice(verbs)
verb2 = random.choice(verbs)
verb3 = random.choice(verbs)

adjective1 = random.choice(adjectives)
adjective2 = random.choice(adjectives)
adjective3 = random.choice(adjectives)

preposition1 = random.choice(prepositions)
preposition2 = random.choice(prepositions)

adverb1 = random.choice(adverbs)

print(f"A {adjective1} {noun1}\n\n\
A {adjective1} {noun1} {verb1} {preposition1} the {adjective2} {noun2}\n\
{adverb1}, the {noun1} {verb2}\n\
the {noun2} {verb3} {preposition2} a {adjective3} {noun3}")
