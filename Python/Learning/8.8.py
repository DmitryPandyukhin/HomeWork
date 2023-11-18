def coin_flip():
    """Возвращает выбранную случайную строку 'heads' или 'tails'."""
    if random.randint(0, 1):
        return "heads"
    else:
        return "tails"
    
for trial in range(10_000):
    heads_tally = 0
    tails_tally = 0
    while (heads_tally == 0 or tails_tally == 0):
        if coin_flip() == "heads":
            heads_tally += 1
        else:
            tails_tally += 1
        number_of_shout += 1

# Среднее количество подбрасываний в каждой попытке
print(number_of_shout / 10000)        
    
