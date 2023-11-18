def convert_cel_to_far(C):
    """Функция получает температуру по шкале Цельсия типа float и возвращает температуру по\
 шкале Фаренгейта типа float."""
    F = C * 9/5 + 32
    return F

def convert_far_to_cel(F):
    """Функция получает температуру по шкале Фаренгейта типа float и возвращает\
 температуру по шкале Цельсия типа float."""
    C = (F - 32) * 5/9
    return C

F = input("Enter a temperature in degrees F: ")
C = convert_far_to_cel(float(F))
print(f"{F} degrees F = {C:.2f} degrees C")

C = input("Enter a temperature in degrees C: ")
F = convert_cel_to_far(float(C))
print(f"{C} degrees C = {F:.2f} degrees F")
                       
