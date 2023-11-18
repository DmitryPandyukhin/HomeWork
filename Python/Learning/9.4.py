universities = [
    ['California Institute of Technology', 2175, 37704],
    ['Harvard', 19627, 39849],
    ['Massachusetts Institute of Technology', 10566, 40732],
    ['Princeton', 7802, 37000],
    ['Rice', 5879, 35551],
    ['Stanford', 19535, 40569],
    ['Yale', 11701, 40500]
]

def enrollment_stats(universities):
    """Получает список списков и возвращает 2 списка"""
    students = [university[1] for university in universities]
    annual_fee = [university[2] for university in universities]
    return [students, annual_fee]

def mean(list):
    """Получает один списковый аргумент и возврещает среднее значение"""
    return sum(list) / len(list)

def median(list):
    """Получает один списковый аргумент и возвращает медиану"""
    median_index = 0
    list2 = list[:]
    list2.sort()
    if len(list2) / 2 == 0:
        median_index = int(len(list2) / 2)
    else:
        median_index = int((len(list2) - 1) / 2)
    return list2[median_index]

students, annual_fee = enrollment_stats(universities)

print("*******************************")
print(f"Total students: {sum(students):,}")
print(f"Total tuition $ {sum(annual_fee):,}")
print()      
print(f"Student mean: {mean(students):,.2f}")
print(f"Student median: {median(students):,}")
print()
print(f"Tuition mean: $ {mean(annual_fee):,.2f}")
print(f"Tuition median $ {median(annual_fee):,}")
print("*******************************")
