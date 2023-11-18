import java.util.Arrays;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;

/**
 * Перечисление римских цифр. Сопоставление римских цифр с арабскими.
 */
public enum RomanDigit {
    I(1), V(5), X(10), L(50), C(100), D(500), M(1000);
    private final int arabicDigit; // Арабское представление цифры.
    RomanDigit(int arabicDigit){ this.arabicDigit = arabicDigit; }
    int getArabicDigit() { return arabicDigit; }

    /**
     * Метод преобразует строку в римскую цифру.
     * @param romanDigitStr Предполагаемая римская цифра.
     * @return Найденное в перечислении соответствие римской цифре или null, если соответствия не найдено.
     */
    static RomanDigit getRomanDigit(char romanDigitStr){
        char ch;
        for(RomanDigit romanDigit : RomanDigit.values()){
            ch = romanDigit.toString().charAt(0);
            if(ch == romanDigitStr)
                return romanDigit;
        }
        return null;
    }

    public static List<RomanDigit> getReverseSortedValues() {
        return Arrays.stream(values())
                .sorted(Comparator.comparing((RomanDigit e) -> e.arabicDigit).reversed())
                .collect(Collectors.toList());
    }
}
