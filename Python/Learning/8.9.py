def elections(probablity_of_elections):
    """Моделирует результат выборов в зависимости от шансов на победу по\
 результатам последнего опроса граждан."""
    if random.random() < probablity_of_elections:
        return "won"
    else:
        return "lost"

total_number_of_wins = 0

for trial in range(10_000):
    number_of_wins = 0    
    if elections(.87) == "won":
        number_of_wins += 1
    if elections(.65) == "won":
        number_of_wins += 1
    if elections(.17) == "won":
        number_of_wins += 1
    if number_of_wins > 1:
        total_number_of_wins += 1

# Процент выборов, когда победит кандидат A
percent = total_number_of_wins / 100
print(percent)
