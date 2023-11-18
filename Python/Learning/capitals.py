capitals_dict = {
    'Alabama': 'Montgomery',
    'Alaska': 'Juneau',
    'Arizona': 'Phoenix',
    'Arkansas': 'Little Rock',
    'California': 'Sacramento',
    'Colorado': 'Denver',
    'Connecticut': 'Hartford',
    'Delaware': 'Dover',
    'Florida': 'Tallahassee',
    'Geargia': 'Atlanta'
}

import random

state, capital = random.choice(list(capitals_dict.items()))

response = input(f"What is the capital of {state}? ").lower()
while response != capital.lower():
    if response == "exit":
        print("Goodbye")
        break
    response = input(f"What is the capital of {state}? ").lower()
else:
    print("Correct")
