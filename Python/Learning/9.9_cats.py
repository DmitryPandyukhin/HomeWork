# С версии 3.6 порядок пар "ключ-значение" в словаре при выводе сохраняется.
# Иначе для удобства вывода лучше использовать список.
dict_cats = dict()
for circle_number in range(1, 101):
    for cat_number in range(1, 101):
        if circle_number == 1:
            dict_cats[cat_number] = True
        elif circle_number in (2, 3, 100) and cat_number % circle_number == 0:
            dict_cats[cat_number] = not dict_cats[cat_number]
        
print(dict_cats)
