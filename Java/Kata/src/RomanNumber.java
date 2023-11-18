import java.util.List;

/**
 * Класс для работы с римскичи числами.
 */
public class RomanNumber{
    private RomanDigit[] romanNumber;
    RomanNumber(String romanNumber){
        if(checkRomanNumber(romanNumber))
            toRomanDigitArray(romanNumber);
    }

    /**
     * Метод проверяет строку на наличие в нем только символов, соответствующих римским.
     * @param number Предполагаемое римское число.
     * @return True - строка состоит полностью из символов, соответствующих римским цифрам.
     * False - в строке есть символы, не соответствующие римским цифрам.
     */
    public boolean checkRomanNumber(String number) {
        for(int i = 0; i < number.length(); i++){
            char ch = number.charAt(i);
            if(RomanDigit.getRomanDigit(ch) == null)
                return false;
        }
        return true;
    }

    /**
     * Метод преобразует строку в массим римских цифр.
     * @param number Преобразуемая строка.
     */
    private void toRomanDigitArray(String number) {
        romanNumber = new RomanDigit[number.length()];
        for(int i = 0; i < number.length(); i++){
            romanNumber[i] = RomanDigit.getRomanDigit(number.charAt(i));
        }
    }

    /**
     * Метод конвертирует арабское число в строку с римским числом.
     * @param arabicNumber Арабское число.
     * @return Строку с римским числом.
     */
    public static String toRomanNumber(int arabicNumber) {
        StringBuilder roman = new StringBuilder();
        int[] values = {1000,900,500,400,100,90,50,40,10,9,5,4,1};

        String[] romanLiterals = {"M","CM","D","CD","C","XC","L","XL","X","IX","V","IV","I"};
        for(int i=0;i<values.length;i++) {
            while(arabicNumber >= values[i]) {
                arabicNumber -= values[i];
                roman.append(romanLiterals[i]);
            }
        }

        return roman.toString();
    }

    /**
     *
     * @return Массив римских цифр.
     */
    public RomanDigit[] getRomanNumber(){
        return romanNumber;
    }
}
