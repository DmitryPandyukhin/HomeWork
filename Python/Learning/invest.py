def invest(amount, rate, years):
    """Функция расчитывает процентное приращение и печатаетэ\
 ежегодную мтоговую сумму."""
    for y in range(1, years + 1):
        amount += amount * rate
        print(f"year {y}: ${amount:.2f}")

invest(100, .05, 4)
help(invest)
