public class ArabicNumber{
    /**
     * Метод конвертирует римское число в арабское.
     * @param romanNumber Римское число.
     * @return Арабское число.
     */
    public static int toArabicNumber(RomanNumber romanNumber) {
        int result = 0;
        int arabic = 0;
        int arabicPrevious = 0;
        var arr = romanNumber.getRomanNumber();
        for(int i = arr.length - 1; i >= 0; i--){
            arabic = arr[i].getArabicDigit();
            if(arabic >= arabicPrevious){
                result += arabic;
            }
            else{
                result -= arabic;
            }
            arabicPrevious = arabic;
        }
        return result;
    }
}
